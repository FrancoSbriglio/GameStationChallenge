namespace GamesStationChallenge.Middleware
{
    /// <summary>
    ///     RequestProfilerModel
    /// </summary>
    public class RequestProfilerModel
    {
        /// <summary>
        ///     RequestTime
        /// </summary>
        public DateTimeOffset RequestTime { get; set; }

        /// <summary>
        ///     Request
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        ///     Response
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        ///     ResponseTime
        /// </summary>
        public DateTimeOffset ResponseTime { get; set; }
    }
}