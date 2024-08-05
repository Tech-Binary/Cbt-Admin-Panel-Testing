
using CbtAdminPanel.Models;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CbtAdminPanel.Controllers
{
    public class BaseController : Controller
    {
        protected readonly MyDbcontext _context;
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly IConfiguration _configuration;

        protected Int64 ClientUserID = 0;
        protected Int64 UserID = 0;
        protected Int64 ClientRegID = 0;
        protected string ClientUserName = "";
        protected string ClientPassword = "";
        protected string ConnectionString = "";
        protected string Version = "";
        //protected ApiResponse response;
        protected string ControllerName;
        protected string token = null;

        public BaseController(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context)
        {

            _hostingEnvironment = hostingEnvironment;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
            _context = context;
            if (_contextAccessor != null && _contextAccessor.HttpContext != null)
            {
                GetDataFromToken();
            }
           // response = new ApiResponse();
        }


        protected void GetDataFromToken()
        {
            if (_contextAccessor != null && _contextAccessor.HttpContext != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                JWTSettings settings = _configuration.GetSection("JWT").Get<JWTSettings>();
                var SecretKey = settings.Secret;
                var key = Encoding.ASCII.GetBytes(SecretKey);
                token = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                token = token.Replace("Bearer ", "");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.ValidIssuer,
                    ValidAudience = settings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                }, out SecurityToken validatedToken);


                if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {

                    throw new SecurityTokenException("Invalid token");
                }

                var jwtToken = (JwtSecurityToken)validatedToken;

                UserID = Int64.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);
                _contextAccessor.HttpContext.Session.SetString("UserID", UserID.ToString());
            }
        }

    }
}
