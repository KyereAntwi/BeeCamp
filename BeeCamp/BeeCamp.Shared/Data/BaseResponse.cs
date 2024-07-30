namespace BeeCamp.Shared.Data;

public class BaseResponse<TResponse>(string message)
{
    public int StatusCode { get; set; }
    public string Message = message;
    public IEnumerable<string> Errors { get; set; } = new string[] { };
    public TResponse? Data { get; set; }
}