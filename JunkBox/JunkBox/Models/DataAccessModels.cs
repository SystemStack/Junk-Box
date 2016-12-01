using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public interface IDataResultModel
    {

    }

    public interface IDataParameterModel
    {
    }



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
        public string CustomerUUID { get; set; }
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

    public class CustomerUUIDModel : IDataResultModel, IDataParameterModel
    {
        public string CustomerUUID { get; set; }
    }

    public class CustomerEmailModel
    {
        public string Email { get; set; }
    }

    public class InsertQueryModel : IDataParameterModel
    {
        public string CustomerUUID { get; set; }
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class QueryModel : IDataResultModel
    {
        public int QueryID { get; set; }
        public string CustomerUUID { get; set; }
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class QueryDataModel : IDataResultModel
    {
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class NonQueryResultModel : IDataResultModel
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

    public class AccessTokenModel
    {
        public string UseType { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class CustomerOrderModel
    {
        public int OrderID { get; set; }
        public string CustomerUUID { get; set; }
        public string PurchasePrice { get; set; }
        public string CheckoutSessionID { get; set; }
        public string ExpirationDate { get; set; }
        public string ImageURL { get; set; }
        public DateTime TimeStamp { get; set; }
    }

}