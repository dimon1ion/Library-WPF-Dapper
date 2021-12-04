using Library_WPF.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Library_WPF.Service.Interface;
using Dapper;
using System.Windows;
using Library_WPF.Model;

namespace Library_WPF.Service
{
    public class DapperExecutor : IRepository
    {
        public static string ConnectionString { get; set; } = Settings.Default.Constr;

        public DapperExecutor()
        {

        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async void Insert(string query, object param = null)
        {
            using (SqlConnection connection = DapperExecutor.GetConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, param);
                MessageBox.Show("Registered successfully!");
            }
        }

        public void Delete(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get<T>(string query)
        {
            using (SqlConnection connection = DapperExecutor.GetConnection())
            {
                connection.Open();
                return connection.Query<T>(query);
            }
        }
        public T GetFirst<T>(string query, object param = null)
        {
            using (SqlConnection connection = DapperExecutor.GetConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(query, param);
            }
        }
    }
}
