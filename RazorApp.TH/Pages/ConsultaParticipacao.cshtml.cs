using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RazorApp.TH.Model; 
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
using RazorApp.TH.Model.UI;
using RazorApp.TH.Model.ValidMobile;

namespace RazorApp.TH.Pages
{
    public class ParticipacaoPage : PageModel
    {
        public List<Model.Info.Data> _info;
        public string ErrorMessage;
        private readonly ILogger<ParticipacaoPage> _logger;
        private readonly IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        public ParticipacaoPage(ILogger<ParticipacaoPage> logger, IRazorRenderService renderService, IWebHostEnvironment env)
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
                    apiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(GetFields());
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
            var fields = new List<Field>
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
             
             
                
            var product = new ValidMobilePageModel
            {
                Products = new List<Product>
                {
                    // new () { Enabled = true, Url = "NomeDaConsulta", Icon = iconValidaCpf, Nome = "Participação Societária", Tooltip = "Consulta Participação Societária", Campos = participacaoSocietaria },
                    new () { Enabled = true, Url = "BuscaPartSoc", Nome = "Participação Societária", Tooltip = "Consulta Participação Societária", Campos = fields },
                    
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(product.Products);
            HttpContext.Session.SetString("apresentacao_products", json);
            

        }

        private List<Model.Info.Data> GetFields()
        {
            return new List<Info.Data>
            {
                new Info.Data
                {
                    Ativo = true,
                    Descricao = "CPF",
                    Modulo = "sCPF",
                    Obrigatorio = true,
                    Ordem = 1,
                    Tamanho = 22,
                    Observacoes = "Observações",
                    Erros = "Erros",
                    UrlIcone = "fa fa-user-o"
                },
                new Info.Data
                {
                    Ativo = true,
                    Descricao = "CNAE",
                    Modulo = "sCNAE",
                    Obrigatorio = false,
                    Ordem = 2,
                    Tamanho = 22,
                    Observacoes = "Observações",
                    Erros = "Erros",
                    UrlIcone = "fa fa-building"
                }
            };
        }
        public async Task<string> RenderFields(Model.Info.Data data)
        {

            var htmlString = await _renderService.ToStringAsync("_Check_FieldsView", data);
            return htmlString;

        }
    }
}
