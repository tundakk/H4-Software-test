namespace H4_Software_test.Controllers
{
    using H4_Software_test.Models;
    using H4_Software_test.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    /// <summary>
    /// Endpoints related to configuring Text Files.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TextFilesController : BaseApiController<TextFilesController>
    {
        private readonly ITextFilesService textFilesService;

        /// <summary>
        /// Default constructor for TextFilesController.
        /// </summary>
        /// <param name="textFilesService"></param>
        /// <param name="logger"></param>
        public TextFilesController(
            ITextFilesService textFilesService,
            ILogger<TextFilesController> logger) : base(logger)
        {
            this.textFilesService = textFilesService;
        }

        /// <summary>
        /// Creates an TextFiles object.
        /// </summary>
        /// <param name="textFile"></param>
        /// <returns>Returns the created TextFiles object.</returns>
        [HttpPost]
        public ActionResult<TextFiles> CreateTextFile(TextFiles textFile)
        {
            if (textFile.FileName == null || textFile.FilePath == null)
            {
                return BadRequest();
            }
            if (textFile.Id <= 0)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            textFile.UserId = userId;
            ServiceResponse<TextFiles> response = textFilesService.Insert(textFile);

            if (!response.Success)
            {
                return Problem();
            }

            if (response.Data == null)
            {
                return NoContent();
            }

            return Ok(response.Data);
        }
    }
}

