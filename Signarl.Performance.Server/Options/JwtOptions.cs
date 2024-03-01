using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Signarl.Performance.Server.Options;

internal sealed class JwtOptions
{
    private TokenValidationParameters? _validationParameters;

    /// <summary>
    /// token过期时间
    /// 秒
    /// </summary>
    public int Expires { get; set; }

    /// <summary>
    /// 是否验证token签名
    /// </summary>
    public bool RequireSignedTokens { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    public string? SecurityKey { get; set; }

    /// <summary>
    /// 是否验证受众
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    /// 是否验证发行人
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    /// 是否验证SecurityKey
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }

    /// <summary>
    /// 是否验证过期时间
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    /// 发行人
    /// </summary>
    public string? ValidIssuer { get; set; }

    /// <summary>
    /// 观众
    /// </summary>
    public string? ValidAudience { get; set; }

    /// <summary>
    /// 获取token验证参数
    /// </summary>
    /// <returns></returns>
    public TokenValidationParameters ToTokenValidationParams()
    {
        if (_validationParameters != null)
            return _validationParameters;

        return _validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,//是否验证SecurityKey
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey!)),//密钥

            // Validate the JWT Issuer (iss) claim
            ValidateIssuer = true,//是否验证Issuer
            ValidIssuer = ValidIssuer,//这两项和前面签发jwt的设置一致

            ValidateAudience = true,//是否验证Audience
            ValidAudience = ValidAudience,

            ValidateLifetime = ValidateLifetime,//是否验证失效时间x
            ClockSkew = TimeSpan.Zero,
        };
    }
}