namespace RazorApp.TH.Model.OpenGateway
{

     

    public class Alerta
    {
        public Data data { get; set; }

        public class Data
        {
            public Customer customer { get; set; }
        }

        public class Customer
        {
            public Profileevent[] profileEvents { get; set; }
        }

        public class Profileevent
        {
            public string eventName { get; set; }
            public string timestamp { get; set; }
        }

    }



}
