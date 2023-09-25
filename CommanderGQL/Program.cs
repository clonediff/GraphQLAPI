using CommanderGQL;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettingsConsts.CommandConStr)))
    .AddGraphQLServer()
    .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettingsConsts.CommandConStr)));

var app = builder.Build();

app.UseWebSockets();

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

/*
 Filter Query example:
query{
    command(where: {platformId: {eq: 2}}){
        id
        platform{
            name
        }
        commandLine
        howTo
    }
}
*/

/*
 Sorting Query example:
query{
    platform(order: {name: DESC}){
        name
    }
}
*/

/*
 mutation example:
mutation{
    addPlatform(input: {
        name: "Ubuntu"
    }){
        platform{
            id
            name
        }
    }
}
*/

/*
 При запуске запроса с подпиской, он начинает вечно ждать события. И при каждой отправке события будет отрабатывать (не работает в Postman и др., работает в браузере, 
 т.к. работает через WebSockets)
 
 subscription example:
subscription{
    onPlatformAdded{
        id
        name
    }
}
*/
