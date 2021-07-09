using System;
using System.Collections.Generic;
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
             "/Login",
             "/"
        };
    }
}
