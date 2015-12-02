using System;
using System.Reflection;
using System.Windows.Markup;

namespace WorkWithDB.UI.Extensions
{
    public class Field : MarkupExtension
    {
        private readonly string _fieldName;

        public Field(string fieldName)
        {
            _fieldName = fieldName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var @this = new This().ProvideValue(serviceProvider);
            if (@this == null)
                return @this;
            var field = @this.GetType().GetField(_fieldName, (BindingFlags)~0);
            return field.GetValue(@this);
        }
    }
}