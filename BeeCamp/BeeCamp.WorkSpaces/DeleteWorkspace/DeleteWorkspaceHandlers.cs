namespace BeeCamp.WorkSpaces.DeleteWorkspace;

public record DeleteWorkspaceCommand(long Id) : ICommand<Result>;

internal sealed class DeleteWorkspaceCommandHandler(Client client) : ICommandHandler<DeleteWorkspaceCommand, Result>
{
    public async Task<Result> Handle(DeleteWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var workspace = await client
            .From<Workspace>()
            .Where(x => x.Id == command.Id)
            .Single(cancellationToken: cancellationToken);

        if (workspace is null)
        {
            var message = $"Workspace with Id {command.Id} was not found";
            return Result.Fail(message);
        }
        
        await client
            .From<Workspace>()
            .Where(x => x.Id == command.Id)
            .Delete(cancellationToken: cancellationToken);

        return Result.Ok();
    }
}