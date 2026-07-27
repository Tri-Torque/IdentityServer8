# Development Guide

This guide explains how to run the IdentityServer8 development hosts locally using .NET Aspire.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later
- [Aspire CLI](https://aspire.dev/get-started/) v13.4+
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) or [Podman](https://podman.io/) (required for SQL Server container)

## Running the Dev Hosts

From the repository root:

```bash
cd src/AppHost
dotnet run
```

Or using the Aspire CLI:

```bash
aspire run --project src/AppHost/AppHost.csproj
```

This starts the **Aspire Dashboard** (opens in your browser) and orchestrates all three development hosts plus a SQL Server 2022 container.

## Development Host Projects

The solution includes three development hosts. Each demonstrates IdentityServer8 with a different storage/identity backend:

### 1. `identityserver` — Core IdentityServer Host

- **Location:** `src/IdentityServer8/host/`
- **Port:** `https://localhost:5001`
- **Storage:** In-memory clients, resources, and scopes; SQLite for optional persisted grants
- **Users:** In-memory test users (see below)
- **Purpose:** Quick testing of the core IdentityServer8 protocol, signing credentials, mTLS, external identity providers, and extension grants.

### 2. `entityframework-host` — Entity Framework Host

- **Location:** `src/EntityFramework/host/`
- **Port:** `https://localhost:5011`
- **Storage:** SQL Server (provisioned by Aspire) for configuration and operational data (clients, resources, tokens)
- **Users:** In-memory test users (see below)
- **Purpose:** Testing the Entity Framework-backed configuration and operational stores, including token cleanup.

### 3. `aspnetidentity-host` — ASP.NET Identity Host

- **Location:** `src/AspNetIdentity/host/`
- **Port:** `https://localhost:5021`
- **Storage:** SQL Server (provisioned by Aspire) for ASP.NET Identity user management
- **Users:** No seeded users — register via the UI (see below)
- **Purpose:** Testing IdentityServer8 integrated with ASP.NET Identity for real user registration, login, and management.

## Test Users

The **identityserver** and **entityframework-host** projects have two pre-configured test users:

| Username | Password | Full Name   | Email                  |
|----------|----------|-------------|------------------------|
| `alice`  | `alice`  | Alice Smith | AliceSmith@email.com   |
| `bob`    | `bob`    | Bob Smith   | BobSmith@email.com     |

Both users have claims for name, email, website, and a sample address.

> **Note:** The `aspnetidentity-host` uses ASP.NET Identity with a real database. There are no seeded users — you must **Register** a new account through the web UI.

## Aspire Dashboard

When running via Aspire, the dashboard provides:

- **Resource overview** — status of all hosts and the SQL Server container
- **Structured logs** — per-service log viewing
- **Distributed traces** — request traces across services
- **Metrics** — OpenTelemetry metrics for each host

## Architecture Notes

- The **AppHost** (`src/AppHost/`) orchestrates all services and provisions a SQL Server 2022 container with two databases:
  - `db` — used by the EntityFramework host
  - `DefaultConnection` — used by the AspNetIdentity host
- The **ServiceDefaults** (`src/ServiceDefaults/`) project provides shared configuration for OpenTelemetry, health checks, service discovery, and HTTP resilience.
- Each host includes health endpoints at `/health` and `/alive`.
