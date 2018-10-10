﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standart.Entity.Entities.Abstract;

namespace WorkWithDB.Standart.Entity.Entities
{
    public class BlogPost : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Created { get; set; }

        public BlogPost Clone()
        {
            return (BlogPost)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("BlogPost(Id: {3}, Created: {1}, UserId: {2})", null, Created, UserId, Id);
        }
    }
}
