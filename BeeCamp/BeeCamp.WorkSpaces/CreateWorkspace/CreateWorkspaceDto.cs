namespace BeeCamp.WorkSpaces.CreateWorkspace;

public record CreateWorkspaceResponse(long Id);

public class CreateWorkspaceRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}