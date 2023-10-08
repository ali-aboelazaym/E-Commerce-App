﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class AppUser:IdentityUser
    {
        //public int AppUserId { get; set; }
        public string DisplayName { get; set; }
        public Adress Adress { get; set; }
    }
}
