using System.ComponentModel.DataAnnotations;

namespace WorkWithDB.Standard.Entity.Entities.Abstract
{
    public class BaseEntity<TKey>
    {
        [System.ComponentModel.DataAnnotations.ScaffoldColumn(false)] 
        public TKey Id { get; set; }

    }
}
