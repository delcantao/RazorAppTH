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
        public Model.Check.Data Result;
        public Dictionary<string, string> fields = new Dictionary<string, string>();


        public ResultadoModel(ILogger<ResultadoModel> logger, IWebHostEnvironment env, IRazorRenderService renderService)
        {
            _logger = logger;
            _env = env;
            _renderService = renderService;
        }

        public void OnGet()
        {
            LoadSession();
            CarregaModulos();
        }
        public async Task<JsonResult> OnPostConsultaApiAsync() //(string CPF, string NOME, string NOMEADC, string FONE, string EMAIL)
        {
            LoadSession();

            var queryParams = new SortedList<string, string>();         

            foreach(var dado in _info)
            {
                var query = HttpContext.Request.Form.ToList().Where(e => e.Key.Contains(dado.Modulo)).ToList();
                if(query == null || query.Count == 0) continue;
                var value = query.FirstOrDefault().Value.ToString();
                queryParams.Add('p' + dado.Modulo, value); 
            }
            var urlToSend = QueryHelpers.AddQueryString(Statics.UrlCheck, queryParams);

         
            


            // ler a página view referente ao carregamento das tabelas
            // carregar via POST de AJAX

            var jsonApi = await Commons.ConsumeApiAsync(urlToSend);
            Result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Check.Data>(jsonApi);

            var htmlView1 = "" ?? await _renderService.ToStringAsync("_Check_ResultadoView1", Result.CAD);
            var htmlView2 = "" ?? await _renderService.ToStringAsync("_Check_ResultadoView2", Result);

            return await Task.FromResult(
                new JsonResult(
                    new { 
                        isValid = true, 
                        message = "",
                        htmlView1 = htmlView1,
                        htmlView2 = htmlView2
                    }));
        }
        public void OnPost()
        {

        }
        public async Task<JsonResult> OnPostCheckAsync()
        { 
            return await Task.FromResult(new JsonResult(new { isValid = true }));
        }
        public async Task<JsonResult> OnPostCarregaModulos(string[] Modulo)
        {
            HttpContext.Session.SetString("modulos", string.Join(",", Modulo));
            _modulos = Modulo;
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
            _modulos = HttpContext.Session.GetString("modulos").Split(",");
        }
    }
}
