using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace JunkBox.Models
{
    #region general purpose EbayModels
    //These models could probably use a rework
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
        public string customerUuid { get; set; }
        public string checkoutSessionId { get; set; }
    }

    public class EbayGetViablePurchasesModel
    {
        public string email { get; set; }
    }
    #endregion

    /// <summary>
    /// Root Model for Order API GET /guest_checkout_session/{checkoutSessionId}
    /// </summary>
    [DataContract]
    public class CheckoutSessionResponse
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
        public PricingSummary pricingSummary { get; set; }

        [DataMember]
        public ProvidedPaymentInstrument providedPaymentInstrument { get; set; }

        [DataMember]
        public ShippingAddress shippingAddress { get; set; }

        [DataMember]
        public Warnings[] warnings { get; set; }

        [DataMember]
        public Errors[] errors { get; set; }
    }

    /// <summary>
    /// Root Model for Browse API GET /item_summary/search
    /// </summary>
    [DataContract]
    public class SearchPagedCollection
    {
        [DataMember]
        public string href { get; set; }

        [DataMember]
        public ItemSummaries[] itemSummaries { get; set; }

        [DataMember]
        public int limit { get; set; }

        [DataMember]
        public string next { get; set; }

        [DataMember]
        public int offset { get; set; }

        [DataMember]
        public string prev { get; set; }

        [DataMember]
        public int total { get; set; }

        [DataMember]
        public Warnings[] warnings { get; set; }

        [DataMember]
        public Errors[] errors { get; set; }
    }

    /// <summary>
    /// Root Model for Order API POST /guest_checkout_session/initiate
    /// </summary>
    [DataContract]
    public class CreateGuestCheckoutSessionRequest
    {
        [DataMember]
        public string contactEmail { get; set; }

        [DataMember]
        public string contactFirstName { get; set; }

        [DataMember]
        public string contactLastName { get; set; }

        [DataMember]
        public CreditCard creditCard { get; set; }

        [DataMember]
        public LineItemInputs[] lineItemInputs { get; set; }

        [DataMember]
        public ShippingAddress shippingAddress { get; set; }
    }

    /// <summary>
    /// Payload Model for Order API POST /guest_checkout_session/{checkoutSessionId}/update_payment_info
    /// </summary>
    public class UpdatePaymentInformation
    {
        public CreditCard creditCard { get; set; }
    }

    [DataContract]
    public class Errors
    {
        [DataMember]
        public int errorId { get; set; }

        [DataMember]
        public string domain { get; set; }

        [DataMember]
        public string category { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public Parameters[] parameters { get; set; }
    }

    [DataContract]
    public class PurchaseOrderSummary
    {
        [DataMember]
        public string purchaseOrderHref { get; set; }

        [DataMember]
        public string purchaseOrderId { get; set; }

        [DataMember]
        public string purchaseOrderPaymentStatus { get; set; }

        [DataMember]
        public Warnings[] warnings { get; set; }

        [DataMember]
        public Errors[] errors { get; set; }
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

    [DataContract]
    public class ItemSummaries
    {
        [DataMember]
        public AdditionalImages[] additionalImages { get; set; }

        [DataMember]
        public int bidCount { get; set; }

        [DataMember]
        public string[] buyingOptions { get; set; }

        [DataMember]
        public Categories[] categories { get; set; }

        [DataMember]
        public string condition { get; set; }

        [DataMember]
        public CurrentBidPrice currentBidPrice { get; set; }

        [DataMember]
        public DistanceFromPickupLocation distanceFromPickupLocation { get; set; }

        [DataMember]
        public string energyEfficiencyClass { get; set; }

        [DataMember]
        public Image image { get; set; }

        [DataMember]
        public string itemAffiliateWebUrl { get; set; }

        [DataMember]
        public string itemGroupHref { get; set; }

        [DataMember]
        public string itemId { get; set; }

        [DataMember]
        public ItemLocation itemLocation { get; set; }

        [DataMember]
        public MarketingPrice marketingPrice { get; set; }

        [DataMember]
        public PickupOptions[] pickupOptions { get; set; }

        [DataMember]
        public Price price { get; set; }

        [DataMember]
        public Seller seller { get; set; }

        [DataMember]
        public ShippingOptions[] shippingOptions { get; set; }

        [DataMember]
        public ThumbnailImages[] thumbnailImages { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public bool topRatedBuyingExperience { get; set; }
    }

    [DataContract]
    public class AdditionalImages
    {
        [DataMember]
        public int height { get; set; }

        [DataMember]
        public string imageUrl { get; set; }

        [DataMember]
        public int width { get; set; }
    }

    [DataContract]
    public class Categories
    {
        [DataMember]
        public string categoryId { get; set; }
    }

    [DataContract]
    public class CurrentBidPrice
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class DistanceFromPickupLocation
    {
        [DataMember]
        public string unitOfMeasure { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class ItemLocation
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
        public string postalCode { get; set; }

        [DataMember]
        public string stateOrProvince { get; set; }
    }

    [DataContract]
    public class MarketingPrice
    {
        [DataMember]
        public DiscountAmount discountAmount { get; set; }

        [DataMember]
        public string discountPercentage { get; set; }

        [DataMember]
        public OriginalPrice originalPrice { get; set; }
    }

    [DataContract]
    public class DiscountAmount
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class OriginalPrice
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class PickupOptions
    {
        [DataMember]
        public string pickupLocationType { get; set; }
    }

    [DataContract]
    public class Price
    {
        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string value { get; set; }
    }

    [DataContract]
    public class ThumbnailImages
    {
        [DataMember]
        public int height { get; set; }

        [DataMember]
        public string imageUrl { get; set; }

        [DataMember]
        public int width { get; set; }
    }

    [DataContract]
    public class CreditCard
    {
        [DataMember]
        public string accountHolderName { get; set; }

        [DataMember]
        public BillingAddress billingAddress { get; set; }

        [DataMember]
        public string brand { get; set; }

        [DataMember]
        public string cardNumber { get; set; }

        [DataMember]
        public string cvvNumber { get; set; }

        [DataMember]
        public int expireMonth { get; set; }

        [DataMember]
        public int expireYear { get; set; }
    }

    [DataContract]
    public class BillingAddress
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
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string postalCode { get; set; }

        [DataMember]
        public string stateOrProvince { get; set; }
    }

    [DataContract]
    public class LineItemInputs
    {
        [DataMember]
        public string itemId { get; set; }

        [DataMember]
        public int quantity { get; set; }
    }
}
 