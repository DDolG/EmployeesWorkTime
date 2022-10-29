using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeesWorkTime.Data.Migrations
{
    public partial class AddedEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payroll_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Forenames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_of_Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail_Home = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employers");
        }
    }
}
