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
using Microsoft.AspNetCore.Hosting;

namespace RazorAppTH.Pages
{
    public class CheckModel : PageModel
    {   
        public List<Model.Info.Data> _info;
        private readonly ILogger<CheckModel> _logger;
        private readonly IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        public CheckModel(ILogger<CheckModel> logger, IRazorRenderService renderService, IWebHostEnvironment env)
        {
            _renderService = renderService;
            _logger = logger;
            _env = env;
        }
        public async Task OnGet()
        {
            string apiResponse = HttpContext.Session.GetString("info");
            if (string.IsNullOrEmpty(apiResponse))
            {
                apiResponse = await Commons.ConsumeApiAsync(Statics.UrlWsIth);
                HttpContext.Session.SetString("info", apiResponse);
            }
            _info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(apiResponse);
          

        }
        public async Task<string> RenderFields(Model.Info.Data data)
        {

            var htmlString = await _renderService.ToStringAsync("_Check_FieldsView", data);
            return htmlString;

        }
         
    }
}
