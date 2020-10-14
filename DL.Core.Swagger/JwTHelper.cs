using DL.Core.utility.Configer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DL.Core.utility.Cacher;

namespace DL.Core.Swagger
{
    public class JwTHelper
    {
        public string CreatToken()
        {
            var config = ConfigerManager.Instance.Build().GetSwaggerSetting();
            var claims = new List<Claim>
            {                // 令牌颁发时间
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                 // 过期时间 100秒
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,config.Issuer), // 签发者
            };
            // 密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  
            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,// 声明的集合
                signingCredentials: creds );
            var handler = new JwtSecurityTokenHandler();
            var strJWT = handler.WriteToken(jwt);
            StaticCacher.SetCacher("token", strJWT);
            return strJWT;
        }   
    }
}
