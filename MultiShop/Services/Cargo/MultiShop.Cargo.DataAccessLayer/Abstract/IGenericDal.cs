using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

    }
}
