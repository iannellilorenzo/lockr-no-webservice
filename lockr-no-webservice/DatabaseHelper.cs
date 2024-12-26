using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lockr_no_webservice
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=account_manager;User=root;Password=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public MySqlDataReader ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            var connection = GetConnection();
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
    }
}
