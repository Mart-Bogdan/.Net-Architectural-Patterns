using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB.Entity
{
    public class BlogPost : BaseEntity<int>
    {
        public int UserId {get;set;} 
        public string Content {get;set;}
        public DateTimeOffset Created { get; set; }

        public override string ToString()
        {
            return string.Format("BlogPost(Id: {3}, Created: {1}, UserId: {2})", null, Created, UserId, Id);
        }
    }
}
