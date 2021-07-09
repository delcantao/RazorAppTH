using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorAppTH.Services.Helpers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RazorAppTH.Pages
{
    public class ProdutosModel : PageModel
    {
        private readonly ILogger<ProdutosModel> _logger;
        public List<Model.Info.Data> _info;
        
        
        public ProdutosModel(ILogger<ProdutosModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {

            // Colocar isso em um middleware
            //var cliente = HttpContext.Session.GetString("cliente");
            //var usuario = HttpContext.Session.GetString("usuario");
            //var senha = HttpContext.Session.GetString("senha");


            //if(string.IsNullOrEmpty(cliente) ||
            //    string.IsNullOrEmpty(usuario) ||
            //    string.IsNullOrEmpty(senha)  
            //)
            //{
            //   return Redirect("https://www.sistemasth.com.br");
            //}


            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(Statics.UrlWsIth);
            string apiResponse = await response.Content.ReadAsStringAsync();
            _info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(apiResponse);

            return Page();
        }

        public string CarregaBloco(Model.Info.Data data)
        {
            return @$"
                <div>
                    <h2>{data.Modulo}</h2>
                    <h3>{data.Observacoes}</h3>
                    <h6>{data.Ordem}</h6>
                </div>
                ";
        }
        public string CarregaSeparador(Model.Info.Data data)
        {
            return @$"
                <div>
                    <h2>{data.Modulo}</h2>
                    <h3>{data.Observacoes}</h3>
                    <h6>{data.Ordem}</h6>
                </div>
                ";
        }

        private void GetDataFromApi()
        {
            
        }
    }
}
