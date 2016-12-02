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


        public static IDictionary<string, object> PlaceGuestOrder(string checkoutSessionId)
        {
            //POST https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/{guest_checkoutsession_id}/place_order

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/place_order";

            return Web.PostWebRequest(apiUrl, "");
        }


        public static IDictionary<string, object> UpdateGuestSessionPaymentInfo(string checkoutSessionId, Dictionary<string, string> customerInfo, Dictionary<string, string> addressInfo)
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

            EbayUpdateGuestSessionPaymentInfoModel payload = new EbayUpdateGuestSessionPaymentInfoModel();

            EbayCreditCardModel creditCard = new EbayCreditCardModel();
            creditCard.accountHolderName = "Frank Smith";
            creditCard.brand = "MASTERCARD";
            creditCard.cardNumber = "5100000001598174";
            creditCard.cvvNumber = "012";
            creditCard.expireMonth = 10;
            creditCard.expireYear = 2019;

            EbayBillingAddressModel billingAddress = new EbayBillingAddressModel();
            billingAddress.firstName = "Frank";
            billingAddress.lastName = "Smith";
            billingAddress.addressLine1 = "3737 Any St";
            billingAddress.addressLine2 = "";
            billingAddress.city = "San Jose";
            billingAddress.stateOrProvince = "CA";
            billingAddress.postalCode = "95134";
            billingAddress.country = "US";

            creditCard.billingAddress = billingAddress;
            payload.creditCard = creditCard;

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/update_payment_info";

            var json_serializer = new JavaScriptSerializer();
            string postBody = json_serializer.Serialize(payload);
            //System.Windows.Forms.MessageBox.Show(postBody);

            return Web.PostWebRequest(apiUrl, postBody);
        }


        public static IDictionary<string, object> InitiateGuestCheckoutSession(string itemId, CustomerResultModel customerInfo, AddressModel addressInfo)
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
            EbayGuestCheckoutSessionRequestModel payload = new EbayGuestCheckoutSessionRequestModel();
            payload.contactEmail = customerInfo.Email;
            payload.contactFirstName = customerInfo.FirstName;
            payload.contactLastName = customerInfo.LastName;

            EbayShippingAddressModel shipping = new EbayShippingAddressModel();
            shipping.recipient = customerInfo.FirstName + " " + customerInfo.LastName;
            shipping.phoneNumber = customerInfo.Phone;
            shipping.addressLine1 = addressInfo.ShippingAddress;
            shipping.addressLine2 = addressInfo.ShippingAddress2;
            shipping.city = addressInfo.ShippingCity;
            shipping.stateOrProvince = addressInfo.ShippingState;
            shipping.postalCode = addressInfo.ShippingZip.ToString();
            shipping.country = "US";

            payload.shippingAddress = shipping;

            EbayLineItemModel[] lineItems = new EbayLineItemModel[] {
                new EbayLineItemModel { quantity = 1, itemId = itemId }
            };

            payload.lineItemInputs = lineItems;

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/initiate";

            var json_serializer = new JavaScriptSerializer();
            string postBody = json_serializer.Serialize(payload);

            return Web.PostWebRequest(apiUrl, postBody);
        }
    }
}