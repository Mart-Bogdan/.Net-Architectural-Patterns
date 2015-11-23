using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB.Entity
{
    public class BlogUser : BaseEntity<int>
    {
       public string Nick {get;set;}
       public string UserPassword {get;set;}
       public string Name { get; set; }
    }
}
