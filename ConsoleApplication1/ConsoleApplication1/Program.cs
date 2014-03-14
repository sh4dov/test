using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Entities;
using Factories;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigureContainer();

            var dataRow = CreateDataRow();

            var container = Factory.Get().Create<IUnityContainer>();
            new ConvertersConfiguration().Configure(container);

            var converter = Factory.Get().Create<IValueConverter>();

            var builder = new EntityFactory();
            var entity = builder.Create(new DataRowValueProvider(dataRow, converter));
        }

        private static void ConfigureContainer()
        {
            var unityContainer = new UnityContainer();
            var locator = new UnityServiceLocator(unityContainer);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private static DataRow CreateDataRow()
        {
            var table = new DataTable("Table1");
            table.Columns.Add("ID", typeof(double));
            table.Columns.Add("GroupName", typeof(string));
            table.Columns.Add("GroupId", typeof(string));

            var dataRow = table.NewRow();
            dataRow["ID"] = 10;
            dataRow["GroupName"] = "bla bla bla";
            dataRow["GroupId"] = "100";

            return dataRow;
        }
    }

    public sealed class ConvertersConfiguration
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterType<IValueConverter, ValueConverterAggregate>();
            Register<NotNeedConversionConverter>(container);
            Register<StringToIntConverter>(container);
        }

        private void Register<T>(IUnityContainer container) where T : IValueConverter
        {
            container.RegisterType<IValueConverter, T>(typeof(T).FullName);
        }
    }

    internal interface IValueConverter
    {
        bool CanHandle(Type sourceType, Type resultType);

        object Convert(object value, Type sourceType, Type resultType);
    }

    internal sealed class NotNeedConversionConverter : IValueConverter
    {
        public bool CanHandle(Type sourceType, Type resultType)
        {
            return sourceType == resultType;
        }

        public object Convert(object value, Type sourceType, Type resultType)
        {
            return value;
        }
    }

    internal sealed class StringToIntConverter : IValueConverter
    {
        public bool CanHandle(Type sourceType, Type resultType)
        {
            return sourceType == typeof(string) && resultType == typeof(int);
        }

        public object Convert(object value, Type sourceType, Type resultType)
        {
            return System.Convert.ToInt32(value as string);
        }
    }

    internal sealed class ValueConverterAggregate : IValueConverter
    {
        private readonly IValueConverter[] _converters;

        public ValueConverterAggregate(params IValueConverter[] converters)
        {
            Guard.ArgumentNotNull(converters, "converters");
            _converters = converters;
        }

        public bool CanHandle(Type sourceType, Type resultType)
        {
            return _converters.Any(c => c.CanHandle(sourceType, resultType));
        }

        public object Convert(object value, Type sourceType, Type resultType)
        {
            Debug.Assert(CanHandle(sourceType, resultType), string.Format("Conversion from type {0} to type {1} is not supported yet.", sourceType.FullName, resultType.FullName));

            var converter = _converters.FirstOrDefault(c => c.CanHandle(sourceType, resultType));
            return converter != null ? converter.Convert(value, sourceType, resultType) : value;
        }
    }

    internal sealed class DataRowValueProvider : IValueProvider
    {
        private readonly DataRow _dataRow;
        private readonly IValueConverter _valueConverter;

        public DataRowValueProvider(DataRow dataRow, IValueConverter valueConverter)
        {
            Guard.ArgumentNotNull(dataRow, "dataRow");
            Guard.ArgumentNotNull(valueConverter, "valueConverter");
            _dataRow = dataRow;
            _valueConverter = valueConverter;
        }

        public T GetValue<T>(string columnName)
        {
            if (!_dataRow.Table.Columns.Contains(columnName))
            {
                return default(T);
            }

            var column = _dataRow.Table.Columns[columnName];
            return (T)_valueConverter.Convert(_dataRow[columnName], column.DataType, typeof(T));
        }
    }

    public class Factory
    {
        private readonly IUnityContainer _container;

        public Factory(IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");
            _container = container;
        }

        public static Factory Get()
        {
            var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            return new Factory(container);
        }

        public T Create<T>()
        {
            return _container.Resolve<T>();
        }

        public object Create(Type type)
        {
            Guard.ArgumentNotNull(type, "type");
            return _container.Resolve(type);
        }
    }
}