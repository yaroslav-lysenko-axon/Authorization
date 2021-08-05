#!/bin/bash

# Load configuration variables
source ./env.sh

# Overwrite 'localhost' with a gateway IP (that'll be a host IP)
if [ "$PERSISTENCE_HOST" = "localhost" ]; then
   PERSISTENCE_HOST=$(ip route show | awk '/default/ {print $3}')
fi

# Start a container
docker run \
    -e Persistence__Host="$PERSISTENCE_HOST" \
    -e Persistence__Port="$PERSISTENCE_PORT" \
    -e Persistence__User="$PERSISTENCE_USER" \
    -e Persistence__Password="$PERSISTENCE_PASSWORD" \
    -e Persistence__Database="$PERSISTENCE_DB" \
    -e Persistence__Schema="$PERSISTENCE_SCHEMA" \
    -p "$APPLICATION_PORT":80 \
    --restart unless-stopped \
    -d --name "$APPLICATION_CONTAINER_NAME" "$APPLICATION_IMAGE_NAME"
