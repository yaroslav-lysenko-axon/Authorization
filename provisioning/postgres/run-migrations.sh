#!/bin/bash

# Load configuration variables
source ./env.sh

# Build migrations project and run it
docker build -t "$MIGRATIONS_IMAGE_NAME" -f ./Dockerfile ../..
docker run \
    -e Persistence__Host="$POSTGRES_HOST" \
    -e Persistence__Port="$POSTGRES_PORT" \
    -e Persistence__User="$POSTGRES_USER" \
    -e Persistence__Password="$POSTGRES_PASSWORD" \
    -e Persistence__Database="$POSTGRES_DB" \
    -e Persistence__Schema="$POSTGRES_SCHEMA" \
    --net="host" --rm "$MIGRATIONS_IMAGE_NAME"
