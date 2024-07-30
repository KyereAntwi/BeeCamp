namespace BeeCamp.WorkSpaces.GetWorkSpace;

public class GetWorkSpaceEndpoint : CarterModule
{
    public GetWorkSpaceEndpoint() : base("/api/v1/workspaces")
    {
        RequireAuthorization();
        IncludeInOpenApi();
        WithTags("Workspaces");
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (ISender sender, long id) =>
        {
            var result = await sender.Send(new GetWorkSpaceQuery(id));

            if (result.IsFailed)
            {
                return Results.NotFound(new BaseResponse<string>("Operation failed")
                {
                    StatusCode = 404,
                    Errors = result.Errors.Select(e => e.Message)
                });
            }

            return Results.Ok(new BaseResponse<GetWorkSpaceResponse>("Success")
            {
                Data = result.ValueOrDefault,
                StatusCode = 200
            });
        });
    }
}