using CommanderGQL;
using CommanderGQL.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettingsConsts.CommandConStr)));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

// dotnet ef migrations add AddPlatformToDB -p .\CommanderGQL\ -s .\CommanderGQL\           (Run to create migrations from root path)
// dotnet ef database update -p .\CommanderGQL\ -s .\CommanderGQL\                          (Run to update db by latest migration)
