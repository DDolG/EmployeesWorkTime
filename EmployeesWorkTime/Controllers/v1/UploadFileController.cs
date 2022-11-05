using CsvHelper;
using EmployeesWorkTime.Contracts;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using EmployeesWorkTime.Controllers.v1.Requests;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UploadFileController : Controller
    {
        List<EmployeeCsvRecord> _employeesLoadFromCsvFile = new List<EmployeeCsvRecord>();

        public UploadFileController()
        {

        }

        [HttpPost(ApiRoutes.UploadFile.UPLOAD_FILE)]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            var path = "./FilesFolder/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            return Ok();
        }

        [HttpGet(ApiRoutes.UploadFile.PARSING_FILE)]
        public IActionResult ParsingFileCSV()
        {
            var path = "./FilesFolder/dataset.csv";
            _employeesLoadFromCsvFile.Clear();
            var reader = new StreamReader(path);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            while (csv.Read())
            {
                EmployeeCsvRecord record = null;
                try
                {
                    record = csv.GetRecord<EmployeeCsvRecord>();
                }
                catch (CsvHelperException ex)
                {
                    //todo write to the log file
                    Console.WriteLine(ex.ToString());
                }
                if (record != null)
                    _employeesLoadFromCsvFile.Add(record);
            }

            csv.Dispose();
            reader.Dispose();
            return Ok(_employeesLoadFromCsvFile);
        }
    }
}
