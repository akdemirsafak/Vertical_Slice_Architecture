using System.Text.Json.Serialization;

namespace Vertical_Slice_Architecture.Shared;

public class AppResponse<T>
{
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    [JsonIgnore] public int StatusCode { get; set; }

    //Static Factory Method
    public static AppResponse<T> Success(T Data, int statusCode = 200)
    {
        return new AppResponse<T> { Data = Data, StatusCode = statusCode };
    }
    public static AppResponse<T> Success(int statusCode = 200)
    {
        return new AppResponse<T> { Data = default, StatusCode = statusCode };
    }
    public static AppResponse<T> Fail(List<string> errors, int statusCode = 0)
    {
        return new AppResponse<T> { Data = default, Errors = errors, StatusCode = statusCode };
    }

    public static AppResponse<T> Fail(string error, int statusCode = 0)
    {
        return new AppResponse<T> { Data = default, Errors = new List<string> { error }, StatusCode = statusCode };
    }
}
