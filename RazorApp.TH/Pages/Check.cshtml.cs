using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorAppTH.Model; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Web.Services;
using Microsoft.AspNetCore.Http;
using RazorAppTH.Services.Helpers;
using System.Net.Http;

namespace RazorAppTH.Pages
{
    public class IndexModel : PageModel
    {   
        public List<Model.Info.Data> _info;
        private readonly ILogger<IndexModel> _logger;
        private readonly IRazorRenderService _renderService; 
        public IndexModel(ILogger<IndexModel> logger, IRazorRenderService renderService )
        {
            _renderService = renderService;
            _logger = logger; 
        }
        public async Task OnGet()
        {
            //using var httpClient = new HttpClient();
            //using var response = await httpClient.GetAsync(Statics.UrlWsIth);
            //string apiResponse = await response.Content.ReadAsStringAsync();
            //_info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(apiResponse);

            _info = new List<Info.Data> { new Info.Data() };
        }
        public async Task<string> RenderBlocks(Model.Info.Data data)
        {
            var htmlString = await _renderService.ToStringAsync("_Block", data);
            return htmlString;

        }
         
    }
}
