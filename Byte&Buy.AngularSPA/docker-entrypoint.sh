#!/bin/sh
cat > /app/dist/config.json <<EOF
{
  "apiBaseUrl": "${API_BASE_URL}",
  "staticImagesBaseUrl": "${STATIC_IMAGES_BASE_URL}"
}
EOF
exec serve -s dist -l 4200