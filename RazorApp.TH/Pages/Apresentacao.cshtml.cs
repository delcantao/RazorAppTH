using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
using RazorApp.TH.Services.Helpers;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RazorApp.TH.Model.UI;
using RazorApp.TH.Model.ValidMobile;
using static RazorApp.TH.Model.UI.Field;

namespace RazorApp.TH.Pages
{
    public class ApresentacaoPage : PageModel
    {
        public List<Field> _info;
        public ValidMobilePageModel ValidMobileData;
        public string ErrorMessage;
        private readonly ILogger<ValidMobilePage> _logger;
        private readonly IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ApresentacaoPage(ILogger<ValidMobilePage> logger, IRazorRenderService renderService, IWebHostEnvironment env, IConfiguration configuration)
        {
            _renderService = renderService;
            _logger = logger;
            _env = env;
            _configuration = configuration;
        }
        public Task OnGet()
        {
 
            _info = new List<Field>
            {
                new()
                {
                    Nome = "CPF",
                    NomeInterno = "sCPF",
                    Opcional = false,
                    Placeholder = "000.000.000-00",
                    Icon = "fa fa-user-o"
                },
                new()
                {
                    Nome = "CNAE",
                    NomeInterno = "sCNAE",
                    Opcional = true,
                    Placeholder = "6204000", 
                    Icon =  "fa fa-building"
                } 
            };
             
            
            var iconSimSwap = _configuration.GetSection("Icons:MobileNumberValidation:SimSwap").Value ;
            var iconValidaCpf = _configuration.GetSection("Icons:MobileNumberValidation:ValidaCpf").Value;
                
            ValidMobileData = new ValidMobilePageModel
            {
                Products = new List<Product>
                {
                    // new () { Enabled = true, Url = "NomeDaConsulta", Icon = iconValidaCpf, Nome = "Participação Societária", Tooltip = "Consulta Participação Societária", Campos = participacaoSocietaria },
                    new () { Enabled = true, Url = "BuscaPartSoc", Icon = iconValidaCpf, Nome = "Participação Societária", Tooltip = "Consulta Participação Societária", Campos = _info },
                    
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ValidMobileData.Products);
            HttpContext.Session.SetString("apresentacao_products", json);
            return Task.CompletedTask;
        }
        public async Task<string> RenderButtons(Product data)
        {

            
            
            var htmlString = await _renderService.ToStringAsync("_Product_General_View", data);
            return htmlString;

        }
        public async Task<string> RenderFields(Field data)
        {

            var htmlString = await _renderService.ToStringAsync("_Product_General_View", data);
            return htmlString;

        }
    }
}
