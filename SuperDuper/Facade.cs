using SuperDuper.DAL;
using SuperDuper.Resources;
using System.Configuration;

namespace SuperDuper
{
    public class Facade
    {
        public static Result<string> BuildQuery<T>(T model, OperationType operationType, bool applyNulls = false) where T : class
        {

            var tableResult = TableFormatter.GetTableNameFromModel(model);
            if(!tableResult.IsSuccess)
                return Result<string>.Failure(tableResult.ErrorMessage);


            var queryResult = QueryFormatter.GetQueryFromModel(model, operationType, tableResult.Value, applyNulls);
            if(!queryResult.IsSuccess)
                return Result<string>.Failure(queryResult.ErrorMessage);

            return Result<string>.Success(queryResult.ErrorMessage);
        }
    }
}
