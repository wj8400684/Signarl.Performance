namespace Signarl.Performance.Core.Models;

public sealed class ApiResponse
{
    public bool SuccessFul { get; set; }

    public int ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public static ApiResponse<TResultModel> Fail<TResultModel>(string errorMessage) where TResultModel : class =>
        new() { SuccessFul = false, ErrorMessage = errorMessage };
}

public sealed class ApiResponse<TResultModel>
    where TResultModel : class
{
    public ApiResponse()
    {
    }

    public ApiResponse(TResultModel value)
    {
        Content = value;
    }

    public bool SuccessFul { get; set; }

    public int? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public TResultModel? Content { get; set; }

    /// <summary>
    /// 隐式将T转化为ResponseResult<T>
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator ApiResponse<TResultModel>(TResultModel value)
    {
        return new ApiResponse<TResultModel>(value) { SuccessFul = true };
    }
}