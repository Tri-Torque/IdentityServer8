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

## NuGet Audit & Package Updates (2026-10-26)

Audited every project in `src/` and `samples/` against the nuget.org vulnerability database. Fixed all vulnerabilities, updated outdated and deprecated packages and cleaned up central package management. Still targets `net10.0`.

### Vulnerability Fixes

| Package | From | To | Advisory | Severity | Fix |
|---------|------|----|----------|----------|-----|
| `System.Security.Cryptography.Pkcs` | 9.0.13 / 10.0.12 | 10.0.14 | [GHSA-846f-f764-76mx](https://github.com/advisories/GHSA-846f-f764-76mx), [GHSA-2fmv-rv2c-59x7](https://github.com/advisories/GHSA-2fmv-rv2c-59x7) | High + Moderate | Central pin in a new `Security pins` ItemGroup. It's a transitive dependency of `Microsoft.Data.SqlClient` and `Aspire.Hosting.SqlServer`. Even the latest versions (SqlClient 7.1.1, Aspire 13.6.4) still allow a vulnerable version. |
| `Microsoft.Build.Tasks.Git` | 10.0.301 | 10.0.403 | [GHSA-23fw-v26w-5fgq](https://github.com/advisories/GHSA-23fw-v26w-5fgq) | Moderate | `Microsoft.SourceLink.GitHub` → `10.0.403` (all projects) |
| `IdentityServer4` | 3.1.0 | — | Two advisories, no patched version | Moderate | Removed the `samples/KeyManagement` sample, which brought it in through `IdentityServer4.KeyManagement` |
| `Refit` | 7.0.0 | — | [GHSA-3hxg-fxwm-8gf7](https://github.com/advisories/GHSA-3hxg-fxwm-8gf7) | Critical | Declared but never referenced; removed from `Directory.Packages.props` |

### Build Infrastructure

| File | Change | Reason |
|------|--------|--------|
| `global.json` | SDK `10.0.302` → `10.0.403` | Use the same version as NetAnalyzers/SourceLink 10.0.403 |
| `Directory.Build.props` | Removed undefined `Version="$(...)"` attributes from the global NetAnalyzers/SourceLink `PackageReference`s | Versions come from CPM; the properties were never defined |
| `Directory.Build.props` | Added `xUnit1051` to `NoWarn` | New xUnit v3 analyzer suggestion (pass `TestContext.Current.CancellationToken`), 325 occurrences; addressing them is a separate change |
| `src/Storage/build/` | Deleted | Dead project: the `Program.Partial.cs` it compiled was already removed |
| `samples/KeyManagement/` | Deleted (with its `.gitignore` entries) | Depended on vulnerable `IdentityServer4` 3.1.0 |

### Package Versions (`Directory.Packages.props`)

| Change | Reason |
|--------|--------|
| `AspnetVersion`, `AspnetMinorVersion`, `MicrosoftExtensionsVersion`, `EfVersion` → `10.0.14` | Latest .NET 10 patches. Also fixes the NU1109 restore error in `Quickstarts/6_AspNetIdentity`, caused by the EF 10.0.10 / ASP.NET 10.0.12 mismatch. |
| `Microsoft.Extensions.Http.Resilience` `10.1.0` → `10.11.0`, `Microsoft.Extensions.ServiceDiscovery` `10.8.0` → `10.11.0` | Latest stable |
| `OpenTelemetry.*` `1.16.0` / `1.17.0` → `1.20.1` | Latest stable; aligned all five packages |
| `Microsoft.IdentityModel.*`, `System.IdentityModel.Tokens.Jwt` `8.19.2` → `8.23.0` | Latest stable |
| `Microsoft.Data.SqlClient` (transitive pin) `7.0.2` → `7.1.1` | Latest stable |
| `SQLitePCLRaw.*` `2.1.12` → `2.1.14` | Required by EF Core Sqlite 10.0.14 (≥ 2.1.13), otherwise NU1109 |
| `GrpcVersion` `2.83.0` → `2.84.0`, `Google.Protobuf` `3.36.1` → `3.36.2` | Latest stable (transitive pins for the Aspire AppHost) |
| `Serilog` `4.3.0` → `4.4.0`, `Serilog.Settings.Configuration` → `10.0.1` | Latest stable |
| `Microsoft.CodeAnalysis.NetAnalyzers` / `Microsoft.SourceLink.GitHub` → `10.0.403` | Match SDK 10.0.403 |
| `Microsoft.NET.Test.Sdk` `18.8.1` → `18.11.0`, `coverlet.collector` `10.0.1` → `10.1.0`, `FluentAssertions` `8.10.0` → `8.11.0` | Latest stable |
| `xunit` `2.9.3` → `xunit.v3.mtp-off` `4.0.2`, `xunit.runner.visualstudio` `3.1.5` → `4.0.1` | xUnit v2 is deprecated. The `mtp-off` variant keeps the existing VSTest-based `dotnet test` CI path and Cobertura coverage working (the default `xunit.v3` package switches to Microsoft.Testing.Platform, which the .NET 10 SDK refuses under VSTest). |
| `Microsoft.AspNetCore.Http.Abstractions` / `Mvc.Abstractions` `2.3.0` removed | Replaced by a `FrameworkReference` (see below) |
| 72 dormant `PackageVersion` entries removed, plus `IdentityServer4.KeyManagement`, `Bullseye`, `SimpleExec`, and the `RuntimeVersion` property | Not used by any project and not pinning any transitive package; resolved package graphs were identical before and after removal |

### `IdentityServer8.Security` → FrameworkReference

**File:** `src/Security/IdentityServer8.Security/IdentityServer8.Security.csproj`

Replaced six package references with `<FrameworkReference Include="Microsoft.AspNetCore.App" />`. The removed references were the legacy 2.x `Microsoft.AspNetCore.Http.Abstractions` and `Mvc.Abstractions` packages and four `Microsoft.Extensions.DependencyInjection*` / `Logging*` packages. The packed `.nuspec` now declares the framework reference and has no package dependencies. A standalone console consumer was verified to restore, build and run against the packed package.

### xUnit v3 Test Migration

| File(s) | Change | Reason |
|---------|--------|--------|
| 6 test `.csproj` files | `xunit` → `xunit.v3.mtp-off`, added `<OutputType>Exe</OutputType>` | xUnit v3 test projects are executables |
| `Validation/Secrets/{BasicAuthenticationCredentialParsing,ClientAssertionSecretParsing,FormPostCredentialParsing}.cs` | 24 `async void` tests → `async Task` | `async void` tests are not supported in v3 (xUnit1049) |
| `EntityFramework.Storage/test/IntegrationTests/IntegrationTest.cs`, `EntityFramework/test/.../IntegrationTest.cs` | `(TestDatabaseProviders as IEnumerable<...>)` → `TestDatabaseProviders.Select(row => row.Data)` | v3 `TheoryData<T>` enumerates `TheoryDataRow<T>`, so the cast returned `null` |
| `Stores/ClientStoreTests.cs` | `new TestTimeoutException(timeout)` → `TestTimeoutException.ForTimedOutTest(timeout)` | Constructor removed in v3 |
| `Stores/PersistedGrantStoreTests.cs` | `GetAllAsync_Should_Filter` clears the table before seeding | The test shares an in-memory database per class and depended on execution order, which changed in v3 |

### `IdentityModel` → `Duende.IdentityModel` 8.1.0

`IdentityModel` 7.0.0 is deprecated and unlisted. Its successor `Duende.IdentityModel` (Apache-2.0, no dependencies, same license as this repo) is now used by all of `src`.

| Change | Details |
|--------|---------|
| `IdentityServer8.csproj`, `IdentityServer8.Storage.csproj` | `PackageReference IdentityModel` → `Duende.IdentityModel`. Packed `.nuspec` files depend on `Duende.IdentityModel` 8.1.0. |
| 89 files under `src/` (prod, hosts, tests) | Namespaces `IdentityModel`, `IdentityModel.Client`, `using static IdentityModel.OidcConstants` → `Duende.IdentityModel[.Client]` |
| `Base64Url.Encode/Decode` (removed in Duende) | Replaced by .NET `System.Buffers.Text.Base64Url.EncodeToString/DecodeFromChars`. Invalid input still throws `FormatException`, so error handling is unchanged. |

**Breaking for consumers:** code that relied on the transitive `IdentityModel` namespace must switch to `Duende.IdentityModel`.

### Verification

- Clean restore of all projects: no NU1109, NU1510 or NU1901–NU1904.
- `dotnet list src/IdentityServer8.sln package --vulnerable --include-transitive` reports nothing vulnerable, and so does a scan of src + samples against the nuget.org vulnerability database.
- All sample solutions build. Samples now use `Duende.IdentityModel`, `Duende.IdentityModel.OidcClient`, `Duende.AccessTokenManagement.OpenIdConnect` and `Duende.AspNetCore.Authentication.OAuth2Introspection`; legacy `IdentityModel` 7.0.0 entries and `samples/Clients/old` were removed. The `samples/Quickstarts/Shared/src/*` templates only build through the quickstart solutions (they need `SolutionDir`).
- Tests (CI command): 1135 passed, 1 skipped, the same as before, with 6 Cobertura reports.

---

## Security Audit vs. Upstream (2026-10-26)

Checked the published security advisories and fork commits of the upstream and sibling projects ([alexhiggins732/IdentityServer8](https://github.com/alexhiggins732/IdentityServer8), [omarbaruzzo/IdentityServer10](https://github.com/omarbaruzzo/IdentityServer10), and the IdentityServer4 successor line) against this codebase.

### Already Applied

| Item | Where |
|------|-------|
| [CVE-2024-39694](https://nvd.nist.gov/vuln/detail/CVE-2024-39694): open redirect via control characters in return URLs | `StringsExtensions.IsLocalUrl` (commit 9ccd9e3) |
| `CachingResourceStore` cache key collision between lookups by name and by scope | Commit 9ccd9e3 |
| Package bumps (`Google.Protobuf`, `HtmlAgilityPack`, `jQuery.validation`) and the case-insensitive CORS origin match from IdentityServer10 | See "Security Patches (2026-07-24)" |
| HTML-encoding of front-channel logout iframe URLs | `EndSessionCallbackResult` |
| Protocol claims (`aud`, `iss`, `iat`, `sub`, …) filtered out of profile service claims | `Constants.Filters.ClaimsServiceFilterClaimTypes` |

Advisories for features this codebase doesn't have (pushed authorization requests, DPoP, BFF) don't apply. alexhiggins732/IdentityServer8 has nothing newer than this fork.

### Fixes Applied

| File | Change | Reason |
|------|--------|--------|
| `Validation/Default/PrivateKeyJwtSecretValidator.cs` | Check `TokenValidationResult.IsValid` and fail when it is false; type-check `SecurityToken` | `JsonWebTokenHandler.ValidateTokenAsync` returns validation errors instead of throwing them. Invalid client assertions (bad signature, audience, expiry) were only rejected because of a `NullReferenceException` on the cast, which happened to fail closed. |
| `Validation/Default/TokenValidator.cs` | Same `IsValid` check in `ValidateJwtAsync`; expiry maps to `expired_token`, everything else to `invalid_token` | Same fail-closed-by-accident pattern. The `SecurityTokenExpiredException` catch block was dead code, so expired JWTs returned `invalid_token`. |
| `ResponseHandling/Default/AuthorizeInteractionResponseGenerator.cs`, `Extensions/ValidatedAuthorizeRequestExtensions.cs` | `max_age=0` now forces a login like `prompt=login` and is then removed from the request (new `RemoveMaxAge()`) | Before, after the user logged in, `now > auth_time + 0` was still true, so the user was sent back to login forever. Any party could trigger this with a crafted authorize URL (denial of service). OIDC Core §3.1.2.1 defines `max_age=0` as equivalent to `prompt=login`. |
| `Services/Default/DefaultUserSession.cs` | Require `Principal.Identity.IsAuthenticated` in addition to `AuthenticateResult.Succeeded` | A handler can report success with an unauthenticated principal, and that principal must not be treated as a signed-in user. |

Tests added: expired and tampered JWT access tokens; client assertions with the wrong audience or expired with a trusted key; `max_age=0` (alone and together with `prompt=login`, including the second pass after login); successful authentication with an unauthenticated identity. Unit tests: 760 passed. Integration tests: 293 passed, 1 skipped.

#### Severity and Impact of the Applied Fixes

None of the fixes closes a hole that could be exploited in a standard deployment. The `max_age=0` change is the only real functional fix.

| Fix | Severity | Impact |
|-----|----------|--------|
| `max_age=0` handling | **Low–Medium** (availability) | Before, a user could never finish logging in on a request with `max_age=0`. Clients that force re-authentication (step-up, sensitive actions) were broken, and any party could send a user a link that traps them in a redirect loop. No data was exposed. `max_age=0` now behaves like `prompt=login`. |
| `IsValid` check in `PrivateKeyJwtSecretValidator` and `TokenValidator` | **Low** (latent; hardening against a potentially critical fail-open) | Invalid tokens were already rejected, but only because a null cast threw an exception. A future library change could have turned that into fail-open. Visible behavior changes: expired JWT access tokens now return `expired_token` instead of `invalid_token` (clients that refresh on `expired_token` benefit), and logs no longer show `NullReferenceException` stack traces. |
| `DefaultUserSession` requires `IsAuthenticated` | **Low** (defense in depth) | Only reachable with a custom authentication handler that reports success with an unauthenticated identity. The standard cookie handler never does that, so behavior doesn't change in a normal setup. |

### Follow-ups (not applied)

These were left out of scope on purpose. The audit only applied bug fixes for existing behavior. Each item below adds new protocol behavior, and some would break existing clients, so each needs its own decision and rollout plan.

| Item | Severity | Why not applied | Impact if applied |
|------|----------|-----------------|-------------------|
| Strict client-assertion audience (`aud` = issuer) and `typ: client-authentication+jwt` per RFC 7523bis ([CVE-2025-27370](https://nvd.nist.gov/vuln/detail/CVE-2025-27370), [CVE-2025-27371](https://nvd.nist.gov/vuln/detail/CVE-2025-27371)) | **Medium**, but only when a client also uses the same signing key with other authorization servers | Almost every existing client, including the IdentityModel client libraries, sends the token endpoint URL as `aud`. Today only that URL is accepted, so this server can't be exploited directly. The risk is on the client side: a malicious authorization server can get the client to sign an assertion meant for this server and replay it here. Requiring `aud` = issuer would reject today's clients. | It should be added as an opt-in option (default off) so clients can migrate gradually. Once enabled, assertions with `aud` set to the token endpoint, or without the `typ` header, are rejected. |
| RFC 9207 `iss` parameter in authorization responses | **Low** (defense in depth) | It's a new protocol feature: the discovery flag `authorization_response_iss_parameter_supported` plus the `iss` response parameter. It only helps clients that talk to several authorization servers, and only when those clients check the parameter. | Adds `iss` to authorize responses and to discovery. Clients that don't know the parameter ignore it. |
| `StrictJarValidation` enabled by default | **Low** | Turning it on would reject request objects (JAR) that don't have the `typ: oauth-authz-req+jwt` header, which breaks existing clients. The option already exists and can be enabled per deployment. | Request objects without the explicit type are rejected. |
| Length limit on the encoded Basic authentication header | **Informational** | The decoded client id and secret lengths are already limited (`InputLengthRestrictions`), and Kestrel limits request header size. The remaining benefit is marginal. | Rejects oversized `Authorization: Basic` headers before Base64 decoding. |

---

## Known Remaining Vulnerabilities

None. As of 2026-10-26, no vulnerable packages resolve in `src/` or `samples/`. The SQLitePCLRaw advisory GHSA-2m69-gcr7-jv3q was resolved by 2.1.12 and the package is now on 2.1.14.

### Deprecated Packages (follow-up)

| Package | Version | Used by | Status |
|---------|---------|---------|--------|
| `IdentityModel` | 7.0.0 | `samples` only (transitively via the packages below) | Deprecated/unlisted; `src` migrated to `Duende.IdentityModel` 8.1.0 |
| `IdentityModel.OidcClient`, `IdentityModel.AspNetCore`, `IdentityModel.AspNetCore.AccessTokenValidation`, `IdentityModel.AspNetCore.OAuth2Introspection` | various | `samples` | Deprecated/unlisted; successors are `Duende.IdentityModel.OidcClient`, `Duende.AccessTokenManagement.OpenIdConnect`, `Duende.AspNetCore.Authentication.OAuth2Introspection` |
| `Microsoft.Web.LibraryManager.Build` | 2.1.175 | `samples` | 3.0.114 available (major version, not applied) |
