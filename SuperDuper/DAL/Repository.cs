using SuperDuper.Resources;

namespace SuperDuper.DAL
{
    internal class Repository
    {

        private Repository()
        {

        }

        internal static Repository GetRepository()
        {
            return new Repository();
        }

        internal Result<T> ExecuteQuery<T>(string query)
        {


            return Result<T>.Success(default);
        }
    }
}
