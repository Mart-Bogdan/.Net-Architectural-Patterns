using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using WorkWithDB.UI.Debug;

namespace WorkWithDB.UI.Extensions
{
    [DefaultProperty("OriginalBinding"), ContentProperty("OriginalBinding")]
    public class TrickyBinding : MarkupExtension
    {
        public TrickyBinding() { }

        public TrickyBinding(Binding originalBinding)
        {
            OriginalBinding = originalBinding;
        }

        private BindingBase _originalBinding;

        public BindingBase OriginalBinding
        {
            get { return _originalBinding; }
            set
            {
                if (GuiDebug.InDesignerMode) return;

                _originalBinding = value;
            }
        }

        public Binding Binding
        {
            get
            {
                var trigger = new Trigger();
                BindingOperations.SetBinding(trigger, Trigger.SourceProperty, OriginalBinding);

                return new MyBinding("Target", trigger);
            }
        }

        public Binding b { get { return Binding; } }

        private class Trigger : DependencyObject
        {
            // ReSharper disable UnusedMember.Local
            public static readonly DependencyProperty SourceProperty
                = DependencyProperty.Register("Source", typeof(object), typeof(Trigger),
                                              new PropertyMetadata((d, e) => d.SetValue(TargetProperty, e.NewValue)));

            public static readonly DependencyProperty TargetProperty
                = DependencyProperty.Register("Target", typeof(object), typeof(Trigger));

            public object Source
            {
                get { return GetValue(SourceProperty); }
                set { SetValue(SourceProperty, value); }
            }

            public object Target
            {
                get { return GetValue(TargetProperty); }
                set { SetValue(TargetProperty, value); }
            }

            // ReSharper restore UnusedMember.Local
        }

        private class MyBinding : Binding
        {
            private readonly Trigger _trigger;

            public MyBinding(String path, Trigger source)
                : base(path)
            {
                _trigger = source;
                Source = source;
                Mode = BindingMode.TwoWay;
            }
        }

        #region Overrides of MarkupExtension

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (GuiDebug.InDesignerMode) return null;

            var service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            if (OriginalBinding is Binding && (OriginalBinding as Binding).ElementName != null)
            {
                var _ref = OriginalBinding.GetType().GetProperty("SourceReference", (BindingFlags)(-1)).GetValue(OriginalBinding, new object[0]);
                var method = _ref.GetType().GetMethod("GetObject", (BindingFlags)(-1));

                var _objectRefArgs = Activator.CreateInstance(method.GetParameters()[1].ParameterType);
                var a = method.Invoke(_ref, new[] { service.TargetObject, _objectRefArgs });

                var sourceTypeField = OriginalBinding.GetType().GetField("_sourceInUse", (BindingFlags)(-1));
                sourceTypeField.SetValue(OriginalBinding, (byte)0);
                (OriginalBinding as Binding).Source = a;
            }

            var b = Binding;

            return b.ProvideValue(serviceProvider);
        }

        #endregion Overrides of MarkupExtension

        public static implicit operator BindingBase(TrickyBinding b)
        {
            return b.b;
        }
    }
}