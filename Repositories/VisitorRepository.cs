using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Models;

namespace VisitsTaskWebAPI_MohammedElmorsy.Repositories
{
    public class VisitorRepository : Repository<Visit> 
    {
        private CompoundDBContext db;
        private DbSet<Visitor> Visitors;
        public VisitorRepository(CompoundDBContext db) : base(db)
        {
            this.db = db;
            Visitors = this.db.Set<Visitor>();
        }

    }
}
