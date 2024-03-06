namespace RazorApp.TH.Model.OpenGateway
{ 
    public class ValidaTelefone
    {
        public Data data { get; set; }

        public class Data
        {
            public Result result { get; set; }
        }

        public class Result
        {
            public string summaryMessage { get; set; }
            public string detailedMessage { get; set; }
        }
    }

 

}