using System;
using System.Collections.Generic;
using System.Text;

namespace Library_WPF.Service.Interface
{
    public interface IRepository
    {
        void InsertUpdate(string query, object param);
        void Delete(string query);
        IEnumerable<T> Get<T>(string query, object param);
        T GetFirst<T>(string query, object param);
    }
}
