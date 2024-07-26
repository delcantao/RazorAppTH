using System.Collections.Generic;

namespace RazorApp.TH.Model.UI
{
    public class Product
    {
        public string Nome { get; set; }
        public string Tooltip { get; set; }
        public string Icon { get; set; }
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public List<Field> Campos { get; set; }

    }
}