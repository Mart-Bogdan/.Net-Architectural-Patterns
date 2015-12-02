using System;
using System.Windows.Markup;

namespace WorkWithDB.UI.Extensions
{
    /// <summary>
    /// Creates new instance of class
    /// </summary>
    public class New : MarkupExtension
    {
        private Type type;

        /// <summary>
        /// Creates new instance of class
        /// </summary>
        /// <param name="t">type of class to instantiate</param>
        public New(Type t)
        {
            type = t;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Activator.CreateInstance(type);
        }
    }
}