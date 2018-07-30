using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace blogs.memoria.helpers
{

    public static class Files
    {
        private static IConfiguration Configuration { get; set; }

        static Files()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public static string LoadInputDataSet()
        {
            return Path.Combine("..","..",Configuration["model:datapath"]);
        }
    }
}