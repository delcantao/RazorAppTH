using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorAppTH.Services.Helpers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RazorAppTH.Pages
{
    public class ProdutosModel : PageModel
    {
        private readonly ILogger<ProdutosModel> _logger;
        public List<Model.Info.Data> _info;
        
        
        public ProdutosModel(ILogger<ProdutosModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

         
        }
        public async Task<string> LoadScripts()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + Statics.UrlResultadoScripts);
            string apiResponse = await response.Content.ReadAsStringAsync();
            
            return apiResponse;

        }
    }
}
