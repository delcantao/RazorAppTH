namespace RazorApp.TH.Model.OpenGateway
{


    public class Score
    {
        public Data data { get; set; }
    }
     
       
    

    public class Data
    {
        public Customer customer { get; set; }
        public Statsresult statsResult { get; set; }
    }

    public class Customer
    {
        public string idCpf { get; set; }
    }

    public class Statsresult
    {
        public int score { get; set; }
    }

}
