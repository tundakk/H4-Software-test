namespace H4_Software_test.Service
{
    using H4_Software_test.Models;
    using H4_Software_test.Repositories;

    public class TextFilesService : BaseService<TextFilesService>, ITextFilesService
    {
        private readonly ITextFilesRepo textFilesRepo;

        public TextFilesService(ITextFilesRepo textFilesRepo, ILogger<TextFilesService> logger) : base(logger)
        {
            this.textFilesRepo = textFilesRepo;

        }
        public ServiceResponse<TextFiles> Insert(TextFiles textFile)
        {
            if (textFile == null)
            {
                return new ServiceResponse<TextFiles>()
                {
                    Success = false,
                    Message = "Text File cannot be null"
                };
            }

            TextFiles responseTextFiles = this.textFilesRepo.Insert(textFile);

            this.textFilesRepo.Save();

            return new ServiceResponse<TextFiles>()
            {
                Data = textFile
            };
        }
    }
}
