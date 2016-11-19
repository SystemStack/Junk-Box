﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Configuration;


//My initial solution for how to handle data access. A singleton object.
namespace JunkBox.DataAccess
{
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
            if (connection != null)
                connection.Close();
        }

        public DbDataReader query(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public DbDataReader select(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public int insert(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            return cmd.ExecuteNonQuery();
        }
    }
}