using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using JunkBox.Models;

namespace JunkBox.Controllers
{
    public class CustomersController : ApiController
    {
        Address mockAddress = new Address { };
        //Mock Database
        Customer[] customers = new Customer[]
        {
            new Customer { Id = 1, FirstName = "Gale", LastName = "Smith", Email = "gale@mail.com", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 2, FirstName = "Walter", LastName = "Cronkite", Email = "news@google.com", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 3, FirstName = "SuperDude", LastName = "CodeWizard", Email= "it@codewriter.org", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 4, FirstName = "Kevin", LastName = "Smith", Email= "", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 5, FirstName = "John", LastName = "Cena", Email= "guy@fighting.kill", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 6, FirstName = "Gale", LastName = "SecondName", Email= "123@456.789", BillingAddress = new Address(), ShippingAddress = new Address()},
            new Customer { Id = 7, FirstName = "Tina", LastName = "Smith", Email= "turner@pop.com", BillingAddress = new Address(), ShippingAddress = new Address()}
        };

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customers;
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
