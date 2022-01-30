using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
   public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
       where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //Using yukarıdaki usinglerle tamamen farklı ve using amacı garbage collectördeki calıştırıp
            //Belleği temizlemek çünkü NorthwindContext bellekte fazla yer kaplıyor
            //using (NorthwindContext context = new NorthwindContext())
            //{
            //    var addedEntity = context.Entry(entity); bu kodlar tamaen NorthwindContext kodları
            //    addedEntity.State = EntityState.Added;
            //    context.SaveChanges();
            //}
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void DeleTe(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null//filtere null ise 
                     ? context.Set<TEntity>().ToList()//Null ise bu satır çalışacaktır ve bize bir liste dönecektir
                     : context.Set<TEntity>().Where(filter).ToList();//filtireleme var ise bu satırda where koşuluna  göre columlar getiriliecektir.
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
