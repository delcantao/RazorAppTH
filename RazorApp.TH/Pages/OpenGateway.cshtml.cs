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
using RazorApp.TH.Model.OpenGateway;
using RazorApp.TH.Model.UI;

namespace RazorApp.TH.Pages
{
    public class OpenGatewayPage : PageModel
    {
        public OpenGatewayModel OpenGatewayData;
        public string ErrorMessage;
        private readonly ILogger<OpenGatewayPage> _logger;
        private readonly IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        public OpenGatewayPage(ILogger<OpenGatewayPage> logger, IRazorRenderService renderService, IWebHostEnvironment env)
        {
            _renderService = renderService;
            _logger = logger;
            _env = env;
        }
        public async Task OnGet()
        {
            // retirando o estado da sessão que fazia com que carregasse mais rápido
            // desta forma fica mais lento, mas por outro lado é o dado real que está no banco de dados.
            //string apiResponse = ""; // HttpContext.Session.GetString("info");
            //if (string.IsNullOrEmpty(apiResponse))
            //{
            //    try
            //    {
            //        apiResponse = await Commons.ConsumeApiAsync(Statics.UrlWsIth);
            //    }
            //    catch(Exception ex)
            //    {
            //        _info = new List<Info.Data>();
            //        ErrorMessage = ex.Message;
            //        return;
            //    }
            //    HttpContext.Session.SetString("info", apiResponse);
            //}
            //_info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(apiResponse);


            // api/BuscaOP010Score?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sCPF={sCPF}
            // api/BuscaOP010ValTel?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}&sCPF={sCPF}
            // api/BuscaOP010ValEnd?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sCEP={sCEP}&sNumeroEndereco={sNumeroEndereco}&sFone={sFone}&sCPF={sCPF}
            // api/BuscaOP010Alerta?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}


            OpenGatewayData = new OpenGatewayModel
            {
                Products = new List<Product>
                {
                    new () { Enabled = true, Url = "BuscaOP010Score", Icon = "fa fa-tachometer", Nome = "Score", Tooltip = "Score Tooltip", Campos = new () { new () { NomeInterno = "sCPF" , Nome = "CPF" } }},
                    new () { Enabled = true, Url = "BuscaOP010ValTel", Icon = "fa fa-phone", Nome = "Valida Fone", Tooltip = "Valida Fone", Campos = new () { new () { NomeInterno = "sCPF" , Nome = "CPF" }, new (){ NomeInterno = "sFone" , Nome = "Fone" } }},
                    new () { Enabled = true, Url = "BuscaOP010ValEnd", Icon = "fa fa-location-arrow", Nome = "Valida Endereço", Tooltip = "Valida Endereço", Campos = new () { new () { NomeInterno = "sCEP" , Nome = "CEP" },  new () { NomeInterno = "sNumeroEndereco" , Nome = "Numero do Endereco" },  new () { NomeInterno = "sCPF", Nome = "CPF", Opcional = true},  new () { NomeInterno = "sFone", Nome = "Fone", Opcional = true } }},
                    new () { Enabled = true, Url = "BuscaOP010Alerta", Icon = "fa fa-exchange", Nome = "Alerta", Tooltip = "Alerta", Campos = new () { new () { NomeInterno = "sFone" , Nome = "Fone" } }}
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(OpenGatewayData.Products);
            HttpContext.Session.SetString("opengateway_products", json);


            

        }
        public async Task<string> RenderButtons(Product data)
        {

            var htmlString = await _renderService.ToStringAsync("_OpenGateway_ProductsView", data);
            return htmlString;

        }
    }
}
