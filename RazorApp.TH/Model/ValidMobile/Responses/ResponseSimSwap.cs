using System;

namespace RazorApp.TH.Model.ValidMobile.Responses
{

    public class ResponseSimSwap
    {
        public Error error { get; set; }
        public string requestId { get; set; }
        public bool simSwapOccurred { get; set; }
        public NetworkInfo networkInfo { get; set; }

        public class Error
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

        public class NetworkInfo
        {
            public object simSwapTimestamp { get; set; }
            public object simSwapPeriodStartHours { get; set; }
            public object simSwapPeriodEndHours { get; set; }
        }
    }
}