# Patches Applied

Summary of patches applied to the IdentityServer8 codebase, with rationale.

---

## .NET 10 Upgrade (2026-07-24)

Upgraded from .NET 9 (`net9.0`, SDK 9.0.305) to .NET 10 (`net10.0`, SDK 10.0.302).
Reference: [omarbaruzzo/IdentityServer10](https://github.com/omarbaruzzo/IdentityServer10)

### Build Infrastructure

| File | Change | Reason |
|------|--------|--------|
| `global.json` | SDK `9.0.305` → `10.0.302`, added `rollForward: latestFeature` | Target .NET 10; allow CI runners flexibility in SDK selection |
| `Directory.Build.props` | TFM `net9.0` → `net10.0`, version → `10.0.0` | Target .NET 10 runtime and libraries |
| `Directory.Build.targets` | Removed ILLink `_FixKnownILLinkPack` workaround | Workaround for [dotnet/linker#3175](https://github.com/dotnet/linker/issues/3175) is no longer needed on .NET 10 |
| `version.json` | Version `8.1.0-preview-net9` → `10.0.0` | Align Nerdbank.GitVersioning with new major version |
| `docs/conf.py` | Version `8.0.0` / `8.0.4` → `10.0.0` | Keep Sphinx docs version in sync |
| `samples/Directory.Build.props` | IdentityServerVersion → `10.0.0` | Align sample projects |

### Package Versions (`Directory.Packages.props`)

| Change | Reason |
|--------|--------|
| `AspnetVersion` / `AspnetMinorVersion` → `10.0.10` | ASP.NET Core 10 |
| `MicrosoftExtensionsVersion` → `10.0.10` | Microsoft.Extensions.* 10 |
| New `EfVersion` variable → `10.0.10` | EF Core packages now use a dedicated variable instead of sharing `AspnetMinorVersion` |
| `Microsoft.Data.SqlClient` `5.1.6` → `6.1.1` | Required by EF Core SqlServer 10.0.10 (transitive dependency) |
| `Azure.Identity` `1.11.4` → `1.14.2` | Required by Microsoft.Data.SqlClient 6.1.1 (transitive dependency) |
| `Microsoft.IdentityModel.*` `8.14.0` → `8.19.2` | Required by ASP.NET Core OpenIdConnect 10.0.10 (transitive dependency) |
| `System.IdentityModel.Tokens.Jwt` `8.14.0` → `8.19.2` | Align with Microsoft.IdentityModel.* |
| `Serilog.Sinks.Console` `6.0.0` → `6.1.1` | Required by Serilog.AspNetCore 10.0.0 (transitive dependency) |
| `Serilog.AspNetCore` decoupled from `$(AspnetVersion)`, now uses `$(SerilogVersion)` = `10.0.0` | Third-party packages don't follow Microsoft's versioning |
| `Npgsql.EntityFrameworkCore.PostgreSQL` → `10.0.3` | EF Core 10 compatible PostgreSQL provider |
| `Pomelo.EntityFrameworkCore.MySql` `7.0.0` → `9.0.0` | Latest version compatible with EF Core 10 |
| `Swashbuckle.AspNetCore` `6.5.0` → `10.2.3` | .NET 10 compatible Swagger/OpenAPI |
| HealthChecks packages decoupled from `$(AspnetVersion)`, pinned to `$(HealthChecksVersion)` = `9.0.0` | No 10.x versions exist; third-party packages don't follow Microsoft's versioning |
| `Microsoft.CodeAnalysis.NetAnalyzers` → `10.0.302` | .NET 10 analyzers |
| `Microsoft.SourceLink.GitHub` → `10.0.301` | .NET 10 SourceLink |
| `Nerdbank.GitVersioning` `3.6.133` → `3.10.91` | Latest stable |
| `Microsoft.NET.Test.Sdk` `18.0.0` → `18.8.1` | Latest test SDK |
| `coverlet.collector` `6.0.4` → `10.0.1` | Latest coverage collector |
| `FluentAssertions` `8.7.1` → `8.10.0` | Latest stable |
| `Microsoft.AspNetCore.Authentication.Google` `3.1.5` → `$(AspnetMinorVersion)` | Was pinned to ancient 3.x version |

### GitHub Actions (5 workflows)

| File | Change | Reason |
|------|--------|--------|
| `develop.yml`, `master.yml`, `pre-release.yml`, `release.yml` | `dotnet-version: 9.0.x` → `10.0.x`, `actions/checkout` v3→v4, `actions/setup-dotnet` v3→v4 | Target .NET 10 SDK in CI |
| `codeql.yml` | Added `setup-dotnet` step with `dotnet-version: 10.0.x` | CodeQL workflow was missing SDK setup; relied on runner default which won't have .NET 10 |

---

## Security Patches (2026-07-24)

### Vulnerable Dependency Updates

| Package | From | To | Vulnerability | Severity |
|---------|------|----|---------------|----------|
| `Google.Protobuf` | 3.23.4 | 3.25.5 | [CVE-2024-7254](https://nvd.nist.gov/vuln/detail/CVE-2024-7254) — DoS via deeply nested groups | Medium (C# impact debated; primarily affects Java) |
| `HtmlAgilityPack` | 1.11.40 | 1.12.4 | Vulnerable transitive dependency on `System.Text.RegularExpressions 4.3.0` | Medium |
| `jQuery.validation` | 1.19.5 | 1.21.0 | XSS vulnerabilities in versions < 1.20.0 | High |

Note: These packages are primarily consumed by sample projects, not the core libraries.

### CORS Origin Matching Bug Fix

**File:** `src/EntityFramework/src/Services/CorsPolicyService.cs`

**Before:** `where o.Origin == origin` (case-sensitive comparison)
**After:** `where o.Origin.ToLower() == origin` (case-insensitive)

**Reason:** The `origin` parameter is already lowered via `ToLowerInvariant()` at the start of the method, but the database column value was compared case-sensitively. Per [RFC 6454 §6.1](https://www.rfc-editor.org/rfc/rfc6454#section-6.1), origin comparison should be case-insensitive for the scheme and host components. This fix aligns the EF-backed CORS policy service with the in-memory implementation's behavior.

---

## AutoMapper Removal (2026-07-24)

Replaced AutoMapper 13.0.1 with hand-written mapping code to resolve both a high-severity DoS vulnerability ([GHSA-rvv3-g6hj-g44x](https://github.com/advisories/GHSA-rvv3-g6hj-g44x)) and an RPL/commercial license conflict (AutoMapper 15+ is incompatible with this project's Apache-2.0 license).

### Scope

AutoMapper was used only in the `EntityFramework.Storage` project for mapping between EF entities and domain models. The public API (`ToModel()`, `ToEntity()`, `UpdateEntity()` extension methods) is unchanged — no downstream callers were affected.

### Files Changed

| File | Change |
|------|--------|
| `src/EntityFramework.Storage/src/Mappers/AllowedSigningAlgorithmsConverter.cs` | Removed `IValueConverter<,>` interfaces; now a plain static utility class |
| `src/EntityFramework.Storage/src/Mappers/PersistedGrantMappers.cs` | Replaced AutoMapper with manual property mapping |
| `src/EntityFramework.Storage/src/Mappers/IdentityResourceMappers.cs` | Replaced AutoMapper with manual property mapping |
| `src/EntityFramework.Storage/src/Mappers/ScopeMappers.cs` | Replaced AutoMapper with manual property mapping |
| `src/EntityFramework.Storage/src/Mappers/ApiResourceMappers.cs` | Replaced AutoMapper with manual property mapping (handles `Secrets` ↔ `ApiSecrets` name mismatch) |
| `src/EntityFramework.Storage/src/Mappers/ClientMappers.cs` | Replaced AutoMapper with manual property mapping (handles enum↔int casts, conditional null mapping, 9 nested collection types) |
| `src/EntityFramework.Storage/src/GlobalUsings.cs` | Removed `global using AutoMapper;` |
| `src/EntityFramework.Storage/src/IdentityServer8.EntityFramework.Storage.csproj` | Removed `PackageReference Include="AutoMapper"` |
| `Directory.Packages.props` | Removed `PackageVersion Include="AutoMapper"` |

### Files Deleted

| File | Reason |
|------|--------|
| `src/EntityFramework.Storage/src/Mappers/PersistedGrantMapperProfile.cs` | No longer needed — mapping logic moved into mapper extension methods |
| `src/EntityFramework.Storage/src/Mappers/IdentityResourceMapperProfile.cs` | Same |
| `src/EntityFramework.Storage/src/Mappers/ScopeMapperProfile.cs` | Same |
| `src/EntityFramework.Storage/src/Mappers/ApiResourceMapperProfile.cs` | Same |
| `src/EntityFramework.Storage/src/Mappers/ClientMapperProfile.cs` | Same |

### Tests Updated

Removed 5 `AutomapperConfigurationIsValid()` test methods (one per mapper test class). All remaining 11 mapper tests pass unchanged.

---

## Known Remaining Vulnerabilities

| Package | Version | Severity | Advisory | Status |
|---------|---------|----------|----------|--------|
| `SQLitePCLRaw.lib.e_sqlite3` | 2.1.11 | High | [GHSA-2m69-gcr7-jv3q](https://github.com/advisories/GHSA-2m69-gcr7-jv3q) | Transitive dependency in test projects only (via EF Core Sqlite) |
