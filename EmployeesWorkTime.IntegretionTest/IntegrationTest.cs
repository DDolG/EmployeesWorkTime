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
    }
}
