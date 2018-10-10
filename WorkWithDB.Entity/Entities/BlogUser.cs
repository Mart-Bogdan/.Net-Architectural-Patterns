using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB.Entity
{
    public class BlogUser : BaseEntity<int>
    {
        public string Nick { get; set; }
        public string UserPassword { get; set; }
        public string Name { get; set; }

        public BlogUser Clone()
        {
            return (BlogUser)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("BlogUser(Id: {3},Name: {0}, Nick: {1}, UserPassword: {2})", Name, Nick, UserPassword, Id);
        }
    }
}
