namespace H4_Software_test.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// BaseApiController.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>PG: Never directly use this class - inherit from ContentServiceBase or ConfigServiceBase instead!.</remarks>
    public abstract class BaseApiController<T> : ControllerBase
        where T : BaseApiController<T>
    {
        /// <summary>
        /// Reference to ILogger to ensure correct access to logging framework.
        /// </summary>
        protected readonly ILogger<T> Logger;

        /// <summary>
        /// BaseApicontroller default constructor. Contains ILogger. Injection constructor.
        /// </summary>
        /// <param name="logger"></param>
        public BaseApiController(
            ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}
