using SuperDuper.Resources;
using System.Text;

namespace SuperDuper.DAL
{
    internal class TableFormatter
    {

        internal static Result<string> GetTableNameFromModel<T>(T model) where T : class
        {
            var type = typeof(T);
            var tableName = type.Name;

            tableName = tableName.ToSnakeCase();

            return Result<string>.Success(tableName);
        }
    }
}
