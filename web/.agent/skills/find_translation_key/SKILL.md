---
name: Find Translation Key
description: Find translation StringKeys or texts in MementoMori TextResourceZhCnMB.json.
---

# Find Translation Key

Use this skill when you need to find the `StringKey` for a specific localized text (e.g., Chinese translated strings) or look up what text corresponds to a `StringKey`. This is extremely useful for localizing the frontend, as replacing hardcoded strings with proper keys ensures correctness.

## How to use

Run the included Python script to search the localization file. The script searches the `Text`, `StringKey`, and `Memo` properties, and outputs the `StringKey` and `Text` for matches.

By default, the script performs a partial string match (e.g. searching "领取" will return "全部领取", "未领取", etc).
To find exact matches only, use the `-e` or `--exact` flag. This is very useful when trying to find the Key for a short word without returning hundreds of results.

You can search for multiple terms at the same time by providing multiple arguments. The search will return results that match *any* of the given terms.

By default, the script outputs a **maximum of 50 results** to avoid flooding the terminal. If you want to see all results, append the `-a` or `--all` flag to your command.

### Command

```bash
python .agent/skills/find_translation_key/scripts/search.py "Query1" ["Query2" ...] [-e|--exact] [-a|--all]
```

### Examples

**Search for a single text:**
```bash
python .agent/skills/find_translation_key/scripts/search.py "领取" 
```

**Search for EXACT match texts (recommended for common words):**
```bash
python .agent/skills/find_translation_key/scripts/search.py "领取" -e
```

**Search for multiple texts at once:**
```bash
python .agent/skills/find_translation_key/scripts/search.py "累计投票数" "受取" "进行中" 
```

**Search and show all results (bypassing the 50 result limit):**
```bash
python .agent/skills/find_translation_key/scripts/search.py "主线冒险" --all
```

**Search by StringKey:**
```bash
python .agent/skills/find_translation_key/scripts/search.py "PopularityVoteRewardTotalVoteCountFormat"
```

The script will output the matching JSON entries. If what you're looking for is not found, try using a shorter query.
