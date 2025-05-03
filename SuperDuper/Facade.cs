using SuperDuper.DAL;
using SuperDuper.Resources;

namespace SuperDuper
{
    public class Facade
    {
        public static Result<T> Apply<T>(T model, OperationType operationType, bool applyNulls = false) where T : class
        {
            var tableResult = TableFormatter.GetTableNameFromModel(model);
            if(!tableResult.IsSuccess)
                return Result<T>.Failure(tableResult.ErrorMessage);


            var queryResult = QueryFormatter.GetQueryFromModel(model, operationType, tableResult.Value, applyNulls);
            if(!queryResult.IsSuccess)
                return Result<T>.Failure(queryResult.ErrorMessage);


            var repo = Repository.GetRepository();
            var result = repo.ExecuteQuery<T>(queryResult.Value);
            if(!result.IsSuccess)
                return Result<T>.Failure(result.ErrorMessage);

            return Result<T>.Success(result.Value);
        }
    }
}
