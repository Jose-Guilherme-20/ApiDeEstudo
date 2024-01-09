using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEntityBaseRepository<T> : IDisposable where T : class
    {
        T Add(T obj);
        Task<T> Update(T obj);
        void Remove(T obj);
        List<T> FindAll();


    }
}