using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standard.Entity.Entities;


namespace WorkWithDB.DAL.Standard.Abstract
{
    public interface IAuthRepository
    {
        BlogUser Login(string login, string password);
        BlogUser Register(BlogUser user);
    }
}
