using System;
using System.ComponentModel.DataAnnotations;
using WorkWithDB.Standard.Entity.Entities.Abstract;

namespace WorkWithDB.Standard.Entity.Entities
{
    public class BlogPost : BaseEntity<int>
    {
        public int ? UserId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.DateTime)]
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
