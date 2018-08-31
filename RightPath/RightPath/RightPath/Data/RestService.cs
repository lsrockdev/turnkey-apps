using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RightPath.Models;
using RightPath.Models.ResponseModel;
using RightPath.Data;


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

        public async Task<LoginResponse> UserLoginAsync (User user)
		{
            try
            {
                var uri = new Uri(string.Format(Constants.RestUrl + "login", ""));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(resultJson);
                    return loginResponse;
                }
                return new LoginResponse("Unknown Error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return new LoginResponse(ex.Message);
            }
		}

        public async Task<LoginResponse> UserSignupAsync (User user)
		{
            try
            {
                var uri = new Uri(string.Format(Constants.RestUrl + "signup", ""));
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var resultJson = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(resultJson);
                    return loginResponse;
                }
                return new LoginResponse("Unknown Error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return new LoginResponse(ex.Message);
            }

		}
	}
}
