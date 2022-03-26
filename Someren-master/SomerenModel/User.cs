
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public  class User
    {
        public string Username { get; set; } // for the username
        public string Password { get; set; }   // for the password  
        public string Salt { get; set; } // for the salt    
        public bool Role { get; set; }// 1= admin 0= user


       

    }
}
