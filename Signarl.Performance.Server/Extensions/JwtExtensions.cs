using Microsoft.AspNetCore.Identity;
using Signarl.Performance.Server.Core;

namespace Signarl.Performance.Server.Extensions;

internal static class JwtExtensions
{
    const string ProviderName = "jwtToken";

    /// <summary>
    /// 添加jwt token 提供
    /// </summary>
    /// <param name="identity"></param>
    /// <returns></returns>
    public static IdentityBuilder AddJwtTokenProvider(this IdentityBuilder identity)
    {
        return identity.AddTokenProvider<JwtUserTokenProvider>(ProviderName);
    }

    /// <summary>
    /// 生成jwt token
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <param name="userManager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static Task<string> GenerateUserTokenAsync<TUser>(this UserManager<TUser> userManager, TUser user)
        where TUser : class
    {
        return userManager.GenerateUserTokenAsync(user, ProviderName, ProviderName);
    }
}