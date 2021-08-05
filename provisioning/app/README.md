# App provisioning

### Overview
Provisioning consists of several parts:
* **env.sh** - not really a script, but an application configuration
* **build.sh** - builds a docker image with an application
* **run.sh** - launches an application container
* **Dockerfile** - needed for building container, used by build.sh

### What should I run?
1. Ensure that configuration inside *env.sh* is valid for you
2. Run *build.sh*
3. Run *run.sh*

### How can I check that it worked?
1. Execute `docker ps` and check that there is a `dotnet-reference-authorization` container up and running.
2. Use `curl` or browser to `GET http://localhost:16420/test/ping` (URL may differ if you've changed a configuration)
