using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Finders
{
    public interface IFinder<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IQueryable<T>> FindAsync(Func<T, Boolean> predicate);
    }
}
