using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 
using System.Collections.Generic; 
using Microsoft.AspNetCore.Http;
using RazorApp.TH.Security;
using System;
using RazorApp.TH.Services.Helpers;

namespace RazorApp.TH.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        public List<Model.Info.Data> _info;


        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet(string cliente, string usuario, string senha)
        {


            if (Statics.IsDev)
            {
                cliente = "TH";
                usuario = "TI";
                senha = "TIWSTIWS";
            }
            if (!string.IsNullOrEmpty(cliente) &&
                !string.IsNullOrEmpty(usuario) &&
                !string.IsNullOrEmpty(senha))
            {
                // Preenche as sessões - Isso será suficiente para sabermos que o usuário está logado, pois:
                // 1. O login será autenticado através do SipWeb
                // 2. A cada requisição à API de consumo de dados fará uma nova autenticação garantindo a segurança.

                // Para saber mais sobre o recurso que invoca esta página
                // Procure no arquivo do SipWeb no caminho: SipWeb/inc/ajax-ws.php > na função > ajaxLogin() 

                //cliente = Encryption.OpenSSLDecrypt(cliente, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");
                //usuario = Encryption.OpenSSLDecrypt(usuario, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");
                //senha = Encryption.OpenSSLDecrypt(senha, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");

                HttpContext.Session.SetString("cliente", cliente);
                HttpContext.Session.SetString("usuario", usuario + "WS");
                HttpContext.Session.SetString("senha", senha);

                //https://solucoes.sistemasth.com.br/login-check?login=123
                return Statics.IsDev ? Redirect("/apresentacao") : Redirect(Statics.UrlSolucoes + $"/login-check?login={cliente}");
            }
            return Redirect(Services.Helpers.Statics.UrlLoginSipWeb);

        }
        public IActionResult OnGetOriginal(string cliente, string usuario, string senha)
        {
             
            if (!string.IsNullOrEmpty(cliente) &&
                !string.IsNullOrEmpty(usuario) &&
                !string.IsNullOrEmpty(senha))
            {
                // Preenche as sessões - Isso será suficiente para sabermos que o usuário está logado, pois:
                // 1. O login será autenticado através do SipWeb
                // 2. A cada requisição à API de consumo de dados fará uma nova autenticação garantindo a segurança.

                // Para saber mais sobre o recurso que invoca esta página
                // Procure no arquivo do SipWeb no caminho: SipWeb/inc/ajax-ws.php > na função > ajaxLogin() 

                //cliente = Encryption.OpenSSLDecrypt(cliente, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");
                //usuario = Encryption.OpenSSLDecrypt(usuario, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");
                //senha = Encryption.OpenSSLDecrypt(senha, "eitTvbiVwYX15YVvFFkqemD0gUL4CX");

                HttpContext.Session.SetString("cliente", cliente);
                HttpContext.Session.SetString("usuario", usuario + "WS");
                HttpContext.Session.SetString("senha", senha);

                //https://solucoes.sistemasth.com.br/login-check?login=123
                return Redirect(Services.Helpers.Statics.UrlSolucoes + $"/login-check?login={cliente}");
            }
            return Redirect(Services.Helpers.Statics.UrlLoginSipWeb);

        }
    }
}
