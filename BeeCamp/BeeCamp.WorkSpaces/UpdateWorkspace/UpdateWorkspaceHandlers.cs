namespace BeeCamp.WorkSpaces.UpdateWorkspace;

public record UpdateWorkspaceCommand(long Id, string Title, string Description) : ICommand<Result>;

internal sealed class UpdateWorkspaceCommandHandler(Client client) : ICommandHandler<UpdateWorkspaceCommand, Result>
{
    public async Task<Result> Handle(UpdateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var validation = new UpdateWorkspaceCommandValidator();
        var validationErrors = await validation.ValidateAsync(command, cancellationToken);

        if (!validationErrors.IsValid) return Result.Fail(validationErrors.Errors.Select(e => e.ErrorMessage));
        
        var workspace = await client
            .From<Workspace>()
            .Where(x => x.Id == command.Id)
            .Single(cancellationToken);

        if (workspace is null)
        {
            var message = $"Workspace with Id {command.Id} does not exist";
            return Result.Fail(message);
        }

        workspace.Title = command.Title;
        workspace.Description = command.Description;

        await workspace.Update<Workspace>(cancellationToken);

        return Result.Ok();
    }
}

public class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidator()
    {
        RuleFor(w => w.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(3).WithMessage("{PropertyName} should be at least 3 characters long")
            .NotNull();
        
        RuleFor(w => w.Description)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(3).WithMessage("{PropertyName} should be at least 3 characters long")
            .NotNull();
    }
}