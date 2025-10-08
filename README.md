# TTT
This project is a fork of alexhiggins732/IdentityServer8

# Identity Server 8 update
This project is a DotNet 8 revival of the Identity Server 4 and Identity Server 4 Admin UI, for Open ID Connect (OIDC) and OAuth, which was archived when .NET Core 3.1 reached end of support.

The latest verion, 8.1.0, is now available on NuGet. It contains [hundreds of security and bug fixes](https://github.com/alexhiggins732/IdentityServer8/blob/master/docs/CHANGELOG.md) from the original Identity Server 4 project.

It is recommend you update all previous version, 4 or 8, to the latest version to ensure you have the latest security updates. 

- [Documentation](http://identityserver8.readthedocs.io/)
- [Support](https://identityserver8.readthedocs.io/en/latest/into/support.html)
- [Gitter Chat](https://app.gitter.im/#/room/#identityserver8:gitter.im)

The [TTT.IdentityServer8 Nuget Package](https://www.nuget.org/packages?q=TTT.IdentityServer8) is available here for use in DotNet 9.

All new development in the archived repository has moved to a paid commercial version in the [Duende Software](https://github.com/duendesoftware) organization. 

See [here](https://duendesoftware.com/products/identityserver) for more details on the commerica version.

This repository will be maintained for bug fixes and security updates for the .NET 9 version of Identity Server 4.

The source code and unit tests will be updated to use the latest .NET 9 features and best practices.

Once the source code and unit tests are stabilized, the documentation will be updated to reflect the changes.

General speaking, the existing documentation for Identity Server 4 will be valid for this repository, but the new features and changes will be documented in the new documentation.

In the meantime, NuGet packages will be published to the [IdentityServer8 NuGet feed](https://www.nuget.org/profiles/IdentityServer8) and the [IdentityServer8 MyGet feed](https://www.myget.org/F/identityserver8/api/v3/index.json).


## Build Status And Stats

[![Master | Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml)
[![Release|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/release.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/release.yml)

[![Develop|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml)
[![CI/CD|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/pre-release.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/pre-release.yml)

## Code Coverage
[![Master | Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml/badge.svg)](https://img.shields.io/codecov/c/github/Tri-Torque/identityserver8) [![Master|CodeQL](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml)

[![Develop|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml/badge.svg)](https://img.shields.io/codecov/c/github/Tri-Torque/identityserver8/tree/develop)
[![Master|CodeQL](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml/badge.svg?branch=develop)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml?branch=develop)

## Documentation
[![Documentation Status](https://readthedocs.org/projects/identityserver8/badge/?version=latest)](https://identityserver8.readthedocs.io/en/latest/?badge=latest)
[Read the docs - identityserver8.readthedocs.io ](http://identityserver8.readthedocs.io/)

## Nuget Packages

### Identity Server 8
|Package||
| ------------- | ------------- |
|[TTT.IdentityServer8](https://www.nuget.org/packages?q=TTT.IdentityServer8)|![NuGet Downloads](https://img.shields.io/nuget/dt/TTT.IdentityServer8)|
|[TTT.IdentityServer8.Storage](https://www.nuget.org/packages?q=TTT.IdentityServer8.Storage)|![NuGet Downloads](https://img.shields.io/nuget/dt/TTT.IdentityServer8.Storage)|
|[TTT.IdentityServer8.EntityFramework](https://www.nuget.org/packages?q=TTT.IdentityServer8.EntityFramework)|![NuGet Downloads](https://img.shields.io/nuget/dt/TTT.IdentityServer8.EntityFramework)|
|[TTT.IdentityServer8.EntityFramework.Storage](https://www.nuget.org/packages?q=TTT.IdentityServer8.EntityFramework.Storage)|![NuGet Downloads](https://img.shields.io/nuget/dt/TTT.IdentityServer8.EntityFramework.Storage)|
|[TTT.IdentityServer8.AspNetIdentity](https://www.nuget.org/packages?q=TTT.IdentityServer8.AspNetIdentity)|![NuGet Downloads](https://img.shields.io/nuget/dt/TTT.IdentityServer8.AspNetIdentity)|
| | |


## What's New

View the [CHANGELOG](docs/CHANGELOG.md) for the latest changes.

## About IdentityServer8
[<img align="right" width="100px" src="https://dotnetfoundation.org/img/logo_big.svg" />](https://dotnetfoundation.org/projects?searchquery=IdentityServer&type=project)

IdentityServer is a free, open source [OpenID Connect](http://openid.net/connect/) and [OAuth 2.0](https://tools.ietf.org/html/rfc6749) framework for ASP.NET Core.
Founded and maintained by [Dominick Baier](https://twitter.com/leastprivilege) and [Brock Allen](https://twitter.com/brocklallen), IdentityServer8 incorporates all the protocol implementations and extensibility points needed to integrate token-based authentication, single-sign-on and API access control in your applications.
IdentityServer8 is officially [certified](https://openid.net/certification/) by the [OpenID Foundation](https://openid.net) and thus spec-compliant and interoperable.
It is part of the [.NET Foundation](https://www.dotnetfoundation.org/), and operates under their [code of conduct](https://www.dotnetfoundation.org/code-of-conduct). It is licensed under [Apache 2](https://opensource.org/licenses/Apache-2.0) (an OSI approved license).

For project documentation, please visit [readthedocs](https://IdentityServer8.readthedocs.io).

## Branch structure
Active development happens on the main branch. This always contains the latest version. Each (pre-) release is tagged with the corresponding version.

## How to build

* [Install]([https://www.microsoft.com/net/download/core#/current](https://dotnet.microsoft.com/en-us/download#/current) the latest .NET 8 SDK
* Install Git
* Clone this repo
* Run `dotnet build src/identityserver8.slm` or `build.sh` in the root of the cloned repo.

## Documentation
For project documentation, please visit [readthedocs](https://IdentityServer8.readthedocs.io).

## Bug reports and feature requests
Please use the [issue tracker](https://github.com/Tri-Torque/IdentityServer8/issues) for that.

## Acknowledgements
IdentityServer8 is built using the following great open source projects and free services:

* [ASP.NET Core](https://github.com/dotnet/aspnetcore)
* [Bullseye](https://github.com/adamralph/bullseye)
* [SimpleExec](https://github.com/adamralph/simple-exec)
* [MinVer](https://github.com/adamralph/minver)
* [Json.Net](http://www.newtonsoft.com/json)
* [XUnit](https://xunit.github.io/)
* [Fluent Assertions](http://www.fluentassertions.com/)
* [GitReleaseManager](https://github.com/GitTools/GitReleaseManager)

..and last but not least a big thanks to all our [contributors](https://github.com/Tri-Torque/IdentityServer8/graphs/contributors)!
