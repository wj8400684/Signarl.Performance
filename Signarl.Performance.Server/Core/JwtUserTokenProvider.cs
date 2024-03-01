using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Signarl.Performance.Server.Options;

namespace Signarl.Performance.Server.Core;

internal sealed class JwtUserTokenProvider : IUserTwoFactorTokenProvider<IdentityUser>
{
    private readonly JwtOptions _options;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurity = new();

    public JwtUserTokenProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _signingCredentials = new SigningCredentials(_options.ToTokenValidationParams().IssuerSigningKey, SecurityAlgorithms.HmacSha256);
    }

    /// <summary>
    /// 是否生成二次验证token
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<IdentityUser> manager, IdentityUser user)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// 生产token
    /// </summary>
    /// <param name="purpose"></param>
    /// <param name="userManager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateAsync(string purpose, UserManager<IdentityUser> userManager, IdentityUser user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = new JwtSecurityToken(claims: authClaims,
                                         issuer: _options.ValidIssuer,
                                         audience: _options.ValidAudience,
                                         signingCredentials: _signingCredentials,
                                         expires: DateTime.Now.AddSeconds(_options.Expires));

        return _jwtSecurity.WriteToken(token);
    }

    /// <summary>
    /// 验证token
    /// </summary>
    /// <param name="purpose"></param>
    /// <param name="token"></param>
    /// <param name="userManager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> ValidateAsync(string purpose, string token, UserManager<IdentityUser> userManager, IdentityUser user)
    {
        throw new NotImplementedException();
    }
}