#!/usr/bin/env python3
"""
Search TextResourceZhCnMB.json using vector similarity search.
Uses remote embedding server for embeddings, Qdrant for vector storage.

Prerequisites:
  1. Start the embedding server: python embedding_server.py
  2. Ensure Qdrant is running at the configured URL
"""

import argparse
import json
import sys

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
DEFAULT_TOP_K = 10


def get_qdrant_client() -> QdrantClient:
    """Get Qdrant client."""
    return QdrantClient(url=QDRANT_URL)


def get_embeddings_from_server(query: str, server_url: str = EMBEDDING_SERVER_URL) -> tuple[list[float], list[int], list[float]]:
    """
    Get embeddings from the remote embedding server.

    Args:
        query: The text to embed
        server_url: The embedding server URL

    Returns:
        Tuple of (dense_vector, sparse_indices, sparse_values)
    """
    url = f"{server_url}/embed"

    payload = {
        "texts": [query],
        "return_dense": True,
        "return_sparse": True,
        "return_colbert_vecs": False
    }

    try:
        with httpx.Client(timeout=30.0) as client:
            response = client.post(url, json=payload)
            response.raise_for_status()
            data = response.json()

        dense_vec = data.get('dense_vecs', [[]])[0] if data.get('dense_vecs') else []
        sparse_indices = data.get('sparse_indices', [[]])[0] if data.get('sparse_indices') else []
        sparse_values = data.get('sparse_values', [[]])[0] if data.get('sparse_values') else []

        return dense_vec, sparse_indices, sparse_values

    except httpx.ConnectError:
        raise RuntimeError(
            f"Cannot connect to embedding server at {server_url}. "
            "Please start it first: python embedding_server.py"
        )
    except httpx.HTTPStatusError as e:
        raise RuntimeError(f"Embedding server error: {e.response.status_code} - {e.response.text}")


def check_server_health(server_url: str = EMBEDDING_SERVER_URL) -> bool:
    """Check if the embedding server is running."""
    try:
        with httpx.Client(timeout=5.0) as client:
            response = client.get(f"{server_url}/health")
            return response.status_code == 200
    except:
        return False


def search(query: str, top_k: int = DEFAULT_TOP_K, server_url: str = EMBEDDING_SERVER_URL) -> list[dict]:
    """
    Search for similar texts using hybrid vector similarity.

    Args:
        query: The search query (can be Chinese text or a StringKey)
        top_k: Number of results to return
        server_url: The embedding server URL

    Returns:
        List of search results with StringKey, Text, and similarity score
    """
    client = get_qdrant_client()

    # Get embeddings from remote server
    dense_vec, sparse_indices, sparse_values = get_embeddings_from_server(query, server_url)

    if not dense_vec:
        raise RuntimeError("Failed to get embeddings from server")

    # Hybrid search using dense vector
    results = client.query_points(
        collection_name=COLLECTION_NAME,
        query=dense_vec,
        using="text-dense",
        limit=top_k,
        with_payload=True
    ).points

    # Format results
    formatted_results = []
    for result in results:
        formatted_results.append({
            'StringKey': result.payload.get('string-key', ''),
            'Text': result.payload.get('text', ''),
            'Similarity': result.score
        })

    return formatted_results


def main():
    parser = argparse.ArgumentParser(
        description='Search game text resources using vector similarity.',
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python search.py "战斗力"
  python search.py "领取奖励" -k 20
  python search.py "[CharacterSortTypeBattlePower]"

Prerequisites:
  1. Start embedding server: python embedding_server.py
        """
    )
    parser.add_argument(
        'query',
        help='Search query (Chinese text or StringKey)'
    )
    parser.add_argument(
        '-k', '--top-k',
        type=int,
        default=DEFAULT_TOP_K,
        help=f'Number of results to return (default: {DEFAULT_TOP_K})'
    )
    parser.add_argument(
        '-j', '--json',
        action='store_true',
        help='Output results as JSON'
    )
    parser.add_argument(
        '--server-url',
        default=EMBEDDING_SERVER_URL,
        help=f'Embedding server URL (default: {EMBEDDING_SERVER_URL})'
    )

    args = parser.parse_args()

    # Check if server is running (skip for JSON output)
    if not args.json and not check_server_health(args.server_url):
        print(f"\nError: Embedding server not running at {args.server_url}", file=sys.stderr)
        print("Please start it first: python embedding_server.py", file=sys.stderr)
        sys.exit(1)

    try:
        results = search(args.query, args.top_k, args.server_url)

        if args.json:
            print(json.dumps(results, indent=2, ensure_ascii=False))
        else:
            print(f"\nSearching for: \"{args.query}\"")
            print(f"Top {len(results)} results:\n")
            for i, r in enumerate(results, 1):
                # Truncate long texts for display
                text = r['Text']
                if len(text) > 100:
                    text = text[:100] + '...'
                print(f"{i}. [{r['StringKey']}]")
                print(f"   Text: {text}")
                print(f"   Similarity: {r['Similarity']:.4f}")
                print()

    except Exception as e:
        print(f"\nError: {e}", file=sys.stderr)
        sys.exit(1)


if __name__ == '__main__':
    main()
