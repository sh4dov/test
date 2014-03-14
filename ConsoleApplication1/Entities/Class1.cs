using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

namespace Entities
{
    public class Entity
    {
        [Column("ID")]
        public int Id { get; internal set; }

        [Column("GroupName")]
        public string Name { get; internal set; }

        public double Value { get; internal set; }
    }

    public class Entity2
    {
        [Column("GroupId")]
        public int GroupId { get; internal set; }

        [Column("Unknown")]
        public string Unknown { get; internal set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string name)
        {
            Name = name;
            Guard.ArgumentNotNull(name, "name");
        }

        public string Name { get; private set; }
    }

    public interface IValueProvider
    {
        T GetValue<T>(string columnName);
    }

    public sealed class ColumnNameToPropertyMapper
    {
        public Dictionary<string, PropertyInfo> CreateMap(Type type)
        {
            Guard.ArgumentNotNull(type, "type");
            var result = new Dictionary<string, PropertyInfo>();
            var properties = type.GetProperties();
            foreach (var property in properties.Where(p => p.CanWrite))
            {
                var attribute = property.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
                if (attribute != null)
                {
                    result[attribute.Name] = property;
                }
            }

            return result;
        }
    }
}