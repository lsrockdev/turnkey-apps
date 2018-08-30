using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RightPath.Models

namespace RightPath.Data
{
	public class RestService : IRestService
	{
		HttpClient client;

		public RestService ()
		{
			client = new HttpClient ();
			client.MaxResponseContentBufferSize = 256000;
		}

        public async Task UserLoginAsync (User user)
		{
		}

        public async Task UserSignupAsync (User user)
		{
		}
	}
}
