using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data
{
    public static class GenericExtensions
    {
        public static string GetTableName<TEntity>()
        {
            var type = typeof(TEntity);

            var tableAttr = type.GetCustomAttribute<TableAttribute>();

            return tableAttr == null ? type.Name : tableAttr.Name;
        }

        public static string GetKeyColumnName<TEntity>()
        {
            var properties = typeof(TEntity).GetProperties();

            if (properties?.Any() != true)
                return null;

            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                if (keyAttributes?.Any() != true)
                    continue;

                object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if(columnAttributes?.Any() != true)
                    return property.Name;

                var columnAttribute = (ColumnAttribute)columnAttributes[0];

                return columnAttribute.Name;
            }

            return null;
        }


        public static string GetColumns<TEntity>(bool excludeKey = false)
        {
            var type = typeof(TEntity);

            var columns = string.Join(", ", type.GetProperties()
                          .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                          .Select(p =>
                          {
                              var columnAttr = p.GetCustomAttribute<ColumnAttribute>();

                              return columnAttr != null ? columnAttr.Name : p.Name;
                          }));

            return columns;
        }

        public static string GetPropertyNames<TEntity>(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            var values = String.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));

            return values;
        }

        public static IEnumerable<PropertyInfo> GetProperties<TEntity>(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

            return properties;
        }

        public static string GetKeyPropertyName<TEntity>()
        {
            var properties = typeof(TEntity).GetProperties().Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (properties?.Any() != true)
                return null;

            return properties.FirstOrDefault().Name;
        }
    }
}