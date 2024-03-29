﻿using Microsoft.AspNetCore.Identity;

namespace MediatRIdentity.DataAccess.Entities {
    public class User : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}