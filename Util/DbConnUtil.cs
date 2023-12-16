using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Util
{
    internal class DbConnUtil
    {
        private static IConfiguration _iconfig;

        static DbConnUtil()
        {
            getAppSettingsFile();
        }

        private static void getAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _iconfig = builder.Build();
        }

        public static string getConnectionString()
        {
            return _iconfig.GetConnectionString("LocalConnectionScreen");
        }
    }
}
