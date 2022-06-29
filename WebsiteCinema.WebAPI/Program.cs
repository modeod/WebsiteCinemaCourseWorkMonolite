using Microsoft.AspNetCore;
using System.Text;

namespace WebsiteCinema.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:7259/", "http://localhost:7260/")
                .Build();
    }
}