using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Models;

namespace VisitsTaskWebAPI_MohammedElmorsy.Repositories
{
    public class VisitRepository : Repository<Visit>
    {
        private CompoundDBContext db;
        private DbSet<Visit> Visits;
        public VisitRepository(CompoundDBContext db) : base(db)
        {
            this.db = db;
            Visits = this.db.Set<Visit>();
        }

        public override Visit GetById(int id)
        {
            return Visits.SingleOrDefault(visit => visit.Id == id);
        }

        public override IEnumerable<Visit> GetAll()
        {
            return Visits.Include(visit => visit.Visitor);
        }

        public IEnumerable<Visit> GetUserVisits(int UserId)
        {
            return Visits.Where(visit => visit.UserId == UserId).Include(visit => visit.Visitor);
        }


    }
}
