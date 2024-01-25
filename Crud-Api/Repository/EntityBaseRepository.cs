using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        protected readonly EntityContext Db;
        protected readonly DbSet<T> DbSet;
        public EntityBaseRepository(EntityContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }
        public virtual T Add(T obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
            return obj;
        }
        public virtual void Remove(T obj)
        {
            DbSet.Remove(obj);
            Db.SaveChanges();

        }
        public virtual async Task<T> Update(T obj)
        {
            DbSet.Update(obj);
            await Db.SaveChangesAsync();
            return obj;
        }
        public void Dispose()
        {
            Db.Dispose();
        }

        public List<T> FindAll()
        {
            return DbSet.ToList();
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return DbSet.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = Db.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}