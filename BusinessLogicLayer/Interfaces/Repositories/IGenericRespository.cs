using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Repositories
{
    public interface IGenericRespository<T> where T : class
    {
        void Update(T Entity);
        Task AddAsync(T Entity);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T Entity);

    }
}
