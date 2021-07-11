using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazorAppTH.Services.Helpers

{
    public static class Statics
    {
        public static string UrlWsIth = "https://api3.sistemasth.com.br/api/ith_Check_Modulos?sCodValidacao=$WB$";
        public static string UrlLoginSipWeb = "https://sipweb.sistemasth.com.br/login";
           
        public static List<string> AllowsAnonymousAccess = new List<string>
        {
             "/",
             "/Login",
             "/Check",
             "/Resultado",
             
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
