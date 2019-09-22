using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Gherkin_chat
{
    /// <summary>
    /// Gherkin Chat Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Program
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Webhost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
