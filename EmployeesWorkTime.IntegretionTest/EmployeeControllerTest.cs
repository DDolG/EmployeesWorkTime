using EmployeesWorkTime.Contracts;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesWorkTime.IntegrationTests
{
    public class EmployeeControllerTest : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyEmployee_ReturnsEmptyResponse()
        {
            //Arrange

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Employees.GET_ALL);

            //Assert
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Employee>>(body);
            Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));
            Assert.False(result.Any());
        }

        [Fact]
        public async Task Get_ReturnsEmployee_WhenEmployerExistsInTheDataBase()
        {
            // Arrange
            const string _testData = "test";
            var createEmployee = await CreateEmployeeAsync(new CreateEmployeeRequest { Payroll_Number = _testData });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Employees.GET.Replace("{employeeId}", createEmployee.Id.ToString()));
            var result = await response.Content.ReadAsAsync<Employee>();
            await DeleteEmployeeAsync(createEmployee.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Payroll_Number, _testData);
            Assert.Equal(result.Id, createEmployee.Id);
        }

        [Fact]
        public async Task UpdateEmployee()
        {
            // Arrange
            const string _testData = "test";
            var createEmployee = await CreateEmployeeAsync(new CreateEmployeeRequest());
            var updateEmployee = new UpdateEmployeeRequest() {Id = createEmployee.Id, Payroll_Number = _testData };

            // Act
            var response = await TestClient.PutAsync<UpdateEmployeeRequest>(ApiRoutes.Employees.UPDATE.Replace("{employeeId}", updateEmployee.Id.ToString()), updateEmployee, new JsonMediaTypeFormatter());
            var result = await GetEmployeeAsync(updateEmployee.Id);
            await DeleteEmployeeAsync(updateEmployee.Id);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Payroll_Number, _testData);
            Assert.Equal(result.Id, createEmployee.Id);
        }

        [Fact]
        public async Task DeleteEmployee()
        {
            // Arrange
            var createEmployee = await CreateEmployeeAsync(new CreateEmployeeRequest());
            
            // Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Employees.GET.Replace("{employeeId}", createEmployee.Id.ToString()));
            var result = await GetEmployeeAsync(createEmployee.Id);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.True(result == null);
        }
    }
}
