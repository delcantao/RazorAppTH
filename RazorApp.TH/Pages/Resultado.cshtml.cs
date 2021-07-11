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

namespace RazorAppTH.Pages
{
    public class ResultadoModel : PageModel
    {
        public IWebHostEnvironment _env;
        private readonly ILogger<ResultadoModel> _logger;
        public List<Model.Info.Data> _info;
        
        
        public ResultadoModel(ILogger<ResultadoModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnGet()
        {

         
        }
      

    }
}
