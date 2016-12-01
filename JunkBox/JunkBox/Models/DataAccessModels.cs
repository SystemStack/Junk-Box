﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class AddressModel
    {
        public string CustomerUUID { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public int BillingZip { get; set; }
        public string BillingAddress { get; set; }
        
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public int ShippingZip { get; set; }
        public string ShippingAddress { get; set; }
        
        //Optional value
        private string _billingAddress2;
        public string BillingAddress2 {
            get { if (_billingAddress2 == null) { return string.Empty; } return _billingAddress2; }
            set { _billingAddress2 = value; }
        }

        //Optional value
        private string _shippingAddress2;
        public string ShippingAddress2
        {
            get { if (_shippingAddress2 == null) { return string.Empty; } return _shippingAddress2; }
            set { _shippingAddress2 = value; }
        }
    }

    public class InsertCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }

    public class CustomerHashSaltModel
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    public class CustomerDataModel
    {
        public string CustomerUUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }

    public class CustomerUUIDModel
    {
        public string CustomerUUID { get; set; }
    }

    public class CustomerEmailModel
    {
        public string Email { get; set; }
    }

    public class InsertQueryModel
    {
        public string CustomerUUID { get; set; }
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class NonQueryResultModel
    {
        public bool Success { get; set; }
    }

    public class CustomerOrderDataModel
    {
        public string PurchasePrice { get; set; }
        public string CheckoutSessionID { get; set; }
        public string ExpirationDate { get; set; }
        public string ImageURL { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class QueryDataModel
    {
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class AccessTokenModel
    {
        public string UseType { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public DateTime DateCreated { get; set; }
    }
}