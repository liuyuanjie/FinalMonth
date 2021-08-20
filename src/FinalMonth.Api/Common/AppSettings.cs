using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinalMonth.Api.Common
{
    public class AppSettings
    {
        private static IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public static IConfiguration Current => _configuration; 
    }
}
