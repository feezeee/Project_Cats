﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}