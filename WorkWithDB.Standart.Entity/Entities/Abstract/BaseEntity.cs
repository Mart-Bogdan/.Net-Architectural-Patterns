using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithDB.Standart.Entity.Entities.Abstract
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

    }
}
