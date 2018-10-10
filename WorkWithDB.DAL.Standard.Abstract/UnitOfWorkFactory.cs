using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithDB.DAL.Standard.Abstract
{
    [Obsolete]
    public static class UnitOfWorkFactory
    {
        private static Func<IUnitOfWork> _factory;

        public static IUnitOfWork CreateInstance()
        {
            if (_factory == null)
                throw new InvalidOperationException("Библиотека работы с данными не инициализирована");

            return _factory.Invoke();
        }

        public static void __Initialize(Func<IUnitOfWork> factory)
        {
            _factory = factory;
        }

    }
}
