# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning 2](http://semver.org/).

## [10.0.0] - 2026-07-27

### Changed

- Upgraded from .NET 9 to .NET 10 (`net10.0`, SDK 10.0.302)
- Upgraded all ASP.NET Core, EF Core, and Microsoft.Extensions packages to 10.0.10
- Upgraded `Microsoft.Data.SqlClient` 5.1.6 → 6.1.1
- Upgraded `Azure.Identity` 1.11.4 → 1.14.2
- Upgraded `Microsoft.IdentityModel.*` 8.14.0 → 8.19.2
- Upgraded `Npgsql.EntityFrameworkCore.PostgreSQL` → 10.0.3
- Upgraded `Pomelo.EntityFrameworkCore.MySql` 7.0.0 → 9.0.0
- Upgraded `Swashbuckle.AspNetCore` 6.5.0 → 10.2.3
- Upgraded `Nerdbank.GitVersioning` 3.6.133 → 3.10.91
- Upgraded `Microsoft.AspNetCore.Authentication.Google` from pinned 3.1.5 to `$(AspnetMinorVersion)`
- Decoupled Serilog and HealthChecks packages from ASP.NET versioning
- Introduced dedicated `$(EfVersion)` variable for EF Core packages
- Updated all GitHub Actions workflows to .NET 10.x, actions/checkout v4, actions/setup-dotnet v4

### Security

- Fixed CVE-2024-39694: Open redirect vulnerability in `IsLocalUrl()` — URLs with ASCII control characters are now rejected in both `/path` and `~/path` forms
- Updated `Google.Protobuf` 3.23.4 → 3.25.5 (CVE-2024-7254, DoS via deeply nested groups)
- Updated `HtmlAgilityPack` 1.11.40 → 1.12.4 (vulnerable transitive dependency)
- Updated `jQuery.validation` 1.19.5 → 1.21.0 (XSS in versions < 1.20.0)
- Fixed CORS origin matching bug in `CorsPolicyService.cs` — database comparison is now case-insensitive per RFC 6454

### Fixed

- Fixed `CachingResourceStore` key collision between `FindApiResourcesByNameAsync` and `FindApiResourcesByScopeNameAsync` — both shared the same cache namespace; added distinct key prefixes
- Resolved `SQLitePCLRaw.lib.e_sqlite3` vulnerability (GHSA-2m69-gcr7-jv3q) via version bump to 2.1.12
- Removed ILLink `_FixKnownILLinkPack` workaround (no longer needed on .NET 10)

### Removed

- Removed AutoMapper dependency — replaced with hand-written mapping code to resolve DoS vulnerability (GHSA-rvv3-g6hj-g44x) and Apache-2.0 license conflict with AutoMapper 15+ RPL license
- Removed 5 AutoMapper profile classes from `EntityFramework.Storage`
 
## [8.1.0] - 2025-10-08

- Updated all NuGet packages except AutoMapper to their respective, latest version
- Fixed obsoleted method calls in FluentAssertions

## [Unreleased] - 2024-02-17

- NET 9.0 Support
- Updated IdentityModel to 7 and updated OpenIdConnect to 8.1.2
- Removed CollectionUtilities.IsNullOrEmpty dependecy because of obselence in new version of Microsoft.IdentityModel.Tokens
- Current templates and quickstarts being added to seperate template and quickstart repositories to continue previous version functionality.
- DotNet tool to install template currently under development.


## [8.0.4] - 2024-02-17

Identity Server 8.0.4 is a security release that addresses hundreds of security vulnerabilities in the IdentityServer8 code base. We recommend that you update to this version.

- Fix over 100+ security vulnerabilities in the IdentityServer8 code base:
 - #17 Unsafe expansion of self-closing HTML tag
 - #18 URL redirection from remote source
 - #19 DOM text reinterpreted as HTML
 - #20 Incomplete string escaping or encoding
 - #21 Inefficient regular expression bug dependencies
 - #22 Bad HTML filtering regexp bug dependencies
 - #23 User-controlled bypass of sensitive method bug
 - #24 Unsafe jQuery plugins bug dependencies

Additional the codebase has been refactored to use the latest DotNet 8 features and best practices. 

This includes refactroing in #25 and consolidation of reused code that remove some nearly 1 million lines of code from the base.:
- Convert Top Level usings
- Convert Implicit usings.
- Samples use shared API and MVC projects to reduce code duplication and need to maintain dozens of copies of the same code.

## [8.0.3] - 2024-02-12

- Security Updates: Addtional priority critical security patches addressing issues outline in #9 and #10.
 - [Security: User-controlled bypass of sensitive method] - Login Controller and view have have explicit methods to handle login and cancel to address User-controlled bypass of sensitive method
 - [Security: Logging of user-controlled data] - Unsanitized user input could be used to forge logs and inject arbitrary commands, including server side includes, xss and sql injection into log files.
- [Maitenance]: Removed over half a million lines of code from the orginal Identity Server 4 code base using packages and libaries.
 - This will allow for easier maintenance and updates to the code base.
 - Developrs can now focus on the core functionality of Identity Server 8 and use LibMan to manage client side packages and keep packages up to date.
- Documentation Website: identityserver8.readthedocs.io has been created and is now the official documentation website for IdentityServer8
- Gitter: A Gitter chat room has been created for IdentityServer8. You can join the chat at https://app.gitter.im/#/room/#identityserver8:gitter.im
- Framework Upgrade: Upgrade Samples, including Clients, Quickstarts, and Key Management, to use DotNet 8 sdk style.
- [Quickstarts] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/Quickstarts) - Updated Quickstart samples to use Dotnet 8 startup with implicit usings and minimal Api.
- [Clients] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/Clients) - Updated client samples to use Dotnet 8 startup with implicit usings and minimal Api.
- [Key Management] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/KeyManagement) - Updated Key management samples to use Dotnet 8 startup with implicit usings and minimal Api. Changed default Entity Framework storage to file system storage as original Key Management is a paid solution. Roadmap: Add DbContext implementation fof key management.
- Client Side Packages: Client Side packages have now been ignored in source and are now installed using LibMan during the build process. This will allow for easier updates and management of client side packages.

## [8.0.2] - 2024-02-12

- Security Updates: Addtional priority critical security patches addressing issues outline in #9 and #10.

## [8.0.1] - 2024-02-10
 
- Security Update: High priority critical security patches addressing issues outline in #9 and #10.

 
### Added
- `IdentityServer8.Security` nuget packages with services to sanitize user input including html, json, xml, javascript, scripts, urls, logs, css, and style sheets.

### Changed
- [Account Login Controller] (https://github.com/alexhiggins732/IdentityServer8/issues/9) 
- [Account Login View] (https://github.com/alexhiggins732/IdentityServer8/issues/9)  
 
### Fixed
- [Security: User-controlled bypass of sensitive method]
  Login Controller and view have have explicit methods to handle login and cancel to address User-controlled bypass of sensitive method
- [Security: Logging of user-controlled data]
  Unsanitized user input could be used to forge logs and inject arbitrary commands, including server side includes, xss and sql injection into log files.
  
## [8.0.1] - 2024-02-10
  
Updated build scripts to use Git Flow branching for SemVer2 compatible nuget packages.
 
### Added

- CodeQl Security scanning
- Dependabot Package scanning. 
### Changed
  
- [IdentityServer8 8.0.1 changes]https://github.com/alexhiggins732/IdentityServer8/pull/7) 

### Fixed
 
- Nuget Package version conflicts.
 
## [8.0.0] - 2024-02-09
 
### Added
Build scripts and readme documentation for initial port from Identity Server 4 and Identity Server 4 Admin   
### Changed
Upgraded Main Identity Server projects and Nuget packages to DotNet 8 
### Fixed
 
- Changed mixed dependencies on `System.Text.Json` and `Newtonsoft.Json` to use `System.Text.Json` which resolved several bugs.
- Change package dependencies and version requirements to run on the latest DotNet 8 packages, resolving many security vulnerablities.