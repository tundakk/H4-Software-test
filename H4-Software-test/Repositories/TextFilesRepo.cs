namespace H4_Software_test.Repositories
{
    using H4_Software_test.Models;

    /// <summary>
    /// OBS OBS OBS. Denne klasse må ikke bruges da den anvender Identity Provider databasen. Skal slettes ved lejlighed
    /// </summary>
    public class TextFilesRepo/* : BaseRepo<TextFiles>, ITextFilesRepo*/
    {
        /// <summary>
        /// Default constructor for TextFilesRepo.
        /// </summary>
        /// <param name="Context">Public readonly property on the BaseRepo class.</param>
        public TextFilesRepo(aspnetH4Softwaretest3a56d5607b184dc6bb4f36cb9fd57e40Context Context) /*: base(Context)*/
        {
        }

        //    /// <summary>
        //    /// Deletes an eod price.
        //    /// </summary>
        //    /// <param name="entity"></param>
        //    /// <returns>returns the eod price that was deleted.</returns>
        //    /// <exception cref="ArgumentException"></exception>
        //    public override TextFiles Delete(TextFiles entity)
        //    {
        //        if (entity == null)
        //        {
        //            throw new ArgumentException("Delete - TextFiles must not be null");
        //        }

        //        Context.TextFiles.Remove(entity);
        //        return entity;
        //    }

        //    /// <summary>
        //    /// Gets all entities from TextFiles table.
        //    /// </summary>
        //    /// <returns>returns a populated list of type TextFiles, of all entities in the database.</returns>
        //    public override IQueryable<TextFiles> GetAll()
        //    {
        //        return Context.TextFiles;
        //    }

        //    /// <summary>
        //    /// Get TextFiles object by ID.
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns>Returns a populated TextFiles object by matching ID.</returns>
        //    /// <exception cref="ArgumentException"></exception>
        //    public TextFiles GetById(int id)
        //    {
        //        if (id <= 0)
        //        {
        //            throw new ArgumentException("GetById - id must be greater than 0");
        //        }

        //        try
        //        {
        //            return Context.TextFiles.Find(id);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Context.TextFiles.Find(null);
        //            throw new ArgumentException("GetById -", ex.Message);
        //        }
        //    }

        //    /// <summary>
        //    /// Inserts a TextFile into the database.
        //    /// </summary>
        //    /// <param name="entity"></param>
        //    /// <returns>returns the populated Text file that was inserted.</returns>
        //    /// <exception cref="ArgumentException"></exception>
        //    public override TextFiles Insert(TextFiles entity)
        //    {
        //        if (entity == null)
        //        {
        //            throw new ArgumentException("Insert - TextFiles must not be null");
        //        }

        //        Context.TextFiles.Add(entity);

        //        return entity;
        //    }
        //}
    }
}

