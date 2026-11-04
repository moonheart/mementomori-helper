#!/usr/bin/env python3
"""
Batch search for translation matches between zhCN.ts and game TextResource.

This script:
1. Extracts Chinese text values from zhCN.ts
2. Searches each text against the vector database
3. Outputs potential StringKey matches
"""

import argparse
import json
import re
import sys
from pathlib import Path
from typing import Optional

try:
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')
    if hasattr(sys.stderr, 'reconfigure'):
        sys.stderr.reconfigure(encoding='utf-8')
except Exception:
    pass

import httpx
from qdrant_client import QdrantClient

# Configuration
QDRANT_URL = "http://192.168.50.50:6333"
COLLECTION_NAME = "mmmr-zhcn"
EMBEDDING_SERVER_URL = "http://127.0.0.1:8765"


def get_qdrant_client() -> QdrantClient:
    """Get Qdrant client."""
    return QdrantClient(url=QDRANT_URL)


def get_embeddings_batch(texts: list[str], server_url: str = EMBEDDING_SERVER_URL) -> list[tuple[list[float], list[int], list[float]]]:
    """
    Get embeddings for multiple texts from the remote embedding server.

    Returns:
        List of tuples (dense_vector, sparse_indices, sparse_values)
    """
    url = f"{server_url}/embed"

    payload = {
        "texts": texts,
        "return_dense": True,
        "return_sparse": True,
        "return_colbert_vecs": False
    }

    try:
        with httpx.Client(timeout=60.0) as client:
            response = client.post(url, json=payload)
            response.raise_for_status()
            data = response.json()

        results = []
        dense_vecs = data.get('dense_vecs', [])
        sparse_indices = data.get('sparse_indices', [])
        sparse_values = data.get('sparse_values', [])

        for i in range(len(texts)):
            dense_vec = dense_vecs[i] if i < len(dense_vecs) else []
            indices = sparse_indices[i] if i < len(sparse_indices) else []
            values = sparse_values[i] if i < len(sparse_values) else []
            results.append((dense_vec, indices, values))

        return results

    except httpx.ConnectError:
        raise RuntimeError(
            f"Cannot connect to embedding server at {server_url}. "
            "Please start it first: python embedding_server.py"
        )


def search_single(dense_vec: list[float], top_k: int, client: QdrantClient) -> list[dict]:
    """Search using a pre-computed dense vector."""
    results = client.query_points(
        collection_name=COLLECTION_NAME,
        query=dense_vec,
        using="text-dense",
        limit=top_k,
        with_payload=True
    ).points

    formatted_results = []
    for result in results:
        formatted_results.append({
            'StringKey': result.payload.get('string-key', ''),
            'Text': result.payload.get('text', ''),
            'Similarity': result.score
        })

    return formatted_results


def extract_zhcn_translations(file_path: str) -> list[tuple[str, str]]:
    """
    Extract translation key-value pairs from zhCN.ts file.

    Returns:
        List of (key, chinese_text) tuples
    """
    content = Path(file_path).read_text(encoding='utf-8')

    # Match patterns like: 'KEY': 'Chinese text',
    # Also handles multi-line strings with template variables like {0}
    pattern = r"'([^']+)':\s*'([^']*(?:\\'[^']*)*)'"

    matches = re.findall(pattern, content)
    return matches


def check_server_health(server_url: str = EMBEDDING_SERVER_URL) -> bool:
    """Check if the embedding server is running."""
    try:
        with httpx.Client(timeout=5.0) as client:
            response = client.get(f"{server_url}/health")
            return response.status_code == 200
    except:
        return False


def batch_search(
    translations: list[tuple[str, str]],
    top_k: int = 3,
    similarity_threshold: float = 0.95,
    batch_size: int = 10
) -> dict:
    """
    Batch search translations against the vector database.

    Args:
        translations: List of (key, chinese_text) tuples
        top_k: Number of results per search
        similarity_threshold: Minimum similarity to consider a match
        batch_size: Number of texts to embed at once

    Returns:
        Dict mapping translation keys to search results
    """
    client = get_qdrant_client()
    results = {}

    # Filter out translations that are too short or are template patterns
    searchable = [
        (key, text) for key, text in translations
        if len(text) >= 2 and not text.startswith('{') and not re.match(r'^\{[0-9]+\}', text)
    ]

    print(f"Searching {len(searchable)} translations (filtered from {len(translations)} total)...\n")

    # Process in batches
    for i in range(0, len(searchable), batch_size):
        batch = searchable[i:i + batch_size]
        texts = [text for _, text in batch]

        try:
            embeddings = get_embeddings_batch(texts)
        except Exception as e:
            print(f"Error getting embeddings for batch {i//batch_size}: {e}", file=sys.stderr)
            continue

        for (key, text), (dense_vec, _, _) in zip(batch, embeddings):
            if not dense_vec:
                continue

            search_results = search_single(dense_vec, top_k, client)

            # Check if best match exceeds threshold
            if search_results and search_results[0]['Similarity'] >= similarity_threshold:
                results[key] = {
                    'original_text': text,
                    'matches': search_results
                }

        # Progress indicator
        if (i + batch_size) % 50 == 0 or i + batch_size >= len(searchable):
            print(f"Processed {min(i + batch_size, len(searchable))}/{len(searchable)} translations...")

    return results


def main():
    parser = argparse.ArgumentParser(
        description='Batch search translation matches between zhCN.ts and game TextResource.',
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python batch_search_zhcn.py -f src/locales/zhCN.ts
  python batch_search_zhcn.py -f src/locales/zhCN.ts -t 0.98 -o matches.json
  python batch_search_zhcn.py -f src/locales/zhCN.ts --show-all

Output format:
  For each translation key with a high-similarity match, shows:
  - Original translation text
  - Best matching StringKey(s) from game resources
  - Similarity score
        """
    )
    parser.add_argument(
        '-f', '--file',
        default='src/locales/zhCN.ts',
        help='Path to zhCN.ts file (default: src/locales/zhCN.ts)'
    )
    parser.add_argument(
        '-t', '--threshold',
        type=float,
        default=0.95,
        help='Minimum similarity threshold (default: 0.95)'
    )
    parser.add_argument(
        '-k', '--top-k',
        type=int,
        default=3,
        help='Number of results per search (default: 3)'
    )
    parser.add_argument(
        '-o', '--output',
        help='Output file for JSON results'
    )
    parser.add_argument(
        '--show-all',
        action='store_true',
        help='Show all matches, not just high-similarity ones'
    )
    parser.add_argument(
        '--min-length',
        type=int,
        default=2,
        help='Minimum text length to search (default: 2)'
    )
    parser.add_argument(
        '--server-url',
        default=EMBEDDING_SERVER_URL,
        help=f'Embedding server URL (default: {EMBEDDING_SERVER_URL})'
    )

    args = parser.parse_args()

    # Check if server is running
    if not check_server_health(args.server_url):
        print(f"\nError: Embedding server not running at {args.server_url}", file=sys.stderr)
        print("Please start it first: python embedding_server.py", file=sys.stderr)
        sys.exit(1)

    # Check if file exists
    if not Path(args.file).exists():
        print(f"\nError: File not found: {args.file}", file=sys.stderr)
        sys.exit(1)

    # Extract translations
    print(f"\nExtracting translations from {args.file}...")
    translations = extract_zhcn_translations(args.file)
    print(f"Found {len(translations)} translation entries.\n")

    if not translations:
        print("No translations found in file.", file=sys.stderr)
        sys.exit(1)

    # Batch search
    results = batch_search(
        translations,
        top_k=args.top_k,
        similarity_threshold=args.threshold if not args.show_all else 0.0
    )

    # Output results
    if args.output:
        with open(args.output, 'w', encoding='utf-8') as f:
            json.dump(results, f, indent=2, ensure_ascii=False)
        print(f"\nResults written to {args.output}")

    # Print summary
    print(f"\n{'='*60}")
    print(f"SUMMARY: Found {len(results)} translation matches")
    print(f"{'='*60}\n")

    for key, data in sorted(results.items()):
        print(f"[{key}]")
        print(f"  zhCN.ts: \"{data['original_text']}\"")
        best = data['matches'][0]
        print(f"  Best match: [[{best['StringKey']}]] -> \"{best['Text'][:80]}{'...' if len(best['Text']) > 80 else ''}\"")
        print(f"  Similarity: {best['Similarity']:.4f}")

        if len(data['matches']) > 1:
            print(f"  Other matches:")
            for match in data['matches'][1:]:
                print(f"    - [[{match['StringKey']}]] ({match['Similarity']:.4f})")
        print()

    # Generate replacement suggestions
    print(f"\n{'='*60}")
    print("POTENTIAL STRINGKEY REPLACEMENTS (similarity >= 0.99)")
    print(f"{'='*60}\n")

    exact_matches = {
        key: data for key, data in results.items()
        if data['matches'][0]['Similarity'] >= 0.99
    }

    if exact_matches:
        for key, data in sorted(exact_matches.items()):
            best = data['matches'][0]
            print(f"'{key}': '[[{best['StringKey']}]]',  // was: \"{data['original_text']}\"")
    else:
        print("No exact matches found.")


if __name__ == '__main__':
    main()
