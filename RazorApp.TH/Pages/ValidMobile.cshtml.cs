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
    public class ValidMobilePage : PageModel
    {
        public ValidMobilePageModel ValidMobileData;
        public string ErrorMessage;
        private readonly ILogger<ValidMobilePage> _logger;
        private readonly IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ValidMobilePage(ILogger<ValidMobilePage> logger, IRazorRenderService renderService, IWebHostEnvironment env, IConfiguration configuration)
        {
            _renderService = renderService;
            _logger = logger;
            _env = env;
            _configuration = configuration;
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
            // api/BuscaNumberIntelligenceSincrono?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}&sCPF={sCPF}&sPeriodoSimSwapEmhoras={sPeriodoSimSwapEmhoras}&sConsentimento={sConsentimento}&sTempoEstimadoDeRetorno={sTempoEstimadoDeRetorno}
            // api/BuscaSIMSwap?sCliente={sCliente}&sUsuario={sUsuario}&sSenha={sSenha}&sFone={sFone}&sPeriodoSimSwapEmhoras={sPeriodoSimSwapEmhoras}&sConsentimento={sConsentimento}
            
            // Valid time periods for Brazil are: 24, 72, 120, 240, 360, 720, 1080 or 2160
            var simSwap = new List<Field>
            {
                new Field
                {
                    Nome = "Celular",
                    NomeInterno = "sFone",
                    Opcional = false,
                    Placeholder = "11 9 9999-9999",
                    //Tooltip = "Digite o número do celular"
                    // InitialValue = "11941009227"
                },
                new Field // Informe o período em horas: 24, 72, 120, 240, 360, 720, 1080 or 2160
                {
                    Nome = "Período de checagem do Swap",
                    NomeInterno = "sPeriodoSimSwapEmhoras",
                    Opcional = false,
                    Placeholder = "15",
                    Tooltip = "Informe o período de 1 a 90 dias", //Se o atributo de troca do SIM for incluído, ele será verificado com o período de horas permitido pelo Mobile Network Operator. O valor deve ser maior que 0.",
                    // //InitialValue = "24",
                    Type = "select",
                    Values = new List<KeyValue>
                    {
                        new("24", "1 dia"),
                        new("72", "3 dias"),
                        new("120", "5 dias"),
                        new("240", "10 dias"),
                        new("360", "15 dias"),
                        new("720", "30 dias"),
                        new("1080", "45 dias"),
                        new("2160", "90 dias"),
                    }
                },
                new Field
                {
                    Nome = "sConsentimento",
                    NomeInterno = "sConsentimento",
                    Opcional = false,
                    Placeholder = "sConsentimento",
                    //Tooltip = "sConsentimento",
                    Type = "hidden",
                    Hidden = true,
                    InitialValue = "true"
                }
            }
            ; //
  

            var numberIntelligenceSincrono = new List<Field>
            {
                new Field
                {
                    Nome = "Celular",
                    NomeInterno = "sFone",
                    Opcional = false,
                    Placeholder = "11 9 9999-9999",
                    //Tooltip = "Informe o número do Celular",
                    
                    // InitialValue = "11941009227"
                },
                new Field
                {
                    Nome = "CPF",
                    NomeInterno = "sCPF",
                    Opcional = false,
                    Placeholder = "000.000.000-00",
                    //Tooltip = "Informe o número do CPF"
                    // InitialValue = "02231613816"
                },
                // new Field // Informe o período em horas: 24, 72, 120, 240, 360, 720, 1080 or 2160
                // {
                //     Nome = "Período de checagem do Swap",
                //     NomeInterno = "sPeriodoSimSwapEmhoras",
                //     Opcional = false,
                //     Placeholder = "15",
                //     Tooltip = "Informe o período de 1 a 90 dias", //Se o atributo de troca do SIM for incluído, ele será verificado com o período de horas permitido pelo Mobile Network Operator. O valor deve ser maior que 0.",
                //     // //InitialValue = "24",
                //     Type = "select",
                //     Hidden = true,
                //     Values = new List<KeyValue>
                //     {
                //         new("", "Não realizar a checagem de SIM Swap"),
                //         new("24", "1 dia"),
                //         new("72", "3 dias"),
                //         new("120", "5 dias"),
                //         new("240", "10 dias"),
                //         new("360", "15 dias"),
                //         new("720", "30 dias"),
                //         new("1080", "45 dias"),
                //         new("2160", "90 dias"),
                //     }
                // },
                new Field
                {
                    Hidden = true,
                    Nome = "Tempo estimado de retorno",
                    NomeInterno = "sTempoEstimadoDeRetorno",
                    Opcional = false,
                    Placeholder = "24",
                    Tooltip = "Informe o tempo estimado de retorno: 24, 72, 120, 240, 360, 720, 1080 or 2160",
                    InitialValue = "2160",
                    Type = "hidden",
                },
                // new Field
                // {
                //     Nome = "sConsentimento",
                //     NomeInterno = "sConsentimento",
                //     Opcional = false,
                //     Placeholder = "sConsentimento",
                //     Tooltip = "sConsentimento",
                //     Type = "hidden",
                //     InitialValue = "true"
                // }
            }
;
            
                //VALID MOBILE:
                //	1. SIMSWAP (SIMSWAP)
                //	2. VALIDACPF (NUMBER INTELLIGENCE)
                //	3. LOCALIZACAO 

            var iconSimSwap = _configuration.GetSection("Icons:MobileNumberValidation:SimSwap").Value ;
            var iconValidaCpf = _configuration.GetSection("Icons:MobileNumberValidation:ValidaCpf").Value;
                
            ValidMobileData = new ValidMobilePageModel
            {
                Products = new List<Product>
                {
                    new () { Enabled = true, Url = "ValidaCPFWHS", Icon = iconValidaCpf, Nome = "Valida CPF", Tooltip = "Valida CPF", Campos = numberIntelligenceSincrono },
                    new () { Enabled = true, Url = "SIMSwap", Icon = iconSimSwap, Nome = "SIM Swap", Tooltip = "SIM Swap", Campos = simSwap },
                    //new () { Enabled = true, Url = "BuscaOP010ValEnd", Icon = "fa fa-location-arrow", Nome = "Valida Endereço", Tooltip = "Valida Endereço", Campos = new () { new () { NomeInterno = "sCEP" , Nome = "CEP" },  new () { NomeInterno = "sNumeroEndereco" , Nome = "Numero do Endereco" },  new () { NomeInterno = "sCPF", Nome = "CPF", Opcional = true},  new () { NomeInterno = "sFone", Nome = "Fone", Opcional = true } }},
                    //new () { Enabled = true, Url = "BuscaOP010Alerta", Icon = "fa fa-exchange", Nome = "Alerta", Tooltip = "Alerta", Campos = new () { new () { NomeInterno = "sFone" , Nome = "Fone" } }}
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ValidMobileData.Products);
            HttpContext.Session.SetString("validmobile_products", json);


            

        }
        public async Task<string> RenderButtons(Product data)
        {

            var htmlString = await _renderService.ToStringAsync("_ValidMobile_ProductsView", data);
            return htmlString;

        }
    }
}
