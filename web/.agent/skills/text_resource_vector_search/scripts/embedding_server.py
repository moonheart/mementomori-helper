#!/usr/bin/env python3
"""
Embedding Server - A standalone HTTP server for generating embeddings.
Uses BGE-M3 via FlagEmbedding for dense and sparse embeddings.

Start this server once, then use search.py to query.
"""

import argparse
import json
import sys
from typing import Optional

try:
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')
    if hasattr(sys.stderr, 'reconfigure'):
        sys.stderr.reconfigure(encoding='utf-8')
except Exception:
    pass

from FlagEmbedding import BGEM3FlagModel
import torch

# Use FastAPI for async support
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import uvicorn


# Configuration
MODEL_NAME = "BAAI/bge-m3"
DEFAULT_HOST = "127.0.0.1"
DEFAULT_PORT = 8765

# Global model
model: Optional[BGEM3FlagModel] = None


class EmbedRequest(BaseModel):
    """Request model for embedding."""
    texts: list[str]
    return_dense: bool = True
    return_sparse: bool = True
    return_colbert_vecs: bool = False


class EmbedResponse(BaseModel):
    """Response model for embedding."""
    dense_vecs: Optional[list[list[float]]] = None
    sparse_indices: Optional[list[list[int]]] = None
    sparse_values: Optional[list[list[float]]] = None


app = FastAPI(title="BGE-M3 Embedding Server")


def load_model() -> BGEM3FlagModel:
    """Load the BGE-M3 model."""
    global model
    if model is None:
        print(f"Loading model {MODEL_NAME}...", file=sys.stderr)
        device = 'cuda' if torch.cuda.is_available() else 'cpu'
        print(f"Using device: {device}", file=sys.stderr)
        model = BGEM3FlagModel(
            MODEL_NAME,
            use_fp16=True,
            device=device
        )
        print("Model loaded successfully!", file=sys.stderr)
    return model


@app.on_event("startup")
async def startup_event():
    """Load model on startup."""
    load_model()


@app.get("/health")
async def health_check():
    """Health check endpoint."""
    return {"status": "healthy", "model": MODEL_NAME}


@app.post("/embed", response_model=EmbedResponse)
async def embed(request: EmbedRequest):
    """Generate embeddings for texts."""
    if model is None:
        raise HTTPException(status_code=503, detail="Model not loaded")

    try:
        embeddings = model.encode(
            request.texts,
            return_dense=request.return_dense,
            return_sparse=request.return_sparse,
            return_colbert_vecs=request.return_colbert_vecs
        )

        response = EmbedResponse()

        if request.return_dense and 'dense_vecs' in embeddings:
            response.dense_vecs = [vec.tolist() for vec in embeddings['dense_vecs']]

        if request.return_sparse and 'lexical_weights' in embeddings:
            sparse_indices = []
            sparse_values = []
            for sparse_vec in embeddings['lexical_weights']:
                indices = list(sparse_vec.keys())
                values = [float(v) for v in sparse_vec.values()]
                sparse_indices.append(indices)
                sparse_values.append(values)
            response.sparse_indices = sparse_indices
            response.sparse_values = sparse_values

        return response

    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


def main():
    parser = argparse.ArgumentParser(
        description='Start the BGE-M3 embedding server.',
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python embedding_server.py
  python embedding_server.py --port 9000
  python embedding_server.py --host 0.0.0.0 --port 8765
        """
    )
    parser.add_argument(
        '--host',
        default=DEFAULT_HOST,
        help=f'Host to bind to (default: {DEFAULT_HOST})'
    )
    parser.add_argument(
        '--port',
        type=int,
        default=DEFAULT_PORT,
        help=f'Port to bind to (default: {DEFAULT_PORT})'
    )

    args = parser.parse_args()

    print(f"\nStarting BGE-M3 Embedding Server on {args.host}:{args.port}")
    print("Press Ctrl+C to stop\n")

    uvicorn.run(app, host=args.host, port=args.port, log_level="warning")


if __name__ == '__main__':
    main()
