using SuperDuper.Resources;

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
            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {

            }

            foreach (var property in properties)
            {
                property.GetValue(model);
            }

            return Result<string>.Failure("");
        }

        private static Result<string> GetSelect<T>(T model, string table, bool applyNulls)
        {
            return Result<string>.Failure("");
        }

        private static Result<string> GetUpdate<T>(T model, string table, bool applyNulls)
        {
            return Result<string>.Failure("");
        }

        private static Result<string> GetDelete<T>(T model, string table, bool applyNulls)
        {
            return Result<string>.Failure("");
        }
    }
}
