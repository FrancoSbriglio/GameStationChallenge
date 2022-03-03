namespace GamesStationChallenge.Exception
{
    public static class ExceptionExtensions
    {


        public static IEnumerable<System.Exception> GetAllExceptions(this System.Exception exception)
        {
            yield return exception;

            if (exception is AggregateException aggrEx)
            {
                foreach (System.Exception innerEx in aggrEx.InnerExceptions.SelectMany(e => e.GetAllExceptions()))
                {
                    yield return innerEx;
                }
            }
            else if (exception.InnerException != null)
            {
                foreach (System.Exception innerEx in exception.InnerException.GetAllExceptions())
                {
                    yield return innerEx;
                }
            }
        }
    }
}
