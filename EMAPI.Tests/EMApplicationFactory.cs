using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace EMAPI.Tests
{
	internal class EMApplicationFactory : WebApplicationFactory<Program>
	{
		
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			Environment.SetEnvironmentVariable("CacheSettings:UseCache", "false");

			// Для подмены сервисов
			_ = builder.ConfigureTestServices(services =>{});

		}
		
	}
}
