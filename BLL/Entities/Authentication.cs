﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Authentication
    {
        public string Login { get; set; }
        public string Password { get; set; }  
        public string IpAddress { get; set; }
    }
}
