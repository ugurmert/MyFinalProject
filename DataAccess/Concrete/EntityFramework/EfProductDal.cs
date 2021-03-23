using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            //burada using kullanarak kullanımı pahalı olan context'i, blok bitince çöp toplayıcı ile bellekten hemen at demiş oluyoruz.
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity);    //referansı yakala
                addedEntity.State = EntityState.Added;  //ekleme durumu olarak set et
                context.SaveChanges();  //ekle şimdi
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);    //referansı yakala
                deletedEntity.State = EntityState.Deleted;  //silme durumu olarak set et
                context.SaveChanges();  //sil şimdi
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
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);    //referansı yakala
                updatedEntity.State = EntityState.Modified;  //güncelleme durumu olarak set et
                context.SaveChanges();  //güncelle şimdi
            }
        }
    }
}
