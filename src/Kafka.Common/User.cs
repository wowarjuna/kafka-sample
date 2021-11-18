using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Common
{
    public class User
    {
        private User()
        {

        }


        public User(string firstName, string lastName)
        {
            this.id = Guid.NewGuid().ToString();
            this.firstName = firstName;
            this.lastName = lastName;
        }
       
        public string id { get;  set; }
       
        public string firstName { get;  set; }
      
        public string lastName { get;  set; }
    }
}
