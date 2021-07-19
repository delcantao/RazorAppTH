using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorAppTH.Model
{    
    public class Check
    {
        public Data Dados { get; set; }
        public class Data
        {
            public CAD CAD { get; set; }
            public NOME NOME { get; set; }
            public NOMEADC NOMEADC { get; set; }
            public FONE FONE { get; set; }
            public NASC NASC { get; set; }
            public EMAIL EMAIL { get; set; }
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

        public class NOME
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }

        public class NOMEADC
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }

        public class FONE
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }

        public class NASC
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }

        public class EMAIL
        {
            public string CODSTATUS { get; set; }
            public string MSGSTATUS { get; set; }
        }

    }
}
