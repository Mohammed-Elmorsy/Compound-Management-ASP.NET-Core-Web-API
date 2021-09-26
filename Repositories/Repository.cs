using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitsTaskWebAPI_MohammedElmorsy.Models;

namespace VisitsTaskWebAPI_MohammedElmorsy.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private CompoundDBContext db;
        private DbSet<TEntity> set;

        public Repository(CompoundDBContext _db)
        {
            db = _db;
            set = db.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity Entity)
        {
            set.Add(Entity);
            db.SaveChanges();

            return Entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return set;
        }

        public virtual TEntity GetById(int id)
        {
            return set.Find(id);
        }

        public virtual bool Remove(int id)
        {
            set.Remove(set.Find(id));
            return db.SaveChanges() > 0;
        }

        public virtual TEntity Update(TEntity Entity)
        {
            set.Attach(Entity);
            db.Entry(Entity).State = EntityState.Modified;
            db.SaveChanges();
            return Entity;
        }
    }
}
