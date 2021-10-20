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
        public string ErrorMessage;
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
            // retirando o estado da sessão que fazia com que carregasse mais rápido
            // desta forma fica mais lento, mas por outro lado é o dado real que está no banco de dados.
            string apiResponse = ""; // HttpContext.Session.GetString("info");
            if (string.IsNullOrEmpty(apiResponse))
            {
                try
                {
                    apiResponse = await Commons.ConsumeApiAsync(Statics.UrlWsIth);
                }
                catch(Exception ex)
                {
                    _info = new List<Info.Data>();
                    ErrorMessage = ex.Message;
                    return;
                }
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
