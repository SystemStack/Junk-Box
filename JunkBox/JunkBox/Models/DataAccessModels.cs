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

    public class NonQueryResultModel : IDataResultModel
    {
        public bool Success { get; set; }
    }

    public abstract class CustomerModel
    {
        public int CustomerID { get; set; }
        public string CustomerUUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }

    public class CustomerResultModel : CustomerModel, IDataResultModel
    {
        private new int CustomerID { get; set; }
    }

    public class SelectCustomerModel : CustomerModel, IDataParameterModel
    {
        private new int CustomerID { get; set; }
        private new string FirstName { get; set; }
        private new string LastName { get; set; }
        private new string Phone { get; set; }
        private new string Hash { get; set; }
        private new string Salt { get; set; }
    }

    public class InsertCustomerModel : CustomerModel, IDataParameterModel
    {
        private new int CustomerID { get; set; }
    }

    public class UpdateCustomerModel : CustomerModel, IDataParameterModel
    {
        private new int CustomerID { get; set; }
    }

    public class DeleteCustomerModel : CustomerModel, IDataParameterModel
    {
        private new int CustomerID { get; set; }
    }

    public abstract class QueryModel
    {
        public int QueryID { get; set; }
        public string CustomerUUID { get; set; }
        public string Frequency { get; set; }
        public string PriceLimit { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
    }

    public class QueryResultModel : QueryModel, IDataResultModel
    {
    }

    public class SelectQueryModel : QueryModel, IDataParameterModel
    {
        private new int QueryID { get; set; }
        private new string Frequency { get; set; }
        private new string PriceLimit { get; set; }
        private new string Category { get; set; }
        private new string CategoryID { get; set; }
    }

    public class InsertQueryModel : QueryModel, IDataParameterModel
    {
        private new int QueryID { get; set; }
    }

    public class UpdateQueryModel : QueryModel, IDataParameterModel
    {
        private new int QueryID { get; set; }
    }

    public class DeleteQueryModel : QueryModel, IDataParameterModel
    {
        private new int QueryID { get; set; }
    }

    public abstract class CustomerOrderModel
    {
        public int OrderID { get; set; }
        public string CustomerUUID { get; set; }
        public string PurchasePrice { get; set; }
        public string Title { get; set; }
        public string CheckoutSessionID { get; set; }
        public string ExpirationDate { get; set; }
        public string ImageURL { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class CustomerOrderResultModel : CustomerOrderModel, IDataResultModel
    {
    }

    public class SelectCustomerOrderModel : CustomerOrderModel, IDataParameterModel
    {
        private new int OrderID { get; set; }
        private new string PurchasePrice { get; set; }
        private new string CheckoutSessionID { get; set; }
        private new string ExpirationDate { get; set; }
        private new string ImageURL { get; set; }
        private new DateTime TimeStamp { get; set; }
    }

    public class InsertCustomerOrderModel : CustomerOrderModel, IDataParameterModel
    {
        private new int OrderID { get; set; }
    }

    public class UpdateCustomerOrderModel : CustomerOrderModel, IDataParameterModel
    {
        private new int OrderID { get; set; }
    }

    public class DeleteCustomerOrderModel : CustomerOrderModel, IDataParameterModel
    {
        private new int OrderID { get; set; }
    }

    public abstract class AddressModel
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

        //Optional value
        private string _billingAddress2;
        public string BillingAddress2
        {
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

    public class AddressResultModel : AddressModel, IDataResultModel
    {
        private new int AddressID { get; set; }
    }

    public class SelectAddressModel : AddressModel, IDataParameterModel
    {
        private new int AddressID { get; set; }
        private new string BillingCity { get; set; }
        private new string BillingState { get; set; }
        private new int BillingZip { get; set; }
        private new string BillingAddress { get; set; }

        private new string ShippingCity { get; set; }
        private new string ShippingState { get; set; }
        private new int ShippingZip { get; set; }
        private new string ShippingAddress { get; set; }

        private new string BillingAddress2 { get; set; }
        private new string ShippingAddress2 { get; set; }
    }

    public class InsertAddressModel : AddressModel, IDataParameterModel
    {
        private new int AddressID { get; set; }
    }

    public class UpdateAddressModel : AddressModel, IDataParameterModel
    {
        private new int AddressID { get; set; }
    }

    public class DeleteAddressModel : AddressModel, IDataParameterModel
    {
        private new int AddressID { get; set; }
    }

    public abstract class AccessTokenModel
    {
        public string UseType { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class AccessTokenResultModel : AccessTokenModel, IDataResultModel
    {
    }

    public class SelectAccessTokenModel : AccessTokenModel, IDataParameterModel
    {
    }

    public class InsertAccessTokenModel : AccessTokenModel, IDataParameterModel
    {
    }

    public class UpdateAccessTokenModel : AccessTokenModel, IDataParameterModel
    {
    }

    public class DeleteAccessTokenModel : AccessTokenModel, IDataParameterModel
    {
    }
}