using System;
namespace RightPath.Models.ResponseModel
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public int userId { get; set; }
        public string message { get; set; }

        public LoginResponse(string errorMessage){
            success = false;
            userId = 0;
            message = errorMessage;
        }
    }
}
