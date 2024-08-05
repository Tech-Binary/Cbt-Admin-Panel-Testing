using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CbtAdminPanel.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;

        public AccountRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public ResponseModel CheckUser(UserModel user)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (user.UserName !="" || user.Password !="")
                {
                    Users userinfo=_context.Users.Where(x=>x.UserName == user.UserName && x.Password== user.Password).FirstOrDefault();
                    if (userinfo != null)
                    {
                        responseModel.Data= GenerateToken(userinfo);
                        responseModel.Status = StatusEnums.success.ToString();
                    }
                    else
                    {
                        responseModel.Status = StatusEnums.error.ToString();
                        responseModel.Message = "No result Found";
                    }
                }
                else
                {
                    responseModel.Status = StatusEnums.error.ToString(); 
                    responseModel.Message = "Invalid User"; 

                }

            }
            catch (Exception ex)
            {
                responseModel.Status = StatusEnums.error.ToString();
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }


        public string GenerateToken(Users users)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,"hars"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,"HarshitVijay77@gmail.com"),
                new Claim(JwtRegisteredClaimNames.NameId,users.Id.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Harshit6h777b76r65dbw4@@@@567567Harshit6h777b76r65dbw4@@@@567567"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "harshitissue",
                Audience = "HarshitAudience",
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
           // GetPrincipalFromExpiredToken(tokenHandler.WriteToken(token), "Harshit6h777b76r65dbw4@@@@567567Harshit6h777b76r65dbw4@@@@567567");
            return tokenHandler.WriteToken(token);
        }
    }
}
