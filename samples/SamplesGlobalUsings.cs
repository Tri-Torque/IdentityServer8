/*
 Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

global using Duende.IdentityModel;
global using Duende.IdentityModel.Client;
global using Serilog;
global using System.Diagnostics;
global using System.Text;
global using System.Text.Json;


global using Duende.IdentityModel.OidcClient;
global using Duende.IdentityModel.OidcClient.Browser;
global using Duende.AccessTokenManagement.OpenIdConnect;
global using Duende.AspNetCore.Authentication.OAuth2Introspection;


global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.DataProtection;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Serilog.Events;
global using Serilog.Sinks.SystemConsole.Themes;

global using System.Net;
global using System.Net.Sockets;
global using System.Runtime.InteropServices;
global using System.Security.Cryptography.X509Certificates;


global using ILogger = Microsoft.Extensions.Logging.ILogger;

global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Runtime.Versioning;