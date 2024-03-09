using System;

namespace RazorApp.TH.Model.ValidMobile.Responses
{ 
   
 
    public class ResponseNumberIntelligence
    {
        public string Id { get; set; }
        public string Informacoes_de_entrada { get; set; }
        public Informacoes_de_retornox Informacoes_de_retorno { get; set; }
        public string Data_envio_informacoes { get; set; }
        public string Data_retorno_informacoes { get; set; }

        public class Informacoes_de_retornox
        {
            public string token { get; set; }
            public NiAttributes niAttributes { get; set; }
        }

        public class NiAttributes
        {
            public SimSwap simSwap { get; set; }
            public NationalIdentityNumber nationalIdentityNumber { get; set; }
        }

        public class SimSwap
        {
            public bool? simSwapOccurred { get; set; }
            public NetworkInfo networkInfo { get; set; }
            public Error error { get; set; }
        }

        public class NetworkInfo
        {
            public object simSwapTimestamp { get; set; }
            public object simSwapPeriodStartHours { get; set; }
            public object simSwapPeriodEndHours { get; set; }
        }

        public class Error
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

        public class NationalIdentityNumber
        {
            public string match { get; set; }
            public Error1 error { get; set; }
        }

        public class Error1
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
    }
}