using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.DAL.Interface
{
    public interface IDataAccessLayer<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        int saveChanges();
    }
}
