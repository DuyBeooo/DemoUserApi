using System;

namespace MyAPI3.Models
{
    public class LoginRespondFormat
    {
        public Data Data { get; internal set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public int StatudeCode { get; set; }
    }

    public class Data
    {
        public string AccessToken { get; set; }
        public string TypeToken { get; set; }

    }

    public class Main
    {
        LoginRespondFormat l = new()
        {
            Data = new Data() { AccessToken = "aaa", TypeToken = "bbb" },
            Error = false,
            Message = "No mess",
            StatudeCode = 200
        };

    }
}
