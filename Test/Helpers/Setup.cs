using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api;
using minimal_api.Infraestrutura.Db;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using minimal_api.Dominio.Interfaces;
using Test.Mocks;

namespace Test.Helpers
{
  public class Setup
  {
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Startup> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
      Setup.testContext = testContext;
      Setup.http = new WebApplicationFactory<Startup>();

      Setup.http = Setup.http.WithWebHostBuilder(builder =>
      {
        builder.ConfigureServices(services =>
        {
          builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");
            services.AddScoped<IAdministradorServico, AdministradoresServicoMock>();
        });
      });
      Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
      Setup.http.Dispose();
    }
  }
}