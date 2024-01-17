using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RazorApp.TH.Services.Helpers
{
    public class Commons
    {
        public static async Task<string> ConsumeApiAsync(string url)
        {
         
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
     
        }

         
    }
}
