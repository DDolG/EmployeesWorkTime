using CsvHelper.Configuration.Attributes;
using System;

namespace EmployeesWorkTime.Controllers.v1.Requests
{
    public class EmployeeCsvRecordTest
    {
        [Name("Personnel_Records.Payroll_Number")]
        public string Payroll_Number { get; set; }

        [Name("Personnel_Records.Forenames")]
        public string Forenames { get; set; }

        [Name("Personnel_Records.Surname")]
        public string Surname { get; set; }

        [Name("Personnel_Records.Date_of_Birth")]
        [Format("dd/MM/yyyy")]
        public DateTime Date_of_Birth { get; set; }

        [Name("Personnel_Records.Telephone")]
        public string Telephone { get; set; }

        [Name("Personnel_Records.Mobile")]
        public string Mobile { get; set; }

        [Name("Personnel_Records.Address")]
        public string Address { get; set; }

        [Name("Personnel_Records.Address_2")]
        public string Address_2 { get; set; }

    }
}
