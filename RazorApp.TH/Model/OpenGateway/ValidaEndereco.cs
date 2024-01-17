namespace RazorApp.TH.Model.OpenGateway
{
      
    public class ValidaEndereco
    {
        public Data data { get; set; }
        public class Data
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public string message { get; set; }
            public int score { get; set; }
        }

    }



}
