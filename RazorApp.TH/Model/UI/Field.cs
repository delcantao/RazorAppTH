using System.Collections.Generic;

namespace RazorApp.TH.Model.UI
{
    public class Field
    {
        public string Nome { get; set; }
        public string NomeInterno { get; set; }
        public string Tooltip { get; set; }
        public string Placeholder { get; set; }
        public bool Opcional { get; set; }
        public string Type { get; set; } = "text";
        public string InitialValue { get; set; }
        public bool Hidden { get; set; }
        public List<KeyValue> Values { get; set; }

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public KeyValue(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
