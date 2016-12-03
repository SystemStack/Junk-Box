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

        [DataMember]
        public string checkoutSessionId { get; set; }

        [DataMember]
        public string expirationDate { get; set; }

        [DataMember]
        public LineItems[] lineItems { get; set; }

        [DataMember]
        public PricingSummary[] pricingSummary { get; set; }

        [DataMember]
        public ProvidedPaymentInstrument providedPaymentInstrument { get; set; }

        [DataMember]
        public ShippingAddress shippingAddress { get; set; }

        [DataMember]
        public Warnings[] warning { get; set; }
    }

    [DataContract]
    public class AcceptedPaymentMethods
    {
        [DataMember]
        public string label { get; set; }

        [DataMember]
        public LogoImage logoImage { get; set; }

        [DataMember]
        public PaymentMethodBrands[] paymentMethodBrands { get; set; }

        [DataMember]
        public PaymentMethodMessages[] paymentMethodMessages { get; set; }

        [DataMember]
        public string[] paymentMethodType { get; set; }
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
        public string paymentMethodBrandType { get; set; }
    }

    [DataContract]
    public class PaymentMethodMessages
    {
        [DataMember]
        public string legalMessage { get; set; }

        [DataMember]
        public bool requiredForUserConfirmation { get; set; }
    }

    [DataContract]
    public class LineItems
    {
        [DataMember]
        public BaseUnitPrice baseUnitPrice { get; set; }
       
        [DataMember]
        public Image image { get; set; }

        [DataMember]
        public string itemId { get; set; }

        [DataMember]
        public NetPrice netPrice { get; set; }

        [DataMember]
        public Promotions[] promotions { get; set; }

        [DataMember]
        public int quantity { get; set; }

        [DataMember]
        public Seller seller { get; set; }

        [DataMember]
        public ShippingOptions[] shippingOptions { get; set; }

        [DataMember]
        public string shortDescription { get; set; }

        [DataMember]
        public string title { get; set; }
    }

    [DataContract]
    public class BaseUnitPrice
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Image
    {
        [DataMember]
        public int height { get; set; }

        [DataMember]
        public string imageUrl { get; set; }

        [DataMember]
        public int width { get; set; }
    }

    [DataContract]
    public class NetPrice
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Promotions
    {
        [DataMember]
        public Discount discount { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string promotionCode { get; set; }

        [DataMember]
        public string promotionType { get; set; }
    }

    [DataContract]
    public class Discount
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Seller
    {
        [DataMember]
        public string feedbackPercentage { get; set; }

        [DataMember]
        public int feedbackScore { get; set; }

        [DataMember]
        public string sellerAccountType { get; set; }

        [DataMember]
        public string username { get; set; }
    }

    [DataContract]
    public class ShippingOptions
    {
        [DataMember]
        public BaseDeliveryCost baseDeliveryCost { get; set; }

        [DataMember]
        public DeliveryDiscount deliveryDiscount { get; set; }

        [DataMember]
        public string maxEstimatedDeliveryDate { get; set; }

        [DataMember]
        public string minEstimatedDeliveryDate { get; set; }

        [DataMember]
        public bool selected { get; set; }

        [DataMember]
        public string shippingCarrierName { get; set; }

        [DataMember]
        public string shippingOptionId { get; set; }

        public string shippingServiceName { get; set; }
    }

    [DataContract]
    public class BaseDeliveryCost
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class DeliveryDiscount
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class PricingSummary
    {
        [DataMember]
        public Adjustment adjustment { get; set; }

        [DataMember]
        public DeliveryCost deliveryCost { get; set; }

        [DataMember]
        public DeliveryDiscount deliveryDiscount { get; set; }

        [DataMember]
        public Fee fee { get; set; }

        [DataMember]
        public PriceDiscount priceDiscount { get; set; }

        [DataMember]
        public PriceSubtotal priceSubtotal { get; set; }

        [DataMember]
        public Tax tax { get; set; }

        [DataMember]
        public Total total { get; set; }
    }

    [DataContract]
    public class Adjustment
    {
        [DataMember]
        public Amount amount { get; set; }

        [DataMember]
        public string label { get; set; }
    }

    [DataContract]
    public class Amount
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class DeliveryCost
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Fee
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class PriceDiscount
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class PriceSubtotal
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Tax
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class Total
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class ProvidedPaymentInstrument
    {
        [DataMember]
        public PaymentInstrumentReference paymentInstrumentReference { get; set; }

        [DataMember]
        public PaymentMethodBrand paymentMethodBrand { get; set; }

        [DataMember]
        public string paymentMethodType { get; set; }
    }

    [DataContract]
    public class PaymentInstrumentReference
    {
        [DataMember]
        public string lastFourDigitForCreditCard { get; set; }
    }

    [DataContract]
    public class PaymentMethodBrand
    {
        [DataMember]
        public LogoImage logoImage { get; set; }

        [DataMember]
        public string paymentMethodBrandType { get; set; }
    }

    [DataContract]
    public class ShippingAddress
    {
        [DataMember]
        public string addressLine1 { get; set; }

        [DataMember]
        public string addressLine2 { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string county { get; set; }

        [DataMember]
        public string phoneNumber { get; set; }

        [DataMember]
        public string postalCode { get; set; }

        [DataMember]
        public string recipient { get; set; }

        [DataMember]
        public string stateOrProvince { get; set; }
    }

    [DataContract]
    public class Warnings
    {
        [DataMember]
        public string category { get; set; }

        [DataMember]
        public string domain { get; set; }

        [DataMember]
        public int errorId { get; set; }

        [DataMember]
        public string[] inputRefIds { get; set; }

        [DataMember]
        public string longMessage { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string[] outputRefIds { get; set; }

        [DataMember]
        public Parameters[] parameters { get; set; }
    }

    [DataContract]
    public class Parameters
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
 