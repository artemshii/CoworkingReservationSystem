using CoworkingReservation.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoworkingReservation.Data
{
    public class JwtTokenCreator
    {
        public string GenerateNewToken(AuthUserData authUserData)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userName", authUserData.UserName),
                new Claim("Id", authUserData.Id.ToString())
            };

            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(2),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Enter JWT Secret here")), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

