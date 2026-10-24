#!/usr/bin/env bash
set -euo pipefail

BASE="${BASE_URL:-http://localhost:5080}"

for path in / /events/1 /register/1 /attendance /session /events/999 /does-not-exist; do

  code=$(curl -L -s -o /tmp/eventease-smoke-body \
    -w '%{http_code}' "$BASE$path")

  expected="200"

  if [ "$path" = "/does-not-exist" ]; then
    expected="404"
  fi

  if [ "$code" != "$expected" ]; then
    echo "FAIL $path HTTP $code (expected $expected)"
    exit 1
  fi

  if [ "$expected" = "200" ]; then
    if ! grep -Eqi 'EventEase|Event|not found|session|Attendance|registration' \
      /tmp/eventease-smoke-body; then
      echo "FAIL unexpected body $path"
      exit 1
    fi
  fi

  echo "PASS $path ($code)"

done

echo "PASS HTTP smoke checks"
