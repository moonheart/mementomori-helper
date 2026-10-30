#!/usr/bin/env bash
# Standalone builds (no Docker, no .NET install needed) -> release/*.zip / *.tar.gz
# Usage: [VERSION=v1.2.3] ./build-release.sh [win-x64 linux-x64 ...]
set -euo pipefail
cd "$(dirname "$0")"
DOTNET=${DOTNET:-$( [ -x ~/.dotnet/dotnet ] && echo ~/.dotnet/dotnet || echo dotnet )}
PNPM="npx -y pnpm@9.15.0"
RIDS=${*:-win-x64 linux-x64}
PORT=5290

(cd web && $PNPM install --frozen-lockfile && $PNPM exec tsc -b && $PNPM exec vite build)

rm -rf release && mkdir release
for rid in $RIDS; do
  out=release/MementoMori-$rid
  "$DOTNET" publish MementoMori.Api -c Release -r "$rid" --self-contained \
    -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:DebugType=none ${VERSION:+-p:Version=${VERSION#v}} -o "$out"
  cp -r web/dist "$out/wwwroot"

  if [[ $rid == win-* ]]; then
    sed 's/$/\r/' > "$out/start.bat" <<EOF
@echo off
cd /d "%~dp0"
set ASPNETCORE_URLS=http://localhost:$PORT
set Game__AutoRequestDelay=500
echo First start downloads game data, can take a few minutes. Browser opens when ready.
echo Close this window to stop.
start "" /b powershell -NoProfile -WindowStyle Hidden -Command "do{Start-Sleep 2;try{\$ok=(Invoke-WebRequest -UseBasicParsing http://localhost:$PORT -TimeoutSec 5).StatusCode -eq 200}catch{\$ok=\$false}}until(\$ok); Start-Process http://localhost:$PORT"
MementoMori.Api.exe
pause
EOF
    (cd release && python3 -m zipfile -c "MementoMori-$rid.zip" "MementoMori-$rid")
  else
    cat > "$out/start.sh" <<EOF
#!/usr/bin/env bash
cd "\$(dirname "\$0")"
export ASPNETCORE_URLS=http://localhost:$PORT Game__AutoRequestDelay=500
echo "First start downloads game data, can take a few minutes. Browser opens when ready."
(until curl -sf -o /dev/null http://localhost:$PORT; do sleep 2; done; xdg-open http://localhost:$PORT) &
exec ./MementoMori.Api
EOF
    chmod +x "$out/start.sh"
    tar -C release -czf "release/MementoMori-$rid.tar.gz" "MementoMori-$rid"
  fi
done
ls -lh release
