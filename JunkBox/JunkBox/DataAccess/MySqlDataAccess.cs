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

        public void OpenConnection()
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

        public void CloseConnection()
        {
            if (connection != null) {

                connection.Close();
        }

        public DbDataReader Query(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public List<Dictionary<string, string>> Select(string query)
        {
            List<Dictionary<string, string>> rows = null;

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                rows = new List<Dictionary<string, string>>();
                while (reader.Read())
                {
                    var row = new Dictionary<string, string>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var columnValue = reader.IsDBNull(i) ? null : reader.GetString(i);
                        row.Add(columnName, columnValue);
                    }
                    rows.Add(row);
                }
            }


            CloseConnection();

            return rows;
        }
        
        public int Insert(string table, Dictionary<string, string> parameters)
        {
            OpenConnection();
            //"INSERT INTO `cs341_t5`.`Customer` (`CustomerID`, `QueryID`, `AddressID`, `FirstName`, `LastName`, `Phone`, `Hash`, `Salt`, `Email`) 
            //VALUES (NULL, '3', '3', 'REGISTER_TEST', 'Test', '1112223333', '4', 'r', 'walter@test.com');
            

            string[] keys = parameters.Keys.ToArray();
            string columns = String.Join(", ", keys);

            string[] vals = parameters.Values.ToArray();
            string values = "'" + String.Join("', '", vals) + "'";

            string query = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ");";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
        }

        public int Delete(string table, string key, string value)
        {
            //"DELETE FROM `cs341_t5`.`Customer` WHERE `Customer`.`CustomerID` = 9"

            OpenConnection();
            string query = "DELETE FROM " + table + " WHERE " + key + "=@value";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.Add(new MySqlParameter("value", value));


            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
        }

        public int Update(string table, Dictionary<string, string> items, string key, string value)
        {
            //UPDATE `cs341_t5`.`Customer` 
            //SET `QueryID` = '2', `AddressID` = '9', `FirstName` = 'UpdateTest', `Phone` = '111222444', `Hash` = 't', `Salt` = '3', `Email` = 'test2@guy.com' 
            //WHERE `Customer`.`CustomerID` = 27;
            OpenConnection();


            string[] i = new string[items.Count];
            int iCount = 0;
            foreach (KeyValuePair<string, string> set in items)
            {
                i[iCount++] = set.Key + "='" + set.Value + "'";
            }

            string itemList = String.Join(", ", i);


            string query = "UPDATE " + table + " SET " + itemList + " WHERE " + key + "=@value;";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.Add(new MySqlParameter("value", value));


            int result = cmd.ExecuteNonQuery();

            CloseConnection();

            return result;
        }

    }
}