using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Standard.Api.Models.Requests
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
    }
}
