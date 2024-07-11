using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.API.Utils;
using Users.db.Entities;
namespace Users.API.Middlewares
{
    public interface IJWTUtils
    {
        string GenerateJWTToken(DbUser user);
        int? ValidateJWTToken(string? token);

    }
    public class JWTutils : IJWTUtils 
    {
        private readonly AppSettings _appSettings;
        public JWTutils (IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;

            if (string.IsNullOrEmpty(_appSettings.secret))
                throw new Exception("отсуствует токен");
        }
        public string GenerateJWTToken(DbUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secret!);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new [] {new Claim("id",user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken (token);
        }
        public int? ValidateJWTToken(string? token)
        {
            if(token == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.secret!);

            try
            {
                tokenHandler.ValidateToken(token,new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)(validatedToken);
                var userid = int.Parse(jwtToken.Claims.First(u => u.Type == "id").Value);
                return userid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
