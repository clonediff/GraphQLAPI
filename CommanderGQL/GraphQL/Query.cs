using CommanderGQL.Data;
using CommanderGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderGQL.GraphQL;

public class Query
{
    [UseProjection]
    public IQueryable<Platform> GetPlatform(AppDbContext context)
    {
        return context.Platforms;
    }

    [UseProjection]
    public IQueryable<Command> GetCommand(AppDbContext context)
    {
        return context.Commands;
    }
}
