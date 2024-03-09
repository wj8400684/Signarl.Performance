using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Signarl.Performance.Core.Models;
using Signarl.Performance.Server.Data;

namespace Signarl.Performance.Server.EndPoints;

[AllowAnonymous]
[HttpGet("/api/user/register")]
public sealed class UserRegisterEndPoint(
    RoleManager<IdentityRole> roleManager,
    UserManager<IdentityUser> userManager)
    : Endpoint<UserRegisterRequest, ApiResponse<UserRegisterResultModel>>
{
    public override async Task<ApiResponse<UserRegisterResultModel>> ExecuteAsync(UserRegisterRequest req, CancellationToken ct)
    {
        var userExists = await userManager.FindByNameAsync(req.Username);
        if (userExists != null)
            return ApiResponse.Fail<UserRegisterResultModel>("该账号已被注册,请重试!");

        var result = await userManager.CreateAsync(password: req.Password, user: new IdentityUser
        {
            Email = req.Email,
            UserName = req.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
        });

        if (!result.Succeeded)
            return ApiResponse.Fail<UserRegisterResultModel>(
                $"注册失败请重试! {string.Join(":", result.Errors.Select(e => e.Description))}");

        var userProfile = await userManager.FindByNameAsync(req.Username);

        if (userProfile == null)
            AddError("注册失败,未知错误");
        
        ThrowIfAnyErrors();

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (!await roleManager.RoleExistsAsync(UserRoles.Client))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Client));

        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            await userManager.AddToRoleAsync(userProfile!, UserRoles.Admin);

        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            await userManager.AddToRoleAsync(userProfile!, UserRoles.Client);

        return new UserRegisterResultModel(userProfile!.Id);
    }
}