using OgrenciBilgiSistemi.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Dal.Interface.Base
{
    public interface IEfRepositoryBase<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetByFilter(Expression<Func<TEntity, bool>> filter);
    }
}
