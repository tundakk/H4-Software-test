namespace H4_Software_test.Service
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Base class for all intergrations services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T>
        where T : BaseService<T>
    {
        /// <summary>
        /// Used to write to the configured logger framework.
        /// </summary>
        protected readonly ILogger<T> Logger;

        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="logger"></param>
        public BaseService(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}

