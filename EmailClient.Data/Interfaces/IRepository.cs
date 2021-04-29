using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Data.Entities;
using LiteDB;

namespace EmailClient.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> FindAll();

        TEntity GetById(int id);

        void Add(TEntity entity);

        bool Update(TEntity entity);

        bool DeleteById(int id);

    }
}
