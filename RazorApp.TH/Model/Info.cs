using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorAppTH.Model
{
    public class Info
    {
        public List<Data> Dados { get; set; } 
        public class Data
        {
            public string Modulo { get; set; }
            public bool Obrigatorio { get; set; }
            public int Ordem { get; set; }
            public string Descricao { get; set; }
            public string UrlIcone { get; set; }
            public int Tamanho { get; set; }
            public string Observacoes { get; set; }
            public bool Ativo { get; set; }
        }

    }
}
