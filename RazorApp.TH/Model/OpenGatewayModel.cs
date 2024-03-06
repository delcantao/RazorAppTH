using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorApp.TH.Model.OpenGateway;

namespace RazorApp.TH.Model
{    
    public class OpenGatewayModel
    { 
        public List<Product> Products { get; set; }

        public Score ScoreData { get; set; }
        public Alerta AlertaData { get; set; }
        public ValidaEndereco ValidaEnderecoData { get; set; }
        public ValidaTelefone ValidaTelefoneData { get; set; }


        public class Product
        {
            public string Nome { get; set; }
            public string Tooltip { get; set; }
            public string Icon { get; set; }
            public bool Enabled { get; set; }
            public string Url { get; set; } 
            public List<Field> Campos { get; set; }


            public class Field
            {
                public string Nome { get; set; }
                public string NomeInterno { get; set; }
                public string Tooltip { get; set; }
                public string Placeholder { get; set; }
                public bool Opcional { get; set; }

            }
        }

    }
}
