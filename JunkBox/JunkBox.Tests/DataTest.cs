using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JunkBox.Tests
{
    [TestClass]
    public class AddressTest
    {
        public int AddressID { get; set; }
        public string CustomerUUID { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public int BillingZip { get; set; }
        public string BillingAddress { get; set; }

        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public int ShippingZip { get; set; }
        public string ShippingAddress { get; set; }

        [TestMethod]
        public void TestInsertAddress()
        {
            CustomerUUID = "251dbc0b-b6ab-11e6-9e73-0050569e2378";
            BillingCity = "Oshkosh";
            BillingState = "WI";
            BillingZip = 54901;
            BillingAddress = "123 4th St";
            ShippingCity = "Oshkosh";
            ShippingState = "WI";
            ShippingZip = 54901;
            ShippingAddress = "123 4th St";

            String queryInsert = "INSERT INTO Address(CustomerUUID, BillingCity, BillingState, Billingzip, BillingAddress, " +
                                 "ShippingCity, ShippingState, ShippingZip, ShippingAddress) VALUES ('251dbc0b-b6ab-11e6-9e73-0050569e2378', " +
                                 "'Oshkosh', 'WI', '54901', '123 4th St', 'Oshkosh', 'WI', '54901', '123 4th St')";
            String insert = "INSERT INTO Address(CustomerUUID, BillingCity, BillingState, Billingzip, BillingAddress, " +
                                 "ShippingCity, ShippingState, ShippingZip, ShippingAddress) VALUES ('" + CustomerUUID + "', " +
                                 "'" + BillingCity + "', '" + BillingState + "', '" + BillingZip + "', '" + BillingAddress + "', '" + ShippingCity + "', '" + ShippingState + "', " +
                                 "'" + ShippingZip + "', '" + ShippingAddress + "')";

            Assert.AreEqual(queryInsert, insert);
        }

        [TestMethod]
        public void TestUpdateShippingAddress()
        {
            AddressID = 3;
            ShippingAddress = "456 7th St";
            ShippingCity = "Peoria";
            ShippingState = "IL";
            ShippingZip = 60012;

            String queryUpdate = "UPDATE Address SET ShippingAddress = '456 7th St', ShippingCity = 'Peoria', " +
                                 "ShippingState = 'IL', ShippingZip = '60012' WHERE AddressID = '3'";

            String update = "UPDATE Address SET ShippingAddress = '" + ShippingAddress + "', ShippingCity = '" + ShippingCity + "', " +
                                 "ShippingState = '" + ShippingState + "', ShippingZip = '" + ShippingZip + "' WHERE AddressID = '" + AddressID + "'";

            Assert.AreEqual(queryUpdate, update);
        }

        [TestMethod]
        public void TestUpdateBillingAddress()
        {
            AddressID = 5;
            BillingAddress = "789 10Th St";
            BillingCity = "Kenosha";
            BillingState = "WI";
            BillingZip = 54974;
            String queryUpdate = "UPDATE Address SET BillingAddress = '789 10Th St', BillingCity = 'Kenosha', " +
                                 "BillingState = 'WI', BillingZip = '54974' WHERE AddressID = '5'";

            String update = "UPDATE Address SET BillingAddress = '" + BillingAddress + "', BillingCity = '" + BillingCity + "', " +
                                 "BillingState = '" + BillingState + "', BillingZip = '" + BillingZip + "' WHERE AddressID = '" + AddressID + "'";

            Assert.AreEqual(queryUpdate, update);
        }

        [TestMethod]
        public void TestDeleteFromAddress()
        {
            AddressID = 7;

            String queryDelete = "DELETE FROM Address WHERE AddressID = '7'";

            String delete = "DELETE FROM Address WHERE AddressID = '" + AddressID + "'";

            Assert.AreEqual(queryDelete, delete);
        }


    }

    [TestClass]
    public class CustomerOrderTest
    {
        public int OrderID { get; set; }
        public string CustomerUUID { get; set; }
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public string PurchasePrice { get; set; }
        public string Title { get; set; }
        public string CheckoutSessionID { get; set; }
        public string ExpirationDate { get; set; }
        public string ImageURL { get; set; }

        [TestMethod]
        public void TestInsertCustomerOrder()
        {
            CustomerUUID = "ff269741-b6aa-11e6-9e73-0050569e2378";
            CustomerID = 3;
            AddressID = 5;
            PurchasePrice = "3.45";
            Title = "";
            CheckoutSessionID = "v1|5000467243|133300069";
            ExpirationDate = "2036-11-28T02:19:40.202Z";
            ImageURL = "http://thumbs.ebaystatic.com/images/i/110186218369-0-1/s-l225.jpg";

            String queryCustomerOrder = "INSERT INTO CustomerOrder (CustomerUUID, CustomerID, AddressID, PurchasePrice, Title, CheckoutSessionID, " +
                                        "ExpirationDate, ImageURL) VALUES ('ff269741-b6aa-11e6-9e73-0050569e2378', '3', '5', '3.45', '', " +
                                        "'v1|5000467243|133300069', '2036-11-28T02:19:40.202Z', 'http://thumbs.ebaystatic.com/images/i/110186218369-0-1/s-l225.jpg')";

            String query = "INSERT INTO CustomerOrder (CustomerUUID, CustomerID, AddressID, PurchasePrice, Title, CheckoutSessionID, " +
                                        "ExpirationDate, ImageURL) VALUES ('" + CustomerUUID + "', '" + CustomerID + "', '" + AddressID + "', '" + PurchasePrice + "', '', " +
                                        "'" + CheckoutSessionID + "', '" + ExpirationDate + "', '" + ImageURL + "')";

            Assert.AreEqual(query, queryCustomerOrder);
        }
        [TestMethod]
        public void updatePurchasePriceCustomerOrder()
        {
            OrderID = 1;
            PurchasePrice = "2.34";

            String queryUpdateCustomerOrder = "UPDATE CustomerOrder SET PurchasePrice = '2.34' WHERE OrderID = '1'";

            String update = "UPDATE CustomerOrder SET PurchasePrice = '" + PurchasePrice + "' WHERE OrderID = '" + OrderID + "'";

            Assert.AreEqual(queryUpdateCustomerOrder, update);
        }

        [TestMethod]
        public void updatePurchasePriceAndImageCustomerOrder()
        {
            OrderID = 3;
            PurchasePrice = "4.56";
            ImageURL = "http://thumbs.ebaystatic.com/images/i/110186166894-0-1/s-l225.jpg";

            String queryUpdateCustomerOrder = "UPDATE CustomerOrder SET PurchasePrice = '4.56', " +
                                              " ImageURL = 'http://thumbs.ebaystatic.com/images/i/110186166894-0-1/s-l225.jpg' WHERE OrderID = '3'";

            String update = "UPDATE CustomerOrder SET PurchasePrice = '" + PurchasePrice + "', " +
                                              " ImageURL = '" + ImageURL + "' WHERE OrderID = '" + OrderID + "'";

            Assert.AreEqual(queryUpdateCustomerOrder, update);
        }

        [TestMethod]
        public void selectPurchasePriceCustomerOrder()
        {
            OrderID = 8;
            String column = "PurchasePrice";

            String querySelect = "SELECT PurchasePrice FROM CustomerOrder WHERE OrderID = '8'";

            String select = "SELECT " + column + " FROM CustomerOrder WHERE OrderID = '" + OrderID + "'";

            Assert.AreEqual(querySelect, select);
        }

        [TestMethod]
        public void deleteFromCustomerOrder()
        {
            OrderID = 19;

            String deleteQuery = "DELETE FROM CustomerOrder WHERE OrderID = '19'";

            String delete = "DELETE FROM CustomerOrder WHERE OrderID = '" + OrderID + "'";

            Assert.AreEqual(deleteQuery, delete);
        }


    }

    [TestClass]
    public class CustomerTest
    {
        public int CustomerID { get; set; }
        public string CustomerUUID { get; set; }
        public int QueryID { get; set; }
        public int AddressID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }

        [TestMethod]
        public void InsertCustomer()
        {
            CustomerUUID = "251dbc0b-b6ab-11e6-9e73-0050569e2378";
            QueryID = 4;
            AddressID = 3;
            FirstName = "Frank";
            LastName = "JimBob";
            Phone = "6082435761";
            Hash = "3k92tAVFyCidUmyLE0AAWBP4PeU88j7i7DhVUsTY1QnQbh4y8jYXJEvJa6zZbymB9aLRh0ryDHuMctI3LI5MZUh4PXs0";
            Salt = "SHg9ezQ=";
            Email = "test@test.com";
            String insertCustomerQuery = "INSERT INTO Customer (CustomerUUID, QueryID,AddressID, FirstName,LastName, Phone,Hash,Salt,Email) VALUES " +
                                         "('251dbc0b-b6ab-11e6-9e73-0050569e2378','4','3','Frank','JimBob','6082435761', " +
                                         "'3k92tAVFyCidUmyLE0AAWBP4PeU88j7i7DhVUsTY1QnQbh4y8jYXJEvJa6zZbymB9aLRh0ryDHuMctI3LI5MZUh4PXs0', " +
                                         "'SHg9ezQ=', 'test@test.com')";

            String insert = "INSERT INTO Customer (CustomerUUID, QueryID,AddressID, FirstName,LastName, Phone,Hash,Salt,Email) VALUES " +
                                         "('" + CustomerUUID + "','" + QueryID + "','" + AddressID + "','" + FirstName + "','" + LastName + "','" + Phone + "', " +
                                         "'" + Hash + "', '" + Salt + "', '" + Email + "')";

            Assert.AreEqual(insertCustomerQuery, insert);
        }

        [TestMethod]
        public void updateCustomerName()
        {
            CustomerID = 4;
            FirstName = "Jenny";
            LastName = "MacEvoy";
            String OldLastName = "Henry";

            String updateCustomerQuery = "Update Customer Set LastName = 'MacEvoy' WHERE FirstName = 'Jenny' AND LastName = 'Henry' AND CustomerID = '4'";

            String update = "Update Customer Set LastName = '" + LastName + "' WHERE FirstName = '" + FirstName + "' AND LastName = '" + OldLastName + "' AND CustomerID = '" + CustomerID + "'";

            Assert.AreEqual(updateCustomerQuery, update);
        }

        [TestMethod]
        public void updatePhoneNumberCustomer()
        {
            CustomerID = 5;
            FirstName = "Jerry";
            LastName = "Seinfeld";
            Phone = "4442132314";

            String updateCustomerQuery = "UPDATE Customer SET PHONE = '4442132314' WHERE FirstName = 'Jerry' AND LastName = 'Seinfeld' AND CustomerId = '5'";

            String update = "UPDATE Customer SET PHONE = '" + Phone + "' WHERE FirstName = '" + FirstName + "' AND LastName = '" + LastName + "' AND CustomerId = '" + CustomerID + "'";

            Assert.AreEqual(updateCustomerQuery, update);

        }

        [TestMethod]
        public void DeleteFromCustomer()
        {
            CustomerID = 5;
            FirstName = "Katie";
            LastName = "Francis";

            String deleteCustomerQuery = "DELETE FROM Customer WHERE FirstName = 'Katie' AND LastName = 'Francis' AND CustomerID = '5'";

            String delete = "DELETE FROM Customer WHERE FirstName = '" + FirstName + "' AND LastName = '" + LastName + "' AND CustomerID = '" + CustomerID + "'";

            Assert.AreEqual(deleteCustomerQuery, delete);
        }

    }

    [TestClass]
    public class QueryTest
    {
        public int QueryID { get; set; }
        public string CustomerUUID { get; set; }
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }

        [TestMethod]
        public void InsertQuery()
        {
            CustomerUUID = "ff269741-b6aa-11e6-9e73-0050569e2378";
            Frequency = "DAILY";
            PriceLimit = "5";
            Category = "Consumer Electronics";
            CategoryID = "293";

            String queryInsert = "INSERT INTO Query (CustomerUUID, Frequency, PriceLimit, Category, CategoryID) VALUES " +
                                 "('ff269741-b6aa-11e6-9e73-0050569e2378','DAILY','5','Consumer Electronics','293')";

            String queryQuery = "INSERT INTO Query (CustomerUUID, Frequency, PriceLimit, Category, CategoryID) VALUES " +
                                 "('" + CustomerUUID + "','" + Frequency + "','" + PriceLimit + "','" + Category + "','" + CategoryID + "')";

            Assert.AreEqual(queryInsert, queryQuery);
        }

        [TestMethod]
        public void TestUpdateQueryPriceLimit()
        {
            QueryID = 4;
            PriceLimit = "5";

            String queryUpdate = "UPDATE Query SET PriceLimit = '5' WHERE QueryId = '4'";

            String query = "UPDATE Query SET PriceLimit = '" + PriceLimit + "' WHERE QueryId = '" + QueryID + "'";

            Assert.AreEqual(queryUpdate, query);
        }

        [TestMethod]
        public void TestUpdateQueryFrequency()
        {
            QueryID = 6;
            Frequency = "WEEKLY";

            String queryUpdate = "UPDATE Query SET Frequency = 'WEEKLY' WHERE QueryId = '6'";

            String query = "UPDATE Query SET Frequency = '" + Frequency + "' WHERE QueryId = '" + QueryID + "'";

            Assert.AreEqual(queryUpdate, query);
        }

        [TestMethod]
        public void TestDeleteQuery()
        {
            QueryID = 45;

            String queryDelete = "DELETE FROM Query WHERE QueryID = '45'";

            String delete = "DELETE FROM Query WHERE QueryID = '" + QueryID + "'";

            Assert.AreEqual(queryDelete, delete);
        }

    }
}
