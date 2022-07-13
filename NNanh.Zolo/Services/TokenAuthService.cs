using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.WebUI;
using Domain.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NNanh.Zolo.Services
{
    public class TokenAuthService : ITokenAuthService
    {
        private readonly IAppService _appService;

        public TokenAuthService(IAppService appService)
        {
            _appService = appService;
        }


        public JwtTokens Generate(string userName)
        {
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_appService.ApplicationSetting.Jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appService.ApplicationSetting.Jwt.TimeExpires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtTokens { Token = tokenHandler.WriteToken(token) };
        }

        public bool ComparePassword(string rawData, string hashString)
        {
            return hashString.Equals(HashPassword(rawData));
        }

        public string HashPassword(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
