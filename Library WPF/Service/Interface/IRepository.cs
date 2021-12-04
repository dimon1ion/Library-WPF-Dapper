using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Service.Interface
{
    public interface IRepository
    {
        void Insert(string query, object param);
        void Delete(string query);
        IEnumerable<T> Get<T>(string query);
        T GetFirst<T>(string query, object param = null);
    }
}
