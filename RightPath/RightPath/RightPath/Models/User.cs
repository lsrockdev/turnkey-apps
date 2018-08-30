using System.Collections.Generic;
using System.Linq;
using RightPath.Enums;

namespace RightPath.Models
{
	public class User
	{
		public int id { get; set; }

		public string name { get; set; }

		public string email{ get; set; }

        public string password { get; set; }

        public User(string email, string password){
            this.id = 0;
            this.name = "";
            this.email = email;
            this.password = password;
        }

        public User(string name, string email, string password)
        {
            this.id = 0;
            this.name = name;
            this.email = email;
            this.password = password;
        }

        public User(int id,string name, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
        }
	}
}
