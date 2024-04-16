using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorApp.TH.Model.OpenGateway;
using RazorApp.TH.Model.UI;
using RazorApp.TH.Model.ValidMobile.Responses;

namespace RazorApp.TH.Model.ValidMobile
{
    public class ValidMobilePageModel
    {
        public List<Product> Products { get; set; }
        public Model.ValidMobile.Responses.newFormat.ResponseNumberIntelligence NumberIntelligence { get; set; }
        public NewFormat.ResponseSimSwap SIMSwap { get; set; }

        public string Json { get; set; }
        

    }
}
