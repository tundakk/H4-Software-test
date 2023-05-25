namespace H4_Software_test.Service
{
    using H4_Software_test.Models;

    public interface ITextFilesService
    {
        /// <summary>
        /// Inserts a new text file into the database.
        /// </summary>
        /// <param name="textFile"></param>
        /// <returns></returns>
        ServiceResponse<TextFiles> Insert(TextFiles textFile);
    }
}