using System;

namespace RazorApp.TH.Model.ValidMobile.Responses
{
    public class NewFormat
    {
        public class ResponseSimSwap
        {
            public Informacoes_de_entrada Informacoes_de_entrada { get; set; }
            public Informacoes_de_retorno Informacoes_de_retorno { get; set; }
            public string Data_envio_informacoes { get; set; }
            public string Data_retorno_informacoes { get; set; }
        }

        public class Informacoes_de_entrada
        {
            public string fone { get; set; }
            public string periodoSimSwap { get; set; }
        }

        public class Informacoes_de_retorno
        {
            public bool? simSwapOccurred { get; set; }
            public NetworkInfo networkInfo { get; set; }
            public Error error { get; set; }
        }

        public class NetworkInfo
        {
            public DateTime? simSwapTimestamp { get; set; }
        }

        public class Error
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
    }

    public class ResponseSimSwap
    {
        public Error error { get; set; }
        public string requestId { get; set; }
        public bool simSwapOccurred { get; set; }
        public NetworkInfo networkInfo { get; set; }

        public class Error
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

        public class NetworkInfo
        {
            public object simSwapTimestamp { get; set; }
            public object simSwapPeriodStartHours { get; set; }
            public object simSwapPeriodEndHours { get; set; }
        }
    }
}