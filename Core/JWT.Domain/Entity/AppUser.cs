﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Entity
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
