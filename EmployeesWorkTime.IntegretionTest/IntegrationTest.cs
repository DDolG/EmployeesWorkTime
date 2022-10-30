using EmployeesWorkTime.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Linq;
using EmployeesWorkTime.Controllers.v1.Responses;
using EmployeesWorkTime.Controllers.v1.Requests;
using System.Threading.Tasks;
using EmployeesWorkTime.Contracts;
using System.Net.Http.Json;
using System.Net.Http.Formatting;
using System.Text;
using System;
using EmployeesWorkTime.Domain;

namespace EmployeesWorkTime.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            var dbServices = services.Where(x => ((x.ServiceType.IsGenericType && x.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)))
                                            || (x.ServiceType == (typeof(DbContextOptions)))).Select(y => services.IndexOf(y)).ToList();
                            dbServices.ForEach(x => services.RemoveAt(x));
                            services.AddDbContext<DataContext>(options =>
                           {
                               options.UseInMemoryDatabase("TestDb");
                           });
                        });
                    });
            TestClient = appFactory.CreateClient();
        }

        protected async Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Employees.CREATE, request);
            return await response.Content.ReadAsAsync<EmployeeResponse>();
        }

        protected async Task<EmployeeResponse> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            var response = await TestClient.PutAsync<UpdateEmployeeRequest>(ApiRoutes.Employees.UPDATE.Replace("{employeeId}", request.Id.ToString()), request, new JsonMediaTypeFormatter());
            return await response.Content.ReadAsAsync<EmployeeResponse>();
        }

        protected async Task<Employee> GetEmployeeAsync(Guid employerId)
        {
            var response = await TestClient.GetAsync(ApiRoutes.Employees.GET.Replace("{employeeId}", employerId.ToString()));
            return await response.Content.ReadAsAsync<Employee>();
        }

        protected async Task<Employee> DeleteEmployeeAsync(Guid employerId)
        {
            var response = await TestClient.DeleteAsync(ApiRoutes.Employees.GET.Replace("{employeeId}", employerId.ToString()));
            return await response.Content.ReadAsAsync<Employee>();
        }
    }
}
