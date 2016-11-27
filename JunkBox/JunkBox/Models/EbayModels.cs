using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;

namespace JunkBox.Models
{
    public class EbayGetSomethingModel
    {
        public string email { get; set; }
    }



    /*
     * "{"findItemsByKeywordsResponse": [ {
     *                                      "ack":["Success"],
     *                                      "version":["1.13.0"],
     *                                      "timestamp":["2016-11-27T00:11:37.814Z"],
     *                                      "searchResult": [ {
     *                                                          "@count":"3",
     *                                                          "item": [ { 
     *                                                                      "itemId":["322281134362"],
     *                                                                      "title":["Harry Potter: Complete 8-Film Collection (DVD, 2011, 8-Disc Set)"],
     *                                                                      "globalId":["EBAY-US"],
     *                                                                      "primaryCategory": [ {
     *                                                                                             "categoryId":["617"],
     *                                                                                             "categoryName":["DVDs & Blu-ray Discs"]
     *                                                                                          }],
     *                                                                      "galleryURL":["http:\/\/thumbs3.ebaystatic.com\/m\/mxwsR-n2s4Re0nQCMUYghhg\/140.jpg"],
     *                                                                      "viewItemURL":["http:\/\/www.ebay.com\/itm\/Harry-Potter-Complete-8-Film-Collection-DVD-2011-8-Disc-Set-\/322281134362"],
     *                                                                      "productId":[{"@type":"ReferenceID","__value__":"110258144"}],
     *                                                                      "paymentMethod":["PayPal"],
     *                                                                      "autoPay":["true"],
     *                                                                      "postalCode":["19082"],
     *                                                                      "location":["Upper Darby,PA,USA"],
     *                                                                      "country":["US"],
     *                                                                      "shippingInfo":[{"shippingServiceCost":[{"@currencyId":"USD","__value__":"0.0"}],"shippingType":["Free"],"shipToLocations":["US"],"expeditedShipping":["false"],"oneDayShippingAvailable":["false"],"handlingTime":["1"]}],
     *                                                                      "sellingStatus":[{"currentPrice":[{"@currencyId":"USD","__value__":"22.99"}],
     *                                                                      "convertedCurrentPrice":[{"@currencyId":"USD","__value__":"22.99"}],
     *                                                                      "sellingState":["Active"],
     *                                                                      "timeLeft":["P5DT0H22M2S"]
     *                                                                   }],
     *                                                           "listingInfo":[{
     *                                                              "bestOfferEnabled":["false"],
     *                                                              "buyItNowAvailable":["false"],
     *                                                              "startTime":["2016-10-03T00:33:39.000Z"],
     *                                                              "endTime":["2016-12-02T00:33:39.000Z"],
     *                                                              "listingType":["StoreInventory"],
     *                                                              "gift":["false"]
     *                                                           }],
     *                                                           "returnsAccepted":["true"],
     *                                                           "condition":[{
     *                                                              "conditionId":["1000"],
     *                                                              "conditionDisplayName":["Brand New"]
     *                                                           }],
     *                                                           "isMultiVariationListing":["false"],
     *                                                           "topRatedListing":["false"]},
     *                                                           {
     *                                                              "itemId":["360778402701"],
     *                                                              "title":["Harry Potter Complete Book Series Special Edition Boxed Set by J.K. Rowling NEW!"],
     *                                                              "globalId":["EBAY-US"],
     *                                                              "primaryCategory":[
     *                                                                  {
     *                                                                      "categoryId":["279"],
     *                                                                      "categoryName":["Other Children & Young Adults"]
     *                                                                  }
     *                                                              ],
     *                                                              "galleryURL":["http:\/\/thumbs2.ebaystatic.com\/m\/mH8W1KPPrORYcke2pI3LSMw\/140.jpg"],
     *                                                              "viewItemURL":["http:\/\/www.ebay.com\/itm\/Harry-Potter-Complete-Book-Series-Special-Edition-Boxed-Set-J-K-Rowling-NEW-\/360778402701"],
     *                                                              "paymentMethod":["PayPal"],
     *                                                              "autoPay":["true"],
     *                                                              "location":["USA"],
     *                                                              "country":["US"],
     *                                                              "shippingInfo":[
     *                                                                  {
     *                                                                      "shippingServiceCost":[
     *                                                                          {
     *                                                                              "@currencyId":"USD",
     *                                                                              "__value__":"3.99"
     *                                                                          }
     *                                                                      ],
     *                                                                      "shippingType":["Flat"],
     *                                                                      "shipToLocations":["Worldwide"],
     *                                                                      "expeditedShipping":["true"],
     *                                                                      "oneDayShippingAvailable":["true"],
     *                                                                      "handlingTime":["3"]}],"sellingStatus":[{"currentPrice":[{"@currencyId":"USD","__value__":"75.01"}],"convertedCurrentPrice":[{"@currencyId":"USD","__value__":"75.01"}],"sellingState":["Active"],"timeLeft":["P15DT16H26M45S"]}],"listingInfo":[{"bestOfferEnabled":["false"],"buyItNowAvailable":["false"],"startTime":["2013-10-29T16:38:22.000Z"],"endTime":["2016-12-12T16:38:22.000Z"],"listingType":["FixedPrice"],"gift":["false"]}],"returnsAccepted":["true"],"condition":[{"conditionId":["1000"],"conditionDisplayName":["Brand New"]}],"isMultiVariationListing":["false"],"topRatedListing":["false"]},{"itemId":["272393126619"],"title":["Harry Potter: Complete 8-Film Collection (DVD, 2011, 8-Disc Set)"],"globalId":["EBAY-US"],"primaryCategory":[{"categoryId":["617"],"categoryName":["DVDs & Blu-ray Discs"]}],"galleryURL":["http:\/\/thumbs4.ebaystatic.com\/m\/mopkgdsKgIZEc6oprczxMaA\/140.jpg"],"viewItemURL":["http:\/\/www.ebay.com\/itm\/Harry-Potter-Complete-8-Film-Collection-DVD-2011-8-Disc-Set-\/272393126619"],"productId":[{"@type":"ReferenceID","__value__":"110258144"}],"paymentMethod":["PayPal"],"autoPay":["false"],"postalCode":["38665"],"location":["Sarah,MS,USA"],"country":["US"],"shippingInfo":[{"shippingServiceCost":[{"@currencyId":"USD","__value__":"0.0"}],"shippingType":["Free"],"shipToLocations":["US"],"expeditedShipping":["true"],"oneDayShippingAvailable":["false"],"handlingTime":["1"]}],"sellingStatus":[{"currentPrice":[{"@currencyId":"USD","__value__":"18.88"}],"convertedCurrentPrice":[{"@currencyId":"USD","__value__":"18.88"}],"sellingState":["Active"],"timeLeft":["P0DT5H47M13S"]}],"listingInfo":[{"bestOfferEnabled":["false"],"buyItNowAvailable":["false"],"startTime":["2016-09-28T05:58:50.000Z"],"endTime":["2016-11-27T05:58:50.000Z"],"listingType":["StoreInventory"],"gift":["false"]}],"returnsAccepted":["true"],"condition":[{"conditionId":["1000"],"conditionDisplayName":["Brand New"]}],"isMultiVariationListing":["false"],"topRatedListing":["true"]}]}],"paginationOutput":[{"pageNumber":["1"],"entriesPerPage":["3"],"totalPages":["76408"],"totalEntries":["229223"]}],"itemSearchURL":["http:\/\/www.ebay.com\/sch\/i.html?_nkw=harry+potter&_ddo=1&_ipg=3&_pgn=1"]}]}" */

}
 