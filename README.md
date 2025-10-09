# TTT
This project is a fork of [alexhiggins732/IdentityServer8](https://github.com/alexhiggins732/IdentityServer8) to make IdentityServer8 work on .NET 9

## Build Status And Stats

[![Master | Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml)
[![Release|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/release.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/release.yml)

[![Develop|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml)
[![CI/CD|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/pre-release.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/pre-release.yml)

## Code Coverage
[![Master | Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/master.yml/badge.svg)](https://img.shields.io/codecov/c/github/Tri-Torque/identityserver8) [![Master|CodeQL](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml/badge.svg)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml)

[![Develop|Build](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/develop.yml/badge.svg)](https://img.shields.io/codecov/c/github/Tri-Torque/identityserver8/tree/develop)
[![Master|CodeQL](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml/badge.svg?branch=develop)](https://github.com/Tri-Torque/IdentityServer8/actions/workflows/codeql.yml?branch=develop)

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


## How to build

* [Install]([https://www.microsoft.com/net/download/core#/current](https://dotnet.microsoft.com/en-us/download#/current) the latest .NET 9 SDK
* Install Git
* Clone this repo
* Run `dotnet build src/identityserver8.sln` or `build.sh` in the root of the cloned repo.
