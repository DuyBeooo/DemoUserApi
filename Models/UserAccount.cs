using System;
using System.Collections.Generic;

#nullable disable

namespace MyAPI.Models
{
    public partial class UserAccount
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public  class RegisterAcc
    {
        public string UserName { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
    }
}
