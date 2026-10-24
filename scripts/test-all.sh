#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
command -v dotnet >/dev/null || { echo 'Install .NET 10 SDK first: https://dotnet.microsoft.com/download/dotnet/10.0'; exit 1; }
dotnet --info
dotnet restore
dotnet build --configuration Release --no-restore
PORT=5080
(dotnet run --no-build --configuration Release --urls "http://localhost:$PORT" > /tmp/eventease-server.log 2>&1) &
PID=$!
trap 'kill "$PID" 2>/dev/null || true' EXIT
for i in $(seq 1 40); do if curl -fsS "http://localhost:$PORT/" >/dev/null 2>&1; then break; fi; sleep 1; done
BASE_URL="http://localhost:$PORT" bash scripts/smoke-test.sh
