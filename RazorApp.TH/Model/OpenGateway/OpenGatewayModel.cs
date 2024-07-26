using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorApp.TH.Model.UI;

namespace RazorApp.TH.Model.OpenGateway
{
    public class OpenGatewayModel
    {
        public List<Product> Products { get; set; }

        public Score ScoreData { get; set; }
        public Alerta AlertaData { get; set; }
        public ValidaEndereco ValidaEnderecoData { get; set; }
        public ValidaTelefone ValidaTelefoneData { get; set; }




    }
}
