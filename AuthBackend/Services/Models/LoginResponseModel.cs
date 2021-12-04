using System.Collections.Generic;
namespace Service.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public bool ResetPassword { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public LoginResponseModel()
        {
        }

    }
}
