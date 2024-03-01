using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Signarl.Performance.Core.Models;
using Signarl.Performance.Server.Extensions;

namespace Signarl.Performance.Server.EndPoints;

[AllowAnonymous]
[HttpGet("/api/user/login")]
public sealed class LoginEndPoint(
    ILogger<LoginEndPoint> logger,
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager)
    : Endpoint<LoginRequest, ApiResponse<LoginResultModel>>
{
    public override async Task<ApiResponse<LoginResultModel>> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        logger.LogInformation($"[{req.Username}-{req.Password}] 尝试登录");

        var result = await signInManager.PasswordSignInAsync(req.Username, req.Password, false, true);

        if (result.IsNotAllowed)
            return ApiResponse.Fail<LoginResultModel>("该账户不允许登录,请稍后再试!");

        if (result.RequiresTwoFactor)
            return ApiResponse.Fail<LoginResultModel>("该账号需要二次验证,请稍后再试!");

        var userProfile = await userManager.FindByNameAsync(req.Username);

        if (userProfile == null)
            return ApiResponse.Fail<LoginResultModel>("账号或密码错误!");

        if (result.IsLockedOut)
            return ApiResponse.Fail<LoginResultModel>($"登陆过于频繁请{userProfile.LockoutEnd}后再试!");

        if (!result.Succeeded)
            return ApiResponse.Fail<LoginResultModel>("登录失败请稍后重试!");

        var token = await userManager.GenerateUserTokenAsync(userProfile);

        return new LoginResultModel(userProfile.Id, token);
    }
}