using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB.DAL.Abstract
{
    public static class UnitOfWorkFactory
    {
        private static Func<IUnitOfWork> _factory;

        public static IUnitOfWork CreateInstance()
        {
            if(_factory == null)
                throw new InvalidOperationException("Библиотека работы с данными не инициализирована");
         
            return _factory.Invoke();
        }

        public static void __Initialize(Func<IUnitOfWork> factory)
        {
            _factory = factory;
        }
    }
}
