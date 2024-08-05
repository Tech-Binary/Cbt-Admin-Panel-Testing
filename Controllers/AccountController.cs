using CbtAdminPanel.Interface;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Dapper.SqlMapper;

namespace CbtAdminPanel.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        //private readonly Token _tokenService;

        //public AccountController(Token tokenService)
        //{
        //    _tokenService = tokenSe
        //}
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository, MyDbcontext _context)
        {
            _repository = repository;
        }

        [Route("Login")]
        [HttpPost]
        public ResponseModel Index([FromBody]UserModel user)
        {

            ResponseModel res =_repository.CheckUser(user);
            return res;
        }
       

        //public string GenerateToken(string username)
        //{
        //    var claims = new[]
        //   {
        //        new Claim(JwtRegisteredClaimNames.Sub,"hars"),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email,"HarshitVijay77@gmail.com")
        //    };
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Harshit6h777b76r65dbw4@@@@567567Harshit6h777b76r65dbw4@@@@567567"));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        Issuer = "harshitissue",
        //        Audience = "HarshitAudience",
        //        SigningCredentials = credentials
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    GetPrincipalFromExpiredToken(tokenHandler.WriteToken(token), "Harshit6h777b76r65dbw4@@@@567567Harshit6h777b76r65dbw4@@@@567567");
        //    return tokenHandler.WriteToken(token);
        //}

        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string secretKey)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        ValidateLifetime = false, // here we are saying that we don't care about the token's expiration date
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        //    JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }

        //    return principal;
        //}
    }
}
