using SuperDuper.Resources;
using System.Reflection;

namespace SuperDuper.DAL
{
    internal class QueryFormatter
    {
        internal static Result<string> GetQueryFromModel<T>(T model, OperationType operation, string table, bool applyNulls)
        {

            Result<string> result = null;

            switch (operation)
            {
                case OperationType.Insert:
                    result = GetInsert(model, table, applyNulls);
                    break;
                case OperationType.Select:
                    result = GetSelect(model, table, applyNulls);
                    break;
                case OperationType.Update:
                    result = GetUpdate(model, table, applyNulls);
                    break;
                case OperationType.Delete:
                    result = GetDelete(model, table, applyNulls);
                    break;
            }


            return result ?? Result<string>.Failure("");
        }

        private static Result<string> GetInsert<T>(T model, string table, bool applyNulls)
        {
            var properties = GetProperties(model, applyNulls);

            string query = "INSERT INTO {0} ({1}) VALUES ({2});";

            string fields = GetFields(properties);
            string values = GetValues(properties);

            query = string.Format(query, table, fields, values);

            return Result<string>.Success(query);
        }

        private static Result<string> GetSelect<T>(T model, string table, bool applyNulls)
        {
            var properties = GetProperties(model, applyNulls);

            string query = "SELECT {0} FROM {1}";

            string fields = GetFields(properties);
            string filters = GetFilters(model, properties);

            query = string.Format(query, fields, table);
            
            if (!string.IsNullOrEmpty(filters))
                query += $" WHERE {filters}";

            return Result<string>.Success(query);
        }

        private static Result<string> GetUpdate<T>(T model, string table, bool applyNulls)
        {
            var properties = GetProperties(model, applyNulls);

            string query = "UPDATE {0} SET {1}";

            string fields = GetFields(properties);
            string values = GetFieldWithValues(properties);
            string filters = GetFilters(model, properties);

            query = string.Format(query, table, values);

            if(!string.IsNullOrEmpty(filters))
                query += $" WHERE {filters}";

            query += ";";

            return Result<string>.Success(query);
        }

        private static Result<string> GetDelete<T>(T model, string table, bool applyNulls)
        {
            var properties = GetProperties(model, applyNulls);

            string query = "DELETE FROM {0}";

            string fields = GetFields(properties);
            string filters = GetFilters(model, properties);

            query = string.Format(query, table);

            if (!string.IsNullOrEmpty(filters))
                query += $" WHERE {filters}";

            query += ";";

            return Result<string>.Success(query);
        }

        private static PropertyInfo[] GetProperties<T>(T model, bool applyNulls)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            if (!applyNulls)
                properties = properties.Where(p => p.GetValue(model) != null).ToArray();

            return properties;
        }

        private static string GetFields(PropertyInfo[] properties)
        {
            string fields = string.Empty;

            // Remove Filters property because is a key word.
            properties = properties.Where(p => p.Name != "Filters").ToArray();

            foreach (var property in properties)
            {
                fields += $"{property.Name.ToSnakeCase()}, ";
            }
            fields = fields.Substring(0, fields.LastIndexOf(','));

            return fields;
        }

        private static string GetValues(PropertyInfo[] properties)
        {
            string values = string.Empty;

            // Remove Filters property because is a key word.
            properties = properties.Where(p => p.Name != "Filters").ToArray();

            foreach (var property in properties)
            {
                values += $"@{property.Name}, ";
            }
            values = values.Substring(0, values.LastIndexOf(","));

            return values;
        }

        private static string GetFieldWithValues(PropertyInfo[] properties)
        {
            string values = string.Empty;

            // Remove Filters property because is a key word.
            properties = properties.Where(p => p.Name != "Filters").ToArray();

            foreach (var property in properties)
            {
                values += $"{property.Name} = @{property.Name}, ";
            }
            values = values.Substring(0, values.LastIndexOf(","));

            return values;
        }

        private static string GetFilters<T>(T model, PropertyInfo[] properties)
        {
            string filters = string.Empty;

            var filterProperty = properties.Where(p => p.Name == "Filters").FirstOrDefault();
            if(filterProperty == null)
                return filters;

            var values = filterProperty.GetValue(model);
            if (values == null || values.GetType() != typeof(string[]))
                return filters;

            string[] list = values != null ? (string[])values : Array.Empty<string>();
            foreach(var currentFilter in list)
                filters += $"{currentFilter} = @{currentFilter} AND ";
            filters = filters.Substring(0, filters.Length - 4);

            return filters;

        }
    }
}
