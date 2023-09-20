using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command");

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
            .Description("This is the platform to which the command belongs");
    }
    
    private class Resolvers
    {
        public Platform GetPlatform([Parent] Command command, AppDbContext context)
        {
            return context.Platforms.First(p => p.Id == command.PlatformId);
        }
    }
}
