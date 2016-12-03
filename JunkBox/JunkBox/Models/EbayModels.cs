using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace JunkBox.Models
{
    public class EbayGetViablePurchasesModel
    {
        public string email { get; set; }
    }

    public class EbayBrowseAPIModel
    {
        public string email { get; set; }
    }

    public class EbayOrderApiInitiateGuestCheckoutSessionModel
    {
        public string email { get; set; }
        public string orderId { get; set; }
        public string imageUrl { get; set; }
    }

    public class EbayOrderApiPlaceGuestOrderModel
    {
        public string email { get; set; }
        public string orderId { get; set; }
        public string imageUrl { get; set; }
    }

    public class EbayBillingAddressModel
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string postalCode { get; set; }
        public string stateOrProvince { get; set; }
    }

    public class EbayCreditCardModel
    {
        public string accountHolderName { get; set; }
        public EbayBillingAddressModel billingAddress { get; set; }
        public string brand { get; set; }
        public string cardNumber { get; set; }
        public string cvvNumber { get; set; }
        public int expireMonth { get; set; }
        public int expireYear { get; set; }
    }

    public class EbayLineItemModel
    {
        public string itemId { get; set; }
        public int quantity { get; set; }
    }

    public class EbayShippingAddressModel
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string postalCode { get; set; }
        public string recipient { get; set; }
        public string stateOrProvince { get; set; }
    }

    public class EbayGuestCheckoutSessionRequestModel
    {
        public string contactEmail { get; set; }
        public string contactFirstName { get; set; }
        public string contactLastName { get; set; }
        public EbayCreditCardModel creditCard { get; set; }
        public EbayLineItemModel[] lineItemInputs { get; set; }
        public EbayShippingAddressModel shippingAddress { get; set; }
    }

    public class EbayUpdateGuestSessionPaymentInfoModel
    {
        public EbayCreditCardModel creditCard { get; set; }
    }

    [DataContract]
    public class CheckoutSessionResponseModel
    {
        [DataMember]
        public AcceptedPaymentMethods acceptedPaymentMethods { get; set; }
    }

    [DataContract]
    public class AcceptedPaymentMethods
    {
        [DataMember]
        public string label { get; set; }

        [DataMember]
        public LogoImage logoImage { get; set; }

        [DataMember]
        public PaymentMethodBrands paymentMethodBrands { get; set; }
    }

    [DataContract]
    public class LogoImage
    {
        [DataMember]
        public int height { get; set; }

        [DataMember]
        public string imageUrl { get; set; }

        [DataMember]
        public int width { get; set; }
    }

    [DataContract]
    public class PaymentMethodBrands
    {
        [DataMember]
        public LogoImage logoImage { get; set; }

        [DataMember]
        public string payment { get; set; }
    }
}
 