using System;
using System.Reflection;
using System.Windows.Markup;

namespace WorkWithDB.UI.Extensions
{
    public class This : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var roField = serviceProvider.GetType().GetProperty("System.Xaml.IRootObjectProvider.RootObject", (BindingFlags)~0);
            if (roField != null)
            {
                return roField.GetValue(serviceProvider, new object[0]);
            }

            var field = serviceProvider.GetType().GetField("_context", (BindingFlags)~0);

            if (field == null)
            {
                throw new Exception("!!!! Похоже изменилась версия рaнтайма, требуемый метод не реализован (получение ParserContext из System.Windows.Markup.ProvideValueServiceProvider)");
                return null;
            }
            var context = field.GetValue(serviceProvider);

            if (context == null)
            {
                throw new Exception("!!!! Похоже изменилась версия рaнтайма, требуемый метод не реализован (получение ParserContext из System.Windows.Markup.ProvideValueServiceProvider)");
                return null;
            }

            return context.GetType().GetProperty("RootElement", (BindingFlags)~0).GetValue(context, new object[0]);
        }
    }
}