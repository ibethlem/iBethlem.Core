namespace iBethlem.Core.Models.Api.Responses;

public class ApiResponse<T> where T : BaseResponse
{
    public ApiResponse(T data) => Data = data;
    public ApiResponse(string message)
    {
        Message = message;
        Error = true;
    }

    public bool Error { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
