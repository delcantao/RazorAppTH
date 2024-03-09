using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazorApp.TH.Services.Helpers

{
    public static class Statics
    {
        public static bool IsDev = false;

        /*        
            VALID MOBILE:
	            1. SIMSWAP (SIMSWAP)
	            2. VALIDACPF (NUMBER INTELLIGENCE)
	            3. LOCALIZACAO 
         */
        public static string UrlWsIth = "https://api.sistemasth.com.br/api/ith_Check_Modulos?sCodValidacao=$WB$";
        public static string UrlCheck = "https://api.sistemasth.com.br/api/Check";
        // public static string BaseUrl = IsDev ? "http://localhost:51777/api/" : "https://api6.sistemasth.com.br/api/";
        public static string BaseUrl = "https://api6.sistemasth.com.br/api/";

        //
        public const string UrlLoginSipWeb = "https://sipweb.sistemasth.com.br/login";
        public const string UrlSolucoes = "https://solucoes.sistemasth.com.br/";

        public const string Cliente = "TH";
        public const string Usuario = "TIWS";
        public const string Senha = "TIWSTIWS";
        //?sCliente=th&sUsuario=tiws&sSenha=tiwstiws
        // parameters -> ?sCliente=th&sUsuario=tiws&sSenha=tiwstiws&pCPF=33032041791&pNomeAdc=leonidas%20tiriba&pFone=1129594077&pEmail=leo.tiriba@gmail.com&pNasc=23-07-1954&pNome=douglas%20mathias%20de%20oliveira%20lima"
        public static List<string> AllowsAnonymousAccess = new List<string>
        {
             "/Login",
             //"/",
             //"/Resultado",
             //"/Check"
        };

        public static async Task<string> RenderStaticPage(params string[] path)
        {
            var FilePath = Path.Combine(path);
            if(!File.Exists(FilePath)) return string.Empty;
            var html = await System.IO.File.ReadAllTextAsync(FilePath);
            return html;
        }
    }
}
