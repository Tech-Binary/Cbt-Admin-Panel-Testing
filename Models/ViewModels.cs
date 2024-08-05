namespace CbtAdminPanel.Models
{
    public class ViewModels
    {
    }
    public class ResponseModel
    {
        public string Status { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }


    public class UserModel
    {
        public string Password { get; set; }
        public string UserName { get; set; }

    }

    public class LoginTokenResponse {
        public string token { get; set; }
    }

    public class JWTSettings
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
