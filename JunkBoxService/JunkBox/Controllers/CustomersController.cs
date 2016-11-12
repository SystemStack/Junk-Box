using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JunkBox.Models;
using JunkBox.DataAccess;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Collections;

namespace JunkBox.Controllers
{
    public class CustomersController : ApiController
    {
        //Every controller that needs to use the database will have to have a dataAccess object.
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        Address mockAddress = new Address { };

        //Mock Database
        Customer[] customers = new Customer[]
        {
            new Customer { Id = 1, FirstName = "Gale", LastName = "Smith", Email = "gale@mail.com", BillingAddress =  new Address() {Street = "123 4th St", City = "NFDL", State = "WI", ZipCode = "54937" }, ShippingAddress = new Address(), Order = new Orders() {OrderID = 1, PurchasePrice = 4.44, Month = 11, Day = 4, Year = 2016  } },
            new Customer { Id = 2, FirstName = "Walter", LastName = "Cronkite", Email = "news@google.com", BillingAddress = new Address() {Street = "456 7th St", City = "Oshkosh", State = "WI", ZipCode = "54901"}, ShippingAddress = new Address()},
            new Customer { Id = 3, FirstName = "SuperDude", LastName = "CodeWizard", Email= "it@codewriter.org", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 4, FirstName = "Kevin", LastName = "Smith", Email= "", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 5, FirstName = "John", LastName = "Cena", Email= "guy@fighting.kill", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 6, FirstName = "Gale", LastName = "SecondName", Email= "123@456.789", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 7, FirstName = "Tina", LastName = "Smith", Email= "turner@pop.com", BillingAddress = new Address(), ShippingAddress = new Address()}
        };

        
        public IEnumerable<Customer> GetAllCustomers()
        {
            //Will progress on this further, later.
            dataAccess.OpenConnection();
            DbDataReader result = dataAccess.query("SELECT * FROM Customer");

            Queue<Customer> customer = new Queue<Customer>();
            //System.Collections.ArrayList<> cust = new System.Collections.ArrayList<Customer>();
            //System.Windows.Forms.MessageBox.Show("LOOPING THROUGH RESULTS! " + result.IsClosed);

            while(result.Read() != false)
            {
                //System.Windows.Forms.MessageBox.Show("FOUND A ROW!");
                
                Customer c = new Customer();
                c.Id = (int)result.GetValue(0);
                c.FirstName = (string)result.GetValue(1);
                c.LastName = (string)result.GetValue(2);
                c.Email = (string)result.GetValue(3);
                //c.Phone =

                customer.Enqueue(c);
                //System.Windows.Forms.MessageBox.Show("ADDED: " + customer.Count);
            }

            Customer[] cust = new Customer[customer.Count];
            for(int x = 0; x < customer.Count; x++)
            {
                cust[x] = customer.Dequeue();
            }

            //System.Windows.Forms.MessageBox.Show("COUNT! " + customer.Count);
            //return result;
            //return customers;
            dataAccess.CloseConnection();
            return cust;
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = customers.FirstOrDefault((p) => p.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //Works!! Had to change the API call from /api/customers/Gale to /api/customers/?firstName=Gale
        //Currently is case sensitive for searches. (Gale returns a result where gale does not)
        public IHttpActionResult GetCustomerByFirstName(string firstName)
        {
            var customer = customers.FirstOrDefault((p) => p.FirstName == firstName);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        public IHttpActionResult GetCustomerByFirstAndLastName(string firstName, string lastName)
        {
            var customer = customers.FirstOrDefault((p) => p.FirstName == firstName && p.LastName == lastName );
            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
