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
using EmployeesWorkTime.Services;
using EmployeesWorkTime.Domain;
using AutoMapper;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UploadFileController : Controller
    {
        private List<EmployeeCsvRecord> _employeesLoadFromCsvFile = new List<EmployeeCsvRecord>();
        private const string FILES_DIRECTORY = "./FilesFolder/";
        private const string FILE_EXTENSION = "*.csv";
        private readonly IEmployeeService _employeeServices;
        private readonly IMapper _mapper;

        public UploadFileController(IEmployeeService employeeServices, IMapper mapper)
        {
            _employeeServices = employeeServices;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.UploadFile.UPLOAD_FILE)]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                return NotFound();

            var path = FILES_DIRECTORY + uploadedFile.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            return Ok();
        }

        [HttpPost(ApiRoutes.UploadFile.CREATE_EMPLOYEE_RECORDS_IN_DATABASE)]
        public async Task<IActionResult> CreateEmployeRecordsFromCsvFile()
        {
            SearchCsvFilesInDirectoryParseAndDeleteCsvFiles();
            var countAddedRow = 0;

            foreach (var worker in _employeesLoadFromCsvFile)
            {
                var tmpEmployee = _mapper.Map<Employee>(worker);
                tmpEmployee.Id = Guid.NewGuid();
                try
                {
                    await _employeeServices.CreateEmployeeAsync(tmpEmployee);
                    countAddedRow++;
                }
                catch (Exception ex)
                {
                    //todo write to the log file
                    Console.WriteLine(ex.ToString());
                }
            }

            return Ok($"successfully added table rows: {countAddedRow}");
        }

        private void SearchCsvFilesInDirectoryParseAndDeleteCsvFiles()
        {
            _employeesLoadFromCsvFile.Clear();
            var csvFiles = Directory.GetFiles(FILES_DIRECTORY, FILE_EXTENSION);
            foreach (var csvFile in csvFiles)
            {
                ParsingFileAddRecordToEmployees(csvFile);
                try
                {
                    System.IO.File.Delete(csvFile);
                }
                catch (Exception ex)
                {
                    //todo write to the log file
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void ParsingFileAddRecordToEmployees(string fileName)
        {
            var reader = new StreamReader(fileName);
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
        }
    }
}
