using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianEscape.Application.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<TResult> Add<TSource, TResult>(TSource source);
        Task Update<TSource>(int id, TSource source);
        Task<TResult> Get<TResult, TSource>(TSource id);
        Task<TResult> Get<TResult>(int id);

        Task<IEnumerable<TResult>> GetAll<TResult>();

        Task<bool> Exists(int id);

        Task Delete(int? id);


    }
}
