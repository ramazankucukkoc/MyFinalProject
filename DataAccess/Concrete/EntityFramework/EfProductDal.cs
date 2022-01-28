using DataAccess.Abstrsct;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //Using yukarıdaki usinglerle tamamen farklı ve using amacı garbage collectördeki calıştırıp
            //Belleği temizlemek çünkü NorthwindContext bellekte fazla yer kaplıyor
            //using (NorthwindContext context = new NorthwindContext())
            //{
            //    var addedEntity = context.Entry(entity); bu kodlar tamaen NorthwindContext kodları
            //    addedEntity.State = EntityState.Added;
            //    context.SaveChanges();
            //}
            using (NorthwindContext context=new NorthwindContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void DeleTe(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }
        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                return filter == null//filtere null ise 
                     ? context.Set<Product>().ToList()//Null ise bu satır çalışacaktır ve bize bir liste dönecektir
                     : context.Set<Product>().Where(filter).ToList();//filtireleme var ise bu satırda where koşuluna  göre columlar getiriliecektir.
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
