using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Abstract.Security
{
    public class UserDescriptor
    {
        public readonly int Id;
        public readonly string Nick;

        public UserDescriptor(int id, string nick)
        {
            Id = id;
            Nick = nick;
        }
    }
}
