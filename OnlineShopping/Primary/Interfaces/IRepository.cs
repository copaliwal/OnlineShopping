using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Primary.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
