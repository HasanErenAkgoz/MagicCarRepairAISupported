# EF Migration Tooling (NET 10)

## Pinned tool contract

The projects use EF Core 9.0.x while targeting `net10.0`. Do not invoke an arbitrary globally installed `dotnet-ef`: its design-time metadata protocol can be incompatible and surface as a `JsonException`. Use a local, pinned EF CLI that matches the EF package major/minor version.

From the repository root, restore the committed local tool manifest. The design-time
factory requires no application settings or secrets: it uses a deliberately non-live
local PostgreSQL connection only to obtain provider metadata. `migrations add` does
not open that connection. Set `ConnectionStrings__DefaultConnection` only when an
explicit database target is required for commands such as `database update`.

```bash
export DOTNET_CLI_HOME=/private/tmp/magiccarrepair-dotnet-cli
export DOTNET_ROOT=/private/tmp/magiccarrepair-dotnet10
export DOTNET_ROOT_ARM64=/private/tmp/magiccarrepair-dotnet10
cd Core.Packages.WebAPI
/private/tmp/magiccarrepair-dotnet10/dotnet tool restore
/private/tmp/magiccarrepair-dotnet10/dotnet tool run dotnet-ef dbcontext info \
  --project ../Core.Packages.Persistence/MagicCarRepairAISupported.Persistence.csproj \
  --startup-project MagicCarRepairAISupported.WebAPI.csproj \
  --context BaseDbContext
/private/tmp/magiccarrepair-dotnet10/dotnet tool run dotnet-ef migrations add AddWorkOrderParticipants \
  --project ../Core.Packages.Persistence/MagicCarRepairAISupported.Persistence.csproj \
  --startup-project MagicCarRepairAISupported.WebAPI.csproj \
  --context BaseDbContext \
  --output-dir ../Core.Packages.Persistence/Migrations
```

Do not use a machine-global tool or commit a tool cache; the versioned manifest is the only tool contract. Before committing, inspect both generated migration and `BaseDbContextModelSnapshot.cs` (if present) for only the intended schema change.

## CI verification

CI restores the pinned local tool and runs `dotnet-ef dbcontext info` before tests. This preflight catches factory discovery/configuration regressions without touching a database. CI also starts the API against PostgreSQL and asserts the expected migration ID appears in `__EFMigrationsHistory`. The API module-test job follows this runtime pattern. Add a migration-specific assertion when `AddWorkOrderParticipants` is introduced; do not run `database update` against production from CI.

## Web API participant authorization test fixture

Reuse `MagicCarRepairWebApplicationFactory` and `JwtTestTokenFactory`:

1. Start the factory with the CI PostgreSQL connection string and its in-memory JWT configuration.
2. Seed a work order and its participants for client `1`; use one customer participant and one unrelated customer/client.
3. Call the new participant/media endpoint with `CreateAuthenticatedClient(userType: 4, clientId: 1)` and the matching user-id claim; expect success.
4. Repeat with a different customer or tenant; expect 403/404 before storage access. Verify the storage mock/file reader was not called.
5. Keep ShopStaff/SystemAdmin cases explicit, rather than widening the customer policy.

The current factory intentionally does not relax production authorization or health checks; tests require a live disposable PostgreSQL instance, as demonstrated by the 23/23 WebAPI test run.

Use the same disposable fixture values as CI:

```bash
docker run --rm -d --name magiccarrepair-webapi-test-db \
  -e POSTGRES_USER=magiccar -e POSTGRES_PASSWORD=magiccar_dev \
  -e POSTGRES_DB=MagicCarRepairDb -p 55432:5432 postgres:16-alpine
ConnectionStrings__DefaultConnection='Host=localhost;Port=55432;Database=MagicCarRepairDb;Username=magiccar;Password=magiccar_dev' \
TokenOptions__SecurityKey='TestSigningKeyForIntegrationTests_MustBe32Chars!!' \
TokenOptions__Issuer='MagicCarRepair' TokenOptions__Audience='MagicCarRepair.API' \
dotnet test Core.Packages.WebAPI.Tests/MagicCarRepairAISupported.WebAPI.Tests.csproj
docker stop magiccarrepair-webapi-test-db
```
