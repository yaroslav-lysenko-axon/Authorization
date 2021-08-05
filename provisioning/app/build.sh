#!/bin/bash

# Load configuration variables
source ./env.sh

# Build
docker build -t "$APPLICATION_IMAGE_NAME" -f ./Dockerfile ../..
