#!/bin/bash

# Load configuration variables
source ./env.sh

# Create postgres data folder if it doesn't exist
mkdir -p "$POSTGRES_DATA_DIR";

# Start existing container or create a new one
docker start "$POSTGRES_CONTAINER_NAME" 2>/dev/null || \
docker run \
    -e POSTGRES_USER="$POSTGRES_USER" \
    -e POSTGRES_PASSWORD="$POSTGRES_PASSWORD" \
    -e POSTGRES_DB="$POSTGRES_DB" \
    -e POSTGRES_SCHEMA="$POSTGRES_SCHEMA" \
    -p "$POSTGRES_PORT":5432 \
    -v "$POSTGRES_DATA_DIR":/var/lib/postgresql/data \
    --user "$(id -u):$(id -g)" \
    -v /etc/passwd:/etc/passwd:ro \
    --restart unless-stopped \
    -d --name "$POSTGRES_CONTAINER_NAME" "$POSTGRES_IMAGE_NAME"
