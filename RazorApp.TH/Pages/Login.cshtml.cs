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
                // Preenche as sessões - Isso será suficiente para sabermos que o usuário está logado, pois:
                // 1. O login será autenticado através do SipWeb
                // 2. A cada requisição à API de consumo de dados uma nova autenticação será feita garantindo a segurança.
                
                // Para saber mais sobre o processo que invoca esta página
                // Procure no arquivo do SipWeb no caminho: SipWeb/inc/ajax-ws.php > na função > ajaxLogin()
                HttpContext.Session.SetString("cliente", cliente);
                HttpContext.Session.SetString("usuario", usuario);
                HttpContext.Session.SetString("senha", senha);
                return new JsonResult(new { success = true });
            }
            return new JsonResult(new { success = false });

        }

    }
}
