﻿using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthorizationService
    {
        Task<Authorization> Authenticate(string login, string password);

    }
}
