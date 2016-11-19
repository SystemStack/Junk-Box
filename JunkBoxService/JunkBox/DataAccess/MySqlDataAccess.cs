using System;
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
        private static string remoteConnectionString = ConfigurationManager.ConnectionStrings["RemoteConnection"].ConnectionString;

        private static MySqlDataAccess instance = null;

        private MySqlConnection connection = null;

        private int updateID = -1;

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
                //System.Windows.Forms.MessageBox.Show(defaultConnectionString);
                connection = new MySqlConnection(remoteConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                try
                {
                    //System.Windows.Forms.MessageBox.Show(remoteConnectionString);

                    connection = new MySqlConnection(defaultConnectionString);
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
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

        public DbDataReader findCustomer(String userName)
        {
            String command = "SELECT FirstName, LastName FROM `Customer` WHERE UserName = '" + userName + "'";

            return executeCommand(command);
        }

        public DbDataReader findUserPurchasePrice(String userName)
        {
            String command = "SELECT * FROM Orders JOIN Customer ON Orders.CustomerId = Customer.Id WHERE Customer.UserName = '" + userName + "'";

            return executeCommand(command);

        }

        public void updateCustomerName(String firstName, String lastName, String userName)
        {
            String command = "UPDATE Customer SET `FirstName= '" + firstName + "',LastName = '" + lastName + "' WHERE Email = '" + userName + "'";

            execute(command);

        }

        public void updateCustomerEmail(String firstName, String lastName, String oldEmail, String newEmail)
        {

            String command = "UPDATE Customer SET Email = '" + newEmail + "' WHERE FirstName = '" + firstName +
                             "' AND LastName = '" + lastName + "' WHERE Email = '" + oldEmail + "'";

            execute(command);
        }

        public void updateCustomerPhoneNumber(String firstName, String lastName, String email, String newPhone)
        {
            String command = "UPDATE Customer SET Phone= '" + newPhone + "' WHERE FirstName = '" + firstName +
                             "' AND LastName = '" + lastName + "' AND Email = '" + email + "'";

            execute(command);
        }

        private void executeID(String command)
        {
            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            updateID = (int)cmd.LastInsertedId;
            CloseConnection();

        }
        private void execute(String command)
        {
            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            CloseConnection();
        }
        private DbDataReader executeCommand(String command)
        {
            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(command, connection);

            MySqlDataReader reader = cmd.ExecuteReader();

            CloseConnection();

            return reader;
        }


    }
}