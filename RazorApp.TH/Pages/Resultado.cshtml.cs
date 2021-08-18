using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorAppTH.Services.Helpers;
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

namespace RazorAppTH.Pages
{
    public class ResultadoModel : PageModel
    {
        public IRazorRenderService _renderService;
        public IWebHostEnvironment _env;
        private readonly ILogger<ResultadoModel> _logger;
        private string[] _modulos { get; set; }
        public List<Model.Info.Data> _info;
        public List<Model.Info.Data> Fields;
        public List<Model.Check.Data> Result;
        public Dictionary<string, string> fields = new Dictionary<string, string>();


        public ResultadoModel(ILogger<ResultadoModel> logger, IWebHostEnvironment env, IRazorRenderService renderService)
        {
            _logger = logger;
            _env = env;
            _renderService = renderService;
        }

        public IActionResult OnGet()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("modulos")))
                return Redirect("./Check");
            LoadSession();
            CarregaModulos();
            return Page();
        }
        public async Task<JsonResult> OnPostConsultaApiAsync() //(string CPF, string NOME, string NOMEADC, string FONE, string EMAIL)
        {
            try
            {
                LoadSession();
             
                var htmlView1 = "";
                var htmlView2 = "";
                var queryParams = new SortedList<string, string>
                {
                    { "sCliente", HttpContext.Session.GetString("cliente") },
                    { "sUsuario", HttpContext.Session.GetString("usuario") },
                    { "sSenha", HttpContext.Session.GetString("senha") }
                };
                //var queryParams = new SortedList<string, string>
                //{
                //    { "sCliente", Statics.Cliente },
                //    { "sUsuario", Statics.Usuario },
                //    { "sSenha", Statics.Senha }
                //};


                foreach(var dado in _info)
                {
                    var query = HttpContext.Request.Form.ToList().Where(e => e.Key.Contains(dado.Modulo)).ToList();
                    if(query == null || query.Count == 0) continue;
                    var value = query.FirstOrDefault().Value.ToString();
                    // Mantem o estado do que foi digitado na tela, caso o usuário decida voltar e escolher um campo novo.
                    HttpContext.Session.SetString(dado.Modulo, value);
                    queryParams.Add('p' + dado.Modulo, value);
                }

                var urlToSend = QueryHelpers.AddQueryString(Statics.UrlCheck, queryParams);
                
                // ler a página view referente ao carregamento das tabelas
                // carregar via POST de AJAX

                var jsonApi = await Commons.ConsumeApiAsync(urlToSend);
                Result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Check.Data>>(jsonApi);
                var message = Result[0].MENSAGEM;             


                if(string.IsNullOrEmpty(message))
                {
                    htmlView1 = await _renderService.ToStringAsync("_Check_ResultadoView1", Result[0].CAD);
                    htmlView2 = await _renderService.ToStringAsync("_Check_ResultadoView2", Result[0]);
                }

                return await Task.FromResult(
                    new JsonResult(
                        new
                        {
                            isValid = string.IsNullOrEmpty(message),
                            message = message ?? "",
                            htmlView1 = htmlView1,
                            htmlView2 = htmlView2
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

        public async Task<JsonResult> OnPostLimparAsync()
        {
            LoadSession();
            if (_info != null) _info.ForEach(e => HttpContext.Session.SetString(e.Modulo, ""));
            return await Task.FromResult(new JsonResult(new { isValid = true }));
        }
        public async Task<JsonResult> OnPostCheckAsync()
        {
            return await Task.FromResult(new JsonResult(new { isValid = true }));
        }
        public async Task<JsonResult> OnPostCarregaModulos(List<string> Modulo)
        {

            if(!Modulo.Contains("CPF")) Modulo.Insert(0, "CPF");
            HttpContext.Session.SetString("modulos", string.Join(",", Modulo.ToArray()));
            _modulos = Modulo.ToArray();
            LoadSession();
            CarregaModulos();
            return await Task.FromResult(new JsonResult(new { isValid = true }));

        }
        private void CarregaModulos()
        {
            Fields = new List<Model.Info.Data>();
            foreach(var modulo in _modulos)
            {
                if(string.IsNullOrEmpty(modulo)) continue;
                var field = _info.SingleOrDefault(e => e.Modulo.Equals(modulo));
                Fields.Add(field);
            }

        }
        private void LoadSession()
        {
            var info = HttpContext.Session.GetString("info");
            if(!string.IsNullOrEmpty(info))
                _info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(info);
            //HttpContext.Session.Remove("modulos");
            _modulos = HttpContext.Session.GetString("modulos") != null ? HttpContext.Session.GetString("modulos").Split(",") : null;
        }
    }
}
