using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorApp.TH.Model
{    
    public class Check
    {
        public Data Dados { get; set; }
        public class Data
        {
            public CAD CAD { get; set; }
            public STATUS NOME { get; set; }
            public STATUS NOMEADC { get; set; }
            public STATUS FONE { get; set; }
            public STATUS NASC { get; set; }
            public STATUS EMAIL { get; set; }
            public STATUS MAE { get; set; }
            public string ID_ERRO { get; set; }
            public string MENSAGEM { get; set; }
            public string Message { get; set; }
            public string TIPO_CONSULTA { get; set; }
            public string ERRO_REF { get; set; }
        }

        public class CAD
        {
            public STATUS STATUS { get; set; }
            public string CPF { get; set; }
            public string NOME { get; set; }
            public string NASCIMENTO { get; set; }
            public string SITUACAO { get; set; }
            public string DTLAVRATURAOBITO { get; set; }
        }

        public class STATUS
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }


    }
}
