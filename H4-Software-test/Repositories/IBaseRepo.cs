namespace H4_Software_test.Repositories
{
    /// <summary>
    /// Theinterface for the base repository class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepo<T> where T : class
    {
        /// <summary>
        ///  Get all entries in the repo.
        /// </summary>
        /// <returns>Returns all instances of a given entity.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add and entity to repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns object that was created.</returns>
        T Insert(T entity);

        /// <summary>
        /// Updates an exising entity in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns an updated object of the entity.</returns>
        T Update(T entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns an object that was removed.</returns>
        T Delete(T entity);

        /// <summary>
        /// saves changes to repository.
        /// </summary>
        void Save();

    }
}