using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news.Service
{
    interface IService<T>
    {
        IQueryable<T> GetAll();
        T Get(int Id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(int Id);
    }
    

}
