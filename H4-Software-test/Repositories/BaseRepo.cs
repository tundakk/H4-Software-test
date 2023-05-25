namespace H4_Software_test.Repositories
{
    using H4_Software_test.Models;

    /// <summary>
    /// The base repository class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : class, new()
    {
        /// <summary>
        /// Public readonly property used as DI for classes.
        /// </summary>
        public readonly aspnetH4Softwaretest3a56d5607b184dc6bb4f36cb9fd57e40Context Context;

        /// <summary>
        /// default constructor for the BaseRepo class.
        /// </summary>
        /// <param name="context"></param>
        public BaseRepo(aspnetH4Softwaretest3a56d5607b184dc6bb4f36cb9fd57e40Context context)
        {
            this.Context = context;
        }

        /// <summary>
        /// The abstract Delete method.
        /// </summary>
        /// <param name="entity"></param>
        public abstract T Delete(T entity);

        /// <summary>
        /// The abstract GetAll method.
        /// </summary>
        public abstract IQueryable<T> GetAll();

        /// <summary>
        /// The abstract Insert method.
        /// </summary>
        public abstract T Insert(T entity);

        /// <summary>
        /// The abstract Update method.
        /// </summary>
        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Update - Entity must not be null");
            }

            var response = Context.SetModified(entity);
            return response;
        }

        /// <summary>
        /// The default Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                var newEx = new Exception($"DAL Save - Could not be completed: {ex.Message}.", ex);
                throw newEx;
            }
        }
    }
}
