using System.Windows;

namespace Movies
{
    public class DataProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(DataProxy));

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static T Unwrap<T>(object data)
        {
            var proxy = data as DataProxy;
            if (proxy != null)
            {
                return Unwrap<T>(proxy.Data);
            }
            return (T)data;
        }

        public static T Unwrap<T>(object data, T defaultValue)
        {
            var proxy = data as DataProxy;
            if (proxy != null)
            {
                return Unwrap(proxy.Data, defaultValue);
            }
            if (data is T)
            {
                return (T)data;
            }
            return defaultValue;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new DataProxy();
        }
    }
}