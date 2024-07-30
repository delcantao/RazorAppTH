using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorApp.TH.Services.Helpers
{
    public class Commons
    {
        public static async Task<string> ConsumeApiAsync(string url)
        {
                if (url.Contains("NomeDaConsulta"))
                {
                    url = "https://localhost:5001/json/test.json";
                }

            
         
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
     
        }

         
    }
}
