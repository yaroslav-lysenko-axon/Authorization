# DB provisioning

### Overview
Provisioning consists of several parts:
* **env.sh** - not really a script, but a configurable variables list
* **run-daemon.sh** - launches postgres container
* **run-migrations.sh** - launches Authorization.Migrations
* **Dockerfile** - needed for building dotnet container with migrations, used by run-migrations.sh

### What should I run?
1. Ensure that configuration inside *env.sh* is valid for you
2. Run *run-daemon.sh*
3. Run *run-migrations.sh*

### How can I check that it worked?
1. Execute `docker ps` and check that there is a `reference-postgres` container up and running.
2. Check that your console output contains `Migration finished!` in stdout.
3. Connect to a database using **DBeaver** or alternatives (make sure you use same parameters as in `env.sh`).
4. Select everything from `VersionInfo` table in `public` schema and check that it contains all migrations.
