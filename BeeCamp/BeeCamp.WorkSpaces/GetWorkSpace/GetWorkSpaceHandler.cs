namespace BeeCamp.WorkSpaces.GetWorkSpace;

public class GetWorkSpaceResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}

public record GetWorkSpaceQuery(long Id) : IQuery<Result<GetWorkSpaceResponse>>;

internal sealed class GetWorkSpaceQueryHandler(Client client, IMapperBase mapper)
    : IQueryHandler<GetWorkSpaceQuery, Result<GetWorkSpaceResponse>> 
{
    public async Task<Result<GetWorkSpaceResponse>> Handle(GetWorkSpaceQuery query, CancellationToken cancellationToken)
    {
        var result = await client.From<Workspace>().Where(w => w.Id == query.Id).Single(cancellationToken);

        if (result is not null) return mapper.Map<GetWorkSpaceResponse>(result);
        
        var message = $"Workspace with id {query.Id} was not found";
        return Result.Fail(message);
    }
}