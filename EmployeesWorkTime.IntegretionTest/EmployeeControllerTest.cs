using EmployeesWorkTime.Contracts;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesWorkTime.IntegrationTests
{
    public class EmployeeControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            //Arrange

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Employees.GET_ALL);

            //Assert
            Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));

            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Employee>>(body);
            Assert.True(!result.Any());
        }

        [Fact]
        public async Task Get_ReturnsEmployer_WhenEmployerExistsInTheDataBase()
        {
            // Arrange
            const string _testData = "test";
            var createEmployee = await CreateEmployeeAsync(new CreateEmployeeRequest{ Payroll_Number = _testData });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Employees.GET.Replace("{employeeId}", createEmployee.Id.ToString()));
            var result =  await response.Content.ReadAsAsync<Employee>();

            // Assert
            Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));
            Assert.Equal(result.Payroll_Number,_testData);
            Assert.Equal(result.Id, createEmployee.Id);
        }
    }
}
