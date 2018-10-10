using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standard.Entity.Entities;

namespace WebApp.Standard.Api.Models.Responces
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public BlogUser User { get; set; }
    }
}
