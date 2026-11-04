#!/usr/bin/env python3
"""
Ingest TextResourceZhCnMB.json into Qdrant vector database.
Uses BGE-M3 via FlagEmbedding for dense and sparse embeddings.
"""

import json
import os
import sys
import uuid
from pathlib import Path

try:
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')
    if hasattr(sys.stderr, 'reconfigure'):
        sys.stderr.reconfigure(encoding='utf-8')
except Exception:
    pass

from FlagEmbedding import BGEM3FlagModel
from qdrant_client import QdrantClient
from qdrant_client.http import models
from tqdm import tqdm


# Configuration
DATA_FILE = "E:/Git_Github/MementoMoriData/Master/TextResourceZhCnMB.json"
QDRANT_URL = "http://192.168.50.50:6333"
COLLECTION_NAME = "mmmr-zhcn"
MODEL_NAME = "BAAI/bge-m3"
BATCH_SIZE = 25  # Batch size for embedding and insertion


def load_data(file_path: str) -> list[dict]:
    """Load JSON data from file."""
    print(f"Loading data from {file_path}...")
    with open(file_path, 'r', encoding='utf-8-sig') as f:
        data = json.load(f)
    print(f"Loaded {len(data)} records")
    return data


def get_model():
    """Load the BGE-M3 model."""
    print(f"Loading embedding model: {MODEL_NAME}...")
    model = BGEM3FlagModel(
        MODEL_NAME,
        use_fp16=True,
        device='cuda' if __import__('torch').cuda.is_available() else 'cpu'
    )
    print("Model loaded successfully")
    return model


def get_qdrant_client() -> QdrantClient:
    """Get Qdrant client."""
    return QdrantClient(url=QDRANT_URL)


def create_collection(client: QdrantClient):
    """Create or recreate collection."""
    # Delete existing collection if exists
    try:
        client.delete_collection(COLLECTION_NAME)
        print(f"Deleted existing collection: {COLLECTION_NAME}")
    except Exception:
        pass

    # Create new collection with dense and sparse vectors
    client.create_collection(
        collection_name=COLLECTION_NAME,
        vectors_config={
            "text-dense": models.VectorParams(
                size=1024,
                distance=models.Distance.COSINE
            )
        },
        sparse_vectors_config={
            "text-sparse": models.SparseVectorParams(
                modifier=models.Modifier.IDF
            )
        }
    )
    print(f"Created collection: {COLLECTION_NAME}")


def ingest_data(data: list[dict], client: QdrantClient, model: BGEM3FlagModel):
    """Ingest data into Qdrant in batches."""
    print("Starting ingestion...")

    # Filter out entries with empty text
    valid_data = [item for item in data if item.get('Text') and item['Text'].strip()]
    print(f"Valid entries with non-empty text: {len(valid_data)}")

    total_batches = (len(valid_data) + BATCH_SIZE - 1) // BATCH_SIZE

    for i in tqdm(range(0, len(valid_data), BATCH_SIZE), desc="Ingesting batches", total=total_batches):
        batch = valid_data[i:i + BATCH_SIZE]

        # Prepare batch data
        texts = [item['Text'] for item in batch]

        # Generate embeddings with BGE-M3 (returns both dense and sparse)
        embeddings = model.encode(
            texts,
            return_dense=True,
            return_sparse=True,
            return_colbert_vecs=False
        )

        # Create points
        points = []
        for j, item in enumerate(batch):
            dense_vec = embeddings['dense_vecs'][j].tolist()
            sparse_vec = embeddings['lexical_weights'][j]

            # Convert sparse vector to Qdrant format
            sparse_indices = list(sparse_vec.keys())
            sparse_values = [float(v) for v in sparse_vec.values()]

            points.append(models.PointStruct(
                id=str(uuid.uuid4()),
                vector={
                    "text-dense": dense_vec,
                    "text-sparse": models.SparseVector(
                        indices=sparse_indices,
                        values=sparse_values
                    )
                },
                payload={
                    'string-key': item.get('StringKey', ''),
                    'text': item['Text']
                }
            ))

        # Upload to Qdrant
        client.upsert(
            collection_name=COLLECTION_NAME,
            points=points
        )

    # Get final count
    collection_info = client.get_collection(COLLECTION_NAME)
    print(f"Ingestion complete. Total documents: {collection_info.points_count}")


def main():
    print("=" * 60)
    print("Text Resource Vector Database Ingestion")
    print("=" * 60)

    # Check if data file exists
    if not os.path.exists(DATA_FILE):
        print(f"Error: Data file not found: {DATA_FILE}")
        sys.exit(1)

    # Load data
    data = load_data(DATA_FILE)

    # Load model
    model = get_model()

    # Get Qdrant client
    client = get_qdrant_client()

    # Create collection
    create_collection(client)

    # Ingest data
    ingest_data(data, client, model)

    print("\n" + "=" * 60)
    print("Ingestion completed successfully!")
    print(f"Qdrant dashboard: {QDRANT_URL}/dashboard#/collections/{COLLECTION_NAME}")
    print("=" * 60)


if __name__ == '__main__':
    main()
