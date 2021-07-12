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

namespace RazorAppTH.Pages
{
    public class ResultadoModel : PageModel
    {
        public IWebHostEnvironment _env;
        private readonly ILogger<ResultadoModel> _logger;
        public List<Model.Info.Data> _info;
        public List<Model.Info.Data> Fields;
        
        public ResultadoModel(ILogger<ResultadoModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
          
        }

        public void OnGet()
        {
            LoadSession();
            var modulos = HttpContext.Session.GetString("modulos").Split(",");            
            CarregaModulos(modulos);
        }
        public void OnPostCarregaModulos(string[] Modulo)
        {
            LoadSession();
            HttpContext.Session.SetString("modulos", string.Join(",", Modulo));
            CarregaModulos(Modulo);

        }
        private void CarregaModulos(string[] modulos)
        {
            Fields = new List<Model.Info.Data>();
            foreach(var modulo in modulos)
            {
                var field = _info.SingleOrDefault(e => e.Modulo.Equals(modulo));
                Fields.Add(field);
            }

        }
        private void LoadSession()
        {
            var info = HttpContext.Session.GetString("info");
            if(!string.IsNullOrEmpty(info))
                _info = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.Info.Data>>(info);

        }
    }
}
