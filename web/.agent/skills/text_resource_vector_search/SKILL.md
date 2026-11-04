---
name: Text Resource Vector Search
description: Search game text resources using vector similarity with bge-m3 and Qdrant.
---

# Text Resource Vector Search

Use this skill to search game text resources using semantic similarity. Unlike the `find_translation_key` skill which uses exact keyword matching, this skill uses vector embeddings to find semantically similar texts.

This is useful when:
- You want to find texts with similar meanings, not just exact matches
- You have a description of what text you're looking for but don't know the exact wording
- You want to discover related text entries

## Prerequisites

### 1. Install Dependencies

```bash
pip install -r .agent/skills/text_resource_vector_search/scripts/requirements.txt
```

### 2. Start the Embedding Server

**IMPORTANT**: Before searching, you must start the embedding server. This keeps the model loaded in memory for fast searches:

```bash
python .agent/skills/text_resource_vector_search/scripts/embedding_server.py
```

The server will:
- Load BGE-M3 model once on startup
- Listen on `http://127.0.0.1:8765` by default
- Serve embedding requests for all subsequent searches

**Options:**
| Option | Description |
|--------|-------------|
| `--host HOST` | Host to bind to (default: 127.0.0.1) |
| `--port PORT` | Port to bind to (default: 8765) |

### 3. Ensure Qdrant is Running

- **Qdrant**: http://192.168.50.50:6333

### 4. Build the Vector Database (First Time Only)

Before searching, you need to ingest the text resources into Qdrant:

```bash
python .agent/skills/text_resource_vector_search/scripts/ingest.py
```

This will:
- Load `TextResourceZhCnMB.json`
- Generate dense and sparse embeddings using `BAAI/bge-m3`
- Store everything in Qdrant collection `mmmr-zhcn`

**Note**: The first run will download the embedding model (~2.2GB) and may take several minutes to complete.

## How to Use

### Step 1: Start the Embedding Server (Once per session)

```bash
python .agent/skills/text_resource_vector_search/scripts/embedding_server.py
```

### Step 2: Search

```bash
python .agent/skills/text_resource_vector_search/scripts/search.py "查询文本"
```

### Options

| Option | Description |
|--------|-------------|
| `-k, --top-k N` | Number of results to return (default: 10) |
| `-j, --json` | Output results as JSON format |
| `--server-url URL` | Embedding server URL (default: http://127.0.0.1:8765) |

### Examples

**Search for similar texts:**
```bash
python .agent/skills/text_resource_vector_search/scripts/search.py "战斗力"
```

**Get more results:**
```bash
python .agent/skills/text_resource_vector_search/scripts/search.py "领取奖励" -k 20
```

**JSON output for programmatic use:**
```bash
python .agent/skills/text_resource_vector_search/scripts/search.py "确认" -j
```

## Output Format

Each result includes:
- **StringKey**: The game's internal key for this text
- **Text**: The Chinese text content
- **Similarity**: A score from 0 to 1 (higher = more similar)

## Architecture

```
┌─────────────────┐     HTTP      ┌────────────────────┐
│   search.py     │ ────────────> │  embedding_server  │
│   (client)      │ <──────────── │  (BGE-M3 model)    │
└────────┬────────┘               └────────────────────┘
         │
         │ gRPC/HTTP
         ▼
┌─────────────────┐
│     Qdrant      │
│ (vector store)  │
└─────────────────┘
```

1. **Embedding Server**: Standalone FastAPI server that loads BGE-M3 once and serves embedding requests
2. **Search Client**: Lightweight script that calls the embedding server and queries Qdrant
3. **Vector Database**: Qdrant stores pre-computed embeddings for fast similarity search

## Configuration

- **Embedding Server URL**: http://127.0.0.1:8765 (configurable via `--server-url`)
- **Qdrant URL**: http://192.168.50.50:6333
- **Collection Name**: `mmmr-zhcn`
- **Embedding Model**: `BAAI/bge-m3`

## Troubleshooting

### "Embedding server not running"
Start the server first:
```bash
python .agent/skills/text_resource_vector_search/scripts/embedding_server.py
```

### "Vector database not found"
Run `ingest.py` first to populate the Qdrant collection.

### "Model loading fails"
Ensure you have:
- Python 3.8+
- Sufficient disk space for the model (~2.2GB)
- Internet connection for first-time model download

### "Qdrant connection failed"
Ensure Qdrant is running at http://192.168.50.50:6333.

## Comparison with find_translation_key

| Feature | find_translation_key | text_resource_vector_search |
|---------|---------------------|---------------------------|
| Search type | Keyword matching | Semantic similarity |
| Speed | Fast | Fast (after server started) |
| Setup | None | Start embedding server once |
| Accuracy | Exact matches only | Understands meaning |
| Use case | Know exact text | Find related/similar texts |
