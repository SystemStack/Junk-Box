using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;

using JunkBox.Common;
using JunkBox.Models;

namespace JunkBox.Ebay
{
    public class OrderAPI
    {
        private static string authToken = ConfigurationManager.AppSettings["AuthToken"];
        private static string baseUrl = "https://api.sandbox.ebay.com";

        
        public static PurchaseOrderSummary PlaceGuestOrder(string checkoutSessionId)
        {
            //POST https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/{guest_checkoutsession_id}/place_order

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/place_order";

            return Web.Post<PurchaseOrderSummary> (apiUrl);
        }
        
        
        public static CheckoutSessionResponse UpdateGuestSessionPaymentInfo(string checkoutSessionId, Dictionary<string, string> customerInfo, Dictionary<string, string> addressInfo)
        {

            /*
             * POST: https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/{guest_checkoutsession_id}/update_payment_info
             * 
             *{ /* UpdatePaymentInformation 
                "creditCard":
                { /* CreditCard 
                    "accountHolderName": string,
                    "billingAddress":
                    { /* BillingAddress 
                        "addressLine1": string,
                        "addressLine2": string,
                        "city": string,
                        "country": string,
                        "county": string,
                        "firstName": string,
                        "lastName": string,
                        "postalCode": string,
                        "stateOrProvince": string
                    },
                    "brand": string,
                    "cardNumber": string,
                    "cvvNumber": string,
                    "expireMonth": integer,
                    "expireYear": integer
                }
              }
            */
            UpdatePaymentInformation payload = new UpdatePaymentInformation();

            payload.creditCard = new CreditCard();
            payload.creditCard.accountHolderName = "";
            payload.creditCard.brand = "";
            payload.creditCard.cardNumber = "";
            payload.creditCard.cardNumber = "";
            payload.creditCard.cvvNumber = "";
            payload.creditCard.expireMonth = 0;
            payload.creditCard.expireYear = 0;

            payload.creditCard.billingAddress = new BillingAddress();
            payload.creditCard.billingAddress.addressLine1 = "";
            payload.creditCard.billingAddress.addressLine2 = "";
            payload.creditCard.billingAddress.city = "";
            payload.creditCard.billingAddress.country = "";
            payload.creditCard.billingAddress.county = "";
            payload.creditCard.billingAddress.firstName = "";
            payload.creditCard.billingAddress.lastName = "";
            payload.creditCard.billingAddress.postalCode = "";
            payload.creditCard.billingAddress.stateOrProvince = "";

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/update_payment_info";

            return Web.Post<CheckoutSessionResponse, UpdatePaymentInformation>(apiUrl, payload);
        }
        

        public static CheckoutSessionResponse InitiateGuestCheckoutSession(string itemId, CustomerResultModel customerInfo, AddressResultModel addressInfo)
        {
            /*
             * Initiate a guest checkout session
                POST https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/initiate

                {
                    "contactEmail" : "fsmith1234@gmail.com",
                    "contactFirstName":"Frank",
                    "contactLastName":"Smith",
                    "shippingAddress" : {
                        "recipient" : "Frank Smith",
                        "phoneNumber" : "617 817 7449 ",
                        "addressLine1" : "3737 Casa Verde St",
                        "city" : "San Jose",
                        "stateOrProvince" : "CA",
                        "postalCode" : "95134",
                        "country" : "US"
                    },
                    "lineItemInputs" : [ {
                        "quantity" : 1,
                        "itemId" : "v1|190006102824|0"
                    }
                    ]
                }
            */
            CreateGuestCheckoutSessionRequest request = new CreateGuestCheckoutSessionRequest();
            request.contactEmail = "fsmith1234@gmail.com";
            request.contactFirstName = "Frank";
            request.contactLastName = "Smith";

            request.shippingAddress = new ShippingAddress();
            request.shippingAddress.recipient = "Frank Smith";
            request.shippingAddress.phoneNumber = "617 817 7449 ";
            request.shippingAddress.addressLine1 = "3737 Casa Verde St";
            request.shippingAddress.city = "San Jose";
            request.shippingAddress.stateOrProvince = "CA";
            request.shippingAddress.postalCode = "95134";
            request.shippingAddress.country = "US";

            request.lineItemInputs = new LineItemInputs[] { new LineItemInputs { quantity = 1, itemId = itemId } };

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/initiate";

            CheckoutSessionResponse response = Web.Post<CheckoutSessionResponse, CreateGuestCheckoutSessionRequest>(apiUrl, request);

            return response;
        }
    }
}