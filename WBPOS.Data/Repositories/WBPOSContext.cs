using WBPOS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WBPOS.Data.Repositories
{
     
     public class WBPOSContext : DbContext
    {
        public WBPOSContext(DbContextOptions<WBPOSContext> options)
            : base(options)
        {
        }
          
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; } 
        public DbSet<User> Users { get; set; }  

    }
}
