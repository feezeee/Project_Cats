using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    internal class MyDB : DbContext
    {
        internal DbSet<Cat> Cats { get; set; }

    }
}
