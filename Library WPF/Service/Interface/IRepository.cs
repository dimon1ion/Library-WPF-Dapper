using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library_WPF.Service.Interface
{
    public interface IRepository
    {
        Task<int> InsertUpdateDelete(string query, object param = null);
        IEnumerable<T> Get<T>(string query, object param = null);
        T GetFirst<T>(string query, object param = null);
    }
}
