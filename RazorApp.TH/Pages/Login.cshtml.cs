using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 
using System.Collections.Generic; 
using Microsoft.AspNetCore.Http;

namespace RazorAppTH.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        public List<Model.Info.Data> _info;


        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public JsonResult OnGet(string cliente, string usuario, string senha)
        {
            if (!string.IsNullOrEmpty(cliente) &&
                !string.IsNullOrEmpty(usuario) &&
                !string.IsNullOrEmpty(senha))
            {
                HttpContext.Session.SetString("cliente", cliente);
                HttpContext.Session.SetString("usuario", usuario);
                HttpContext.Session.SetString("senha", senha);
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });

        }

    }
}
