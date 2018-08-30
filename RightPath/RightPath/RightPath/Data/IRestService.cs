using System.Collections.Generic;
using System.Linq;
using RightPath.Models;
using RightPath.Enums;

namespace RightPath.Data
{
	public interface IRestService
	{
        Task UserLoginAsync (User user);
        Task UserSignupAsync(User user);
	}
}
