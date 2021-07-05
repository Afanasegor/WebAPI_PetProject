using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.IncludeAll().AsEnumerable();
        }
        public T Get(int id)
        {
            return entities.IncludeAll().SingleOrDefault(p => p.Id == id);
        }

        public void Create(T item)
        {
            entities.Add(item);
        }

        public void Update(T item)
        {
            entities.Update(item);
        }

        public void Delete(int id)
        {            
            T entity = entities.Find(id);            
            if (entity != null)
            {
                entities.Remove(entity);
            }            
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public static class NevigationPropertiesLoader
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual)
                {
                    queryable = queryable.Include(property.Name);
                }
            }
            return queryable;
        }
    }

}
