namespace BeeCamp.WorkSpaces.DeleteWorkspace;

public class DeleteWorkspaceEndpoint : CarterModule
{
    public DeleteWorkspaceEndpoint() : base("/api/v1/workspaces")
    {
        IncludeInOpenApi();
        WithTags("Workspaces");
        RequireAuthorization();
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (ISender sender, long id) =>
            {
                var result = await sender.Send(new DeleteWorkspaceCommand(id));
                
                
            })
        .WithName("DeleteWorkspace")
        .WithDescription("Delete a workspace by id")
        .Produces(StatusCodes.Status200OK)
        .Produces<BaseResponse<string>>(StatusCodes.Status404NotFound);
    }
}