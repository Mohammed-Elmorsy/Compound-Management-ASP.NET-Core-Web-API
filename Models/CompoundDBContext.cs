using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitsTaskWebAPI_MohammedElmorsy.Models
{
    public class CompoundDBContext: DbContext
    {
        public CompoundDBContext(DbContextOptions<CompoundDBContext> options): base(options) {}

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
