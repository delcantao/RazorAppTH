using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using RazorApp.TH.Services.Helpers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Web.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using RazorApp.TH.Model;
using System.Xml.Linq; 

namespace RazorApp.TH.Pages
{
    public class ResultadoOpenGatewayPage: PageModel
    {
        public IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        private readonly ILogger<ResultadoOpenGatewayPage> _logger; 
        public Model.OpenGatewayModel.Product Product;
        public string NameProduct { get; set; } = "Check Operadoras";



        public ResultadoOpenGatewayPage(ILogger<ResultadoOpenGatewayPage> logger, IWebHostEnvironment env, IRazorRenderService renderService)
        {
            _logger = logger;
            _env = env;
            _renderService = renderService;
        }

        public IActionResult OnGet(string product)
        {
            // api/BuscaOP010Score?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sCPF={sCPF}
            // api/BuscaOP010ValTel?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}&sCPF={sCPF}
            // api/BuscaOP010ValEnd?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sCEP={sCEP}&sNumeroEndereco={sNumeroEndereco}&sFone={sFone}&sCPF={sCPF}
            // api/BuscaOP010Alerta?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}

            //var a = new List<OpenGateway.Product>
            //{
            //    new () { Enabled = true, Icon = "fa fa-tachometer", Nome = "Score", Tooltip = "Score Tooltip", Campos = new () { new () { Nome = "sCPF" } }},
            //    new () { Enabled = true, Icon = "fa fa-phone", Nome = "Valida Fone", Tooltip = "Valida Fone", Campos = new () { new () { Nome = "sCPF" }, new (){ Nome = "sFone" } }},
            //    new () { Enabled = true, Icon = "fa fa-location-arrow", Nome = "Valida Endereço", Tooltip = "Valida Endereço", Campos = new () { new () { Nome = "sCEP" },  new () { Nome = "sNumeroEndereco" },  new () { Nome = "sCPF", Opcional = true},  new () { Nome = "sFone", Opcional = true } }},
            //    new () { Enabled = true, Icon = "fa fa-exchange", Nome = "Alerta", Tooltip = "Alerta", Campos = new () { new () { Nome = "sFone" } }}
            //};



            LoadProduct(product);

            return Page();
        }

        private void LoadProduct(string product)
        {
            var prodJson = HttpContext.Session.GetString("opengateway_products");
            var prodObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.OpenGatewayModel.Product>>(prodJson);
            Product = prodObj.FirstOrDefault(e => e.Nome.ToLower() == product.ToLower());

        }
        public async Task<JsonResult> OnPostConsultaApiAsync() //(string CPF, string NOME, string NOMEADC, string FONE, string EMAIL)
        {
            try
            {
                var product = HttpContext.Request.Form["product"].ToString();

                LoadProduct(product);

                var htmlView1 = "";
                var htmlView2 = "";
                var queryParams = new SortedList<string, string>
                {
                    { "sCliente", HttpContext.Session.GetString("cliente") },
                    { "sUsuario", HttpContext.Session.GetString("usuario") },
                    { "sSenha", HttpContext.Session.GetString("senha") }
                };


                //foreach (var dado in _info)
                //{
                //    var query = HttpContext.Request.Form.ToList().Where(e => e.Key.Contains(dado.Modulo)).ToList();
                //    if (query == null || query.Count == 0) continue;
                //    var value = query.FirstOrDefault().Value.ToString();
                //    // Mantem o estado do que foi digitado na tela, caso o usuário decida voltar e escolher um campo novo.
                //    HttpContext.Session.SetString(dado.Modulo, value);
                //    queryParams.Add('p' + dado.Modulo, value);
                //}
                
                var formField = HttpContext.Request.Form.ToList();
                foreach (var field in formField)
                {
                    if (field.Key == "product") continue;
                    if (field.Key == "__RequestVerificationToken") continue;
                    
                    queryParams.Add(field.Key, field.Value);
                }
                if (queryParams.ContainsKey("sFone") && !queryParams["sFone"].StartsWith("55")) queryParams["sFone"] = "55" + queryParams["sFone"];


                var urlToSend = QueryHelpers.AddQueryString($"{Statics.BaseUrl}/{Product.Url}", queryParams);
                
                // ler a página view referente ao carregamento das tabelas
                // carregar via POST de AJAX

                var jsonResult = await Commons.ConsumeApiAsync(urlToSend);
                 
                if (jsonResult.Contains("ID_ERRO"))
                {
                    //var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.ErrorModel[]>(jsonResult);
                    //return await Task.FromResult(
                    //    new JsonResult(
                    //        new
                    //        {
                    //            isValid = false,
                    //            message = error[0].MENSAGEM.ToString().Substring(3)
                    //        }));
                    product = "Error";
                }

                var result = new OpenGatewayModel(); 
                switch (product)
                {
                    case "Score":
                        var serializedResult1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OpenGateway.Score[]>(jsonResult);
                        result.ScoreData = serializedResult1[0];
                        htmlView1 = await _renderService.ToStringAsync("_OpenGateway_ScoreResultView", result);
                        break;
                    case "Valida Fone":
                        var serializedResult2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OpenGateway.ValidaTelefone[]>(jsonResult);
                        result.ValidaTelefoneData = serializedResult2[0];
                        htmlView1 = await _renderService.ToStringAsync("_OpenGateway_ValidaTelefoneResultView", result);
                        break;
                    case "Valida Endereço":
                        var serializedResult3 = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OpenGateway.ValidaEndereco[]>(jsonResult);
                        result.ValidaEnderecoData = serializedResult3[0];
                        htmlView1 = await _renderService.ToStringAsync("_OpenGateway_ValidaEnderecoResultView", result);
                        break;
                    case "Alerta":
                        var serializedResult4 = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.OpenGateway.Alerta[]>(jsonResult);
                        result.AlertaData= serializedResult4[0];
                        htmlView1 = await _renderService.ToStringAsync("_OpenGateway_AlertaResultView", result);
                        break;
                    case "Error":
                        var error = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.ErrorModel[]>(jsonResult);                        
                        htmlView1 = await _renderService.ToStringAsync("_OpenGateway_ErrorView", error);
                        break;

                }


                


                return await Task.FromResult(
                    new JsonResult(
                        new
                        {
                            isValid = true,
                            message =  "",
                            htmlView1 = htmlView1,
                            htmlView2 = htmlView2,
                            jsonResult
                        }));

            }
            catch(System.Exception ex)
            {
                return await Task.FromResult(
                   new JsonResult(
                       new
                       {
                           isValid = false,
                           message = "Ocorreu um erro ao realizar a solicitação." + ex.Message,
                           htmlView1 = "",
                           htmlView2 = ""
                       }));
            }
        }


        private void Deserialize()
        {

        }

    }
}
