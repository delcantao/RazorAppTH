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
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(Statics.UrlWsIth);
                apiResponse = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("info", apiResponse);
            }
            _info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(apiResponse);

            
            if (_info.Count >= 5)
            {
                _info.FirstOrDefault(e => e.Modulo.Equals("CPF")).UrlIcone = "far fa-address-card";
                _info.FirstOrDefault(e => e.Modulo.Equals("NOME")).UrlIcone = "fas fa-user";
                _info.FirstOrDefault(e => e.Modulo.Equals("NOMEADC")).UrlIcone = "fas fa-user-plus";
                _info.FirstOrDefault(e => e.Modulo.Equals("FONE")).UrlIcone = "fas fa-phone-alt";
                //_info.FirstOrDefault(e => e.Modulo.Equals("EMAIL")).UrlIcone = "icon icon-email";
                //_info.FirstOrDefault(e => e.Modulo.Equals("NASC")).UrlIcone = "far fa-calendar-check";
                _info.FirstOrDefault(e => e.Modulo.Equals("PEP")).UrlIcone = "far fa-calendar-check";

            }

        }
        public async Task<string> RenderBlocks(Model.Info.Data data)
        {

            var htmlString = await _renderService.ToStringAsync("_Check_Blocks", data);
            return htmlString;

        }
         
    }
}
