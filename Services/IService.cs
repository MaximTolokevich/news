using System.Collections.Generic;
using System.Linq;

namespace news.Services
{
    public interface IService<T>
    {
        public IEnumerable<T> GetAll();
        public T Get(int Id);
        public bool Create(T item);
        public bool Update(T item);
        public bool Delete(int Id);
    }


}
