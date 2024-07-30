namespace BeeCamp.WorkSpaces.CreateWorkspace;

public class CreateWorkspaceEndpoint : CarterModule
{
    public CreateWorkspaceEndpoint() : base("/api/v1/workspaces")
    {
        RequireAuthorization();
        WithTags("Workspaces");
        IncludeInOpenApi();
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (ISender sender, [FromBody] CreateWorkspaceRequest request) =>
        {
            var result = await sender.Send(new CreateWorkspaceCommand(request.Title, request.Description));

            if (result.IsFailed)
            {
                return Results.BadRequest(new BaseResponse<string>("Operation failed")
                {
                    StatusCode = 400,
                    Errors = result.Errors.Select(e => e.Message)
                });
            }
            
            return Results.CreatedAtRoute("", new {id = result.ValueOrDefault.Id}, new BaseResponse<CreateWorkspaceResponse>("Success")
            {
                Data = result.ValueOrDefault,
                StatusCode = 201
            });
        })
            .WithName("CreateWorkspace")
            .WithDescription("Create a new workspace")
            .Produces<BaseResponse<CreateWorkspaceResponse>>(StatusCodes.Status201Created)
            .Produces<BaseResponse<CreateWorkspaceResponse>>(StatusCodes.Status400BadRequest);
    }
}