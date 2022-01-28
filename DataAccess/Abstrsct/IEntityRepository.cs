using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstrsct
{
    //where T:class,IEntity,new() Burada Where koşuluyla kısıltlama veriyoruz class burada refarans tipli
    //Oldugunu belirtiyor IEntity burada yazmamızın sebebi(Örnegin DivideByZeroException sınıflarını) kullanmamızı engelliyor
    //IEntity yer alan sınıfları kullanıyoruz ve new() kullanmamızın sebebi ise classları kullanmamızı saglıyor .
    public interface IEntityRepository<T>where T:class,IEntity,new()
    {
        //Expression<Func<T,bool>> filter=null kodu sayesinde örneğin bir bankacılıkta 
        //tüm hesapları getirmemizi saglıyor filter=null demek tüm hesabı getir demektir.
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        //T Get(Expression<Func<T, bool>>filter);
        //Burada ise tek bir hesabı getiriyor örneğin 50001 nolu hesabı getir sadece demektir.
        T Get(Expression<Func<T, bool>>filter);
        void Add(T entity);
        void Update(T entity);
        void DeleTe(T entity);

    }
}
