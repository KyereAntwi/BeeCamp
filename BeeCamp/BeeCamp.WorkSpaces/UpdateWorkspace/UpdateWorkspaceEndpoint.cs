using BeeCamp.Shared.Utilities.Errors;

namespace BeeCamp.WorkSpaces.UpdateWorkspace;

public class UpdateWorkspaceEndpoint : CarterModule
{
    public UpdateWorkspaceEndpoint() : base("/api/v1/workspaces")
    {
        IncludeInOpenApi();
        WithTags("Workspaces");
        RequireAuthorization();
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", async (ISender sender, long id, [FromBody] UpdateWorkspaceRequest request) =>
        {
            var result = await sender.Send(new UpdateWorkspaceCommand(id, request.Title, request.Description));

            if (!result.IsSuccess) return Results.Accepted();
            
            if (result.HasError<ValidationErrors>())
            {
                return Results.BadRequest(new BaseResponse<string>("Validation failed")
                {
                    StatusCode = 400,
                    Errors = result.Errors.Select(e => e.Message)
                });
            }

            if (result.HasError<RecordNotFoundErrors>())
            {
                return Results.NotFound(new BaseResponse<string>("Failed")
                {
                    StatusCode = 404,
                    Errors = result.Errors.Select(e => e.Message)
                });
            }

            return Results.Accepted();
        })
        .WithName("UpdateWorkspace")
        .WithDescription("Update a workspace")
        .Produces(StatusCodes.Status202Accepted)
        .Produces<BaseResponse<string>>(StatusCodes.Status400BadRequest)
        .Produces<BaseResponse<string>>(StatusCodes.Status404NotFound);
    }
}