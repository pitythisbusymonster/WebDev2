
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace AmberTurnerSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseDefaultServiceProvider(           // added this
                        options => options.ValidateScopes = false);
                });
    }
}
