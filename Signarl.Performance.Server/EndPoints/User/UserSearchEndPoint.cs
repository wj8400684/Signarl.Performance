using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Signarl.Performance.Core.Models;
using Signarl.Performance.Server.Data;

namespace Signarl.Performance.Server.EndPoints;

[Authorize(Roles = UserRoles.Admin)]
[HttpGet("/api/user/info")]
public sealed class UserSearchEndPoint(UserManager<IdentityUser> userManager) : Endpoint<EmptyRequest, UserSearchResultModel>
{
    public override async Task<UserSearchResultModel> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var user = await userManager.GetUserAsync(HttpContext.User);

        if (user == null)
            AddError("请重新登陆！");

        ThrowIfAnyErrors();

        return new UserSearchResultModel(user!.UserName!);
    }
}