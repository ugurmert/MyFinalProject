using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //burada using kullanarak kullanımı pahalı olan context'i, blok bitince çöp toplayıcı ile bellekten hemen at demiş oluyoruz.
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);        //referansı yakala
                addedEntity.State = EntityState.Added;          //ekleme durumu olarak set et
                context.SaveChanges();                          //ekle şimdi
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);      //referansı yakala
                deletedEntity.State = EntityState.Deleted;      //silme durumu olarak set et
                context.SaveChanges();                          //sil şimdi
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
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);      //referansı yakala
                updatedEntity.State = EntityState.Modified;     //güncelleme durumu olarak set et
                context.SaveChanges();                          //güncelle şimdi
            }
        }
    }
}
