using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Api.Models.Requests
{
    public class LoginModel
    {
        public string Nick { get; set; }
        public string Password { get; set; }
    }
}
