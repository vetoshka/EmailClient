using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Data.Entities;
using LiteDB;

namespace EmailClient.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> FindAll();

        TEntity GetById(string id);

        void Add(TEntity entity);

        bool Update(TEntity entity);

        bool DeleteById(string id);

    }
}
