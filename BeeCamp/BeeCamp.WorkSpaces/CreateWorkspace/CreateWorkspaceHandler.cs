namespace BeeCamp.WorkSpaces.CreateWorkspace;

public record CreateWorkspaceCommand(
    string Title,
    string Description
    ) : ICommand<Result<CreateWorkspaceResponse>>;

internal sealed class
    CreateWorkspaceCommandHandler(Client client) : ICommandHandler<CreateWorkspaceCommand, Result<CreateWorkspaceResponse>>
{
    public async Task<Result<CreateWorkspaceResponse>> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var validation = new CreateWorkspaceCommandValidator();
        var validationErrors = await validation.ValidateAsync(command, cancellationToken);

        if (validationErrors.Errors.Any())
        {
            return Result.Fail(validationErrors.Errors.Select(e => e.ErrorMessage));
        }
        
        var newWorkspace = new Workspace { Title = command.Title, Description = command.Description };
        
        var result = await client
            .From<Workspace>()
            .Insert(newWorkspace, new QueryOptions { Returning = QueryOptions.ReturnType.Representation }, cancellationToken);

        if (result.Model != null) return Result.Ok(new CreateWorkspaceResponse(result.Model.Id));

        const string message = "Creating work space failed";
        return Result.Fail(message);
    }
}


public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
{
    public CreateWorkspaceCommandValidator()
    {
        RuleFor(w => w.Title)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long")
            .NotNull();
        
        RuleFor(w => w.Description)
            .NotEmpty().WithMessage("{PropertyName} must not be empty")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long")
            .NotNull();
    }
}