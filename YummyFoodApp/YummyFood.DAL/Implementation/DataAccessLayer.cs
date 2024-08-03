using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.DAL.Interface;

namespace YummyFood.DAL.Implementation
{
    public class DataAccessLayer<TEntity> : IDataAccessLayer<TEntity> where TEntity : class
    {
        protected DbContext db;
        public DataAccessLayer(DbContext _db)
        {
            db = _db;
        }
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entity = db.Set<TEntity>().Find(id);
            if(entity != null)
            {
                db.Set<TEntity>().Remove(entity);
            }
        }

        public TEntity Find(object id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
           return db.Set<TEntity>().ToList();
        }

        public int saveChanges()
        {
            return db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }
    }
}
