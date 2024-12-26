using Blob.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blob.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobService _blobService;
        public BlobStorageController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload-from-path")]
        public async Task<IActionResult> UploadFileFromPath([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return BadRequest("Caminho do arquivo inválido ou arquivo não encontrado.");
            }

            try
            {
                // Lê o arquivo do caminho especificado
                using var fileStream = System.IO.File.OpenRead(filePath);
                var fileName = Path.GetFileName(filePath);

                // Chama o serviço BlobService para fazer o upload
                var result = await _blobService.UploadAsync(fileStream, fileName, true);

                return Ok(new { Url = result });
            }
            catch (Exception ex)
            {
                // Tratamento de erro
                return StatusCode(500, $"Erro ao fazer upload: {ex.Message}");
            }
        }
    }
}
