var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithImageTag("2022-latest");

var db = sqlServer.AddDatabase("db");
var defaultConnectionDb = sqlServer.AddDatabase("DefaultConnection");

builder.AddProject<Projects.IdentityServerHost>("identityserver")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.EntityFrameworkHost>("entityframework-host")
    .WithReference(db)
    .WaitFor(db)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.AspNetIdentityHost>("aspnetidentity-host")
    .WithReference(defaultConnectionDb)
    .WaitFor(defaultConnectionDb)
    .WithExternalHttpEndpoints();

builder.Build().Run();
