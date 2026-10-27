import sys
import json
import os

def main():
    # Attempt to fix encoding issues for Windows terminals
    try:
        if hasattr(sys.stdout, 'reconfigure'):
            sys.stdout.reconfigure(encoding='utf-8')
        if hasattr(sys.stderr, 'reconfigure'):
            sys.stderr.reconfigure(encoding='utf-8')
    except Exception:
        pass

    if len(sys.argv) < 2 or (len(sys.argv) == 2 and sys.argv[1] in ("-a", "--all", "-e", "--exact", "-h", "--help")):
        print("\n❌ Usage: python search.py \"<query1>\" [\"<query2>\" ...] [-a|--all] [-e|--exact]")
        print("Example: python search.py \"累计投票数\" \"进行中\" --all")
        print("         python search.py \"领取\" -e")
        sys.exit(1)

    args = sys.argv[1:]
    
    print_all = False
    exact_match = False

    if "-a" in args or "--all" in args:
        print_all = True
        args = [arg for arg in args if arg not in ("-a", "--all")]

    if "-e" in args or "--exact" in args:
        exact_match = True
        args = [arg for arg in args if arg not in ("-e", "--exact")]

    queries = [q.lower() for q in args]
    file_path = "E:/Git_Github/MementoMoriData/Master/TextResourceZhCnMB.json"

    if not os.path.exists(file_path):
        print(f"\n❌ Error: The file {file_path} could not be found.")
        sys.exit(1)

    try:
        # Use utf-8-sig to automatically handle BOM if present
        with open(file_path, 'r', encoding='utf-8-sig') as f:
            data = json.load(f)
        
        results = []
        for item in data:
            if exact_match:
                in_text = ('Text' in item and item['Text'] is not None and any(q == item['Text'].lower() for q in queries))
                in_key = ('StringKey' in item and item['StringKey'] is not None and any(q == item['StringKey'].lower() for q in queries))
                in_memo = ('Memo' in item and item['Memo'] is not None and any(q == item['Memo'].lower() for q in queries))
            else:
                in_text = ('Text' in item and item['Text'] is not None and any(q in item['Text'].lower() for q in queries))
                in_key = ('StringKey' in item and item['StringKey'] is not None and any(q in item['StringKey'].lower() for q in queries))
                in_memo = ('Memo' in item and item['Memo'] is not None and any(q in item['Memo'].lower() for q in queries))
            
            if in_text or in_key or in_memo:
                # Clean up carriage returns/newlines for terminal display
                s_key = item.get("StringKey", "")
                s_text = item.get("Text", "")
                if s_key: s_key = s_key.replace('\r', '').replace('\n', ' ')
                if s_text: s_text = s_text.replace('\r', '').replace('\n', ' ')
                
                results.append({
                    "StringKey": s_key,
                    "Text": s_text
                })

        match_type = "Exact match" if exact_match else "Partial match"
        print(f"\n🔍 Searching for: {', '.join(args)} ({match_type})...\n")

        if not results:
            print("No results found.")
        else:
            print(f"Found {len(results)} result(s):")
            limit = len(results) if print_all else min(50, len(results))
            print(json.dumps(results[:limit], indent=2, ensure_ascii=False))
            if len(results) > limit:
                print(f"\n... and {len(results) - limit} more results. (Use -a or --all to see all)")
                
    except Exception as e:
        print(f"\n❌ Error reading or parsing file: {str(e)}")
        sys.exit(1)

if __name__ == '__main__':
    main()
