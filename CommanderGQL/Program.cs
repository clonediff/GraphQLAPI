using CommanderGQL;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettingsConsts.CommandConStr)))
    .AddGraphQLServer()
    .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>();
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettingsConsts.CommandConStr)));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL();
app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions()
{
    GraphQLEndPoint = "/graphql"
});

app.Run();

// dotnet ef migrations add AddPlatformToDB -p .\CommanderGQL\ -s .\CommanderGQL\           (Run to create migrations from root path)
// dotnet ef database update -p .\CommanderGQL\ -s .\CommanderGQL\                          (Run to update db by latest migration)
// https://localhost:5001/graphql                                                           (Opens web ui for GraphQL)
// https://localhost:5001/graphql-voyager                                                   (Opens web ui for GraphQL voyager)
