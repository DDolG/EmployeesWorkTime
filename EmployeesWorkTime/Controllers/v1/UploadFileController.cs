using EmployeesWorkTime.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UploadFileController : Controller
    {

        IWebHostEnvironment _appEnvironment;

        public UploadFileController(IWebHostEnvironment appEnvironment)
        {

            _appEnvironment = appEnvironment;
        }

        [HttpPost(ApiRoutes.UploadFile.UPLOAD_FILE)]
        public async Task<IActionResult> AddFileCSV(IFormFile uploadedFile)
        {
            var path = "./FilesFolder/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            return Ok();
        }
    }
}
