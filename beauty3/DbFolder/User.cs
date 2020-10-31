﻿using Microsoft.AspNetCore.Identity;
namespace beauty3.DbFolder
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public bool? Stats    {get;set;}
        public bool Ban { get; set; } = false;
    }
}
