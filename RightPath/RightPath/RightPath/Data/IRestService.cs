using RightPath.Models;
using RightPath.Models.ResponseModel;
using System.Threading.Tasks;

namespace RightPath.Data
{
	public interface IRestService
	{
        Task<LoginResponse> UserLoginAsync (User user);
        Task<LoginResponse> UserSignupAsync(User user);
	}
}
