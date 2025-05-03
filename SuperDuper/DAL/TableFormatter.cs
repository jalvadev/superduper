using SuperDuper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.DAL
{
    internal class TableFormatter
    {

        internal static Result<string> GetTableNameFromModel<T>(T model) where T : class
        {
            var type = typeof(T);
            var tableName = type.Name;

            // TODO: Formatear a plural (?)

            // TODO: Formatear a snake_case.

            return Result<string>.Success("");
        }
    }
}
