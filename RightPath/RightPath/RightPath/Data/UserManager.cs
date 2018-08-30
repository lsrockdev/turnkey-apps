using System;
using RightPath.Models;
using System.Threading.Tasks;
using RightPath.Models.ResponseModel;

namespace RightPath.Data
{
    public class UserManager
    {
        IRestService restService;
        public UserManager(IRestService service)
        {
            restService = service;
        }

        public Task<LoginResponse> LoginTaskAsync(User user)
        {
            return restService.UserLoginAsync(user);
        }
    }
}
