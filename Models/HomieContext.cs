using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homie_backend_test.Models
{
    public class HomieContext : DbContext
    {
        public HomieContext(DbContextOptions<HomieContext> options) : base(options)
        {

        }

        public DbSet<Partners> Partners { get; set; }
        public DbSet<Propertys> Propertys { set; get; }
        public DbSet<Tenants> Tenants {set;get;}
    }
}