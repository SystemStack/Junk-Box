using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Configuration;


//My initial solution for how to handle data access. A singleton object.
namespace JunkBox.DataAccess {
    public class MySqlDataAccess : IDataAccess
    {
        private static string defaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static MySqlDataAccess instance = null;
        private MySqlConnection connection = null;

        public static MySqlDataAccess GetDataAccess()
        {
            if (instance == null)
                instance = new MySqlDataAccess();

            return instance;
        }

        private MySqlDataAccess()
        {
        }

        //public void OpenConnection()
        private void OpenConnection()
        {
            try
            {
                connection = new MySqlConnection(defaultConnectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //public void CloseConnection()
        private void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        /*
        public DbDataReader Query(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }*/

        public List<IDictionary<string, object>> Select(string query, IDictionary<string, object> whereParameters)
        {
            List<IDictionary<string, object>> rows = null;

            OpenConnection();

            MySqlCommand cmd = ParameterizeCommand(query, whereParameters);
            cmd.Connection = connection;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                rows = new List<IDictionary<string, object>>();
                while (reader.Read())
                {
                    IDictionary<string, object> row = new Dictionary<string, object>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        row.Add(columnName, columnValue);
                    }
                    rows.Add(row);
                }
            }

            CloseConnection();

            return rows;
        }

        public int Insert(string query, IDictionary<string, object> parameters)
        {
            OpenConnection();

            MySqlCommand cmd = ParameterizeCommand(query, parameters);
            cmd.Connection = connection;

            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
        }

        public int Delete(string table, string key, string value)
        {
            /*
            //"DELETE FROM `cs341_t5`.`Customer` WHERE `Customer`.`CustomerID` = 9"

            OpenConnection();
            string query = "DELETE FROM " + table + " WHERE " + key + "=@value";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.Add(new MySqlParameter("value", value));


            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
            */
            return 0;
        }

        //NOTE!!!!! This and Insert are /EXACTLY/ the same. Revise later.
        public int Update(string query, IDictionary<string, object> parameters)
        {
            OpenConnection();

            MySqlCommand cmd = ParameterizeCommand(query, parameters);
            cmd.Connection = connection;

            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
        }

        private MySqlCommand ParameterizeCommand(string query, IDictionary<string, object> parameters)
        {
            MySqlCommand command = new MySqlCommand(query);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> entry in parameters)
                {
                    command.Parameters.Add(new MySqlParameter(entry.Key, entry.Value));
                }
            }

            return command;
        }

    }
}