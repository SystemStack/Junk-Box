using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

using JunkBox.Common;
using JunkBox.DataAccess;
using JunkBox.Models;

namespace JunkBox.Ebay
{
    public class Auth
    {
        private static string appIdSandbox = ConfigurationManager.AppSettings["AppIDSandBox"]; //Our 'Client ID'
        private static string certIdSandbox = ConfigurationManager.AppSettings["CertIDSandBox"];//Our 'Client Secret'

        private static AccessTokenTable accessTokenTable = AccessTokenTable.Instance();

        public static IDictionary<string, object> RequestApplicationAccessToken()
        {
            /*
             * The Authorization header requires a Base64-encoded value that is comprised of the client ID and client secret values.
             * To generate this value, combine your application's client ID and client secret values by separating them with a colon, 
             * and Base64 encode those combined values. In other words, Base64 encode the following: <client_id>:<client_secret>.
             * POST https://api.sandbox.ebay.com/identity/v1/oauth2/token
             
                HTTP headers:
                    Content-Type = application/x-www-form-urlencoded
                    Authorization = Basic <B64-encoded-oauth-credentials>

                HTTP method: POST

                URL: https://api.sandbox.ebay.com/identity/v1/oauth2/token

                Request body (wrapped for readability):
                    grant_type=client_credentials&
                    redirect_uri=<redirect_URI>&
                    scope=https://api.ebay.com/oauth/api_scope


                EXAMPLE RESPONSE:
                    access_token : "[long ass sequence of characters]"
                    expires_in : 7200
                    refresh_token : "N/A"
                    token_type : "Application Access Token"
             */

            var plainTextBytes = Encoding.UTF8.GetBytes(appIdSandbox + ":" + certIdSandbox);
            string authHeader = Convert.ToBase64String(plainTextBytes);
            string urlApi = "https://api.sandbox.ebay.com/identity/v1/oauth2/token";

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["grant_type"] = "client_credentials";
            query["redirect_uri"] = "";
            query["scope"] = "https://api.ebay.com/oauth/api_scope https://api.ebay.com/oauth/api_scope/buy.guest.order";

            return Web.PostTokenRequest(urlApi, authHeader, query.ToString());
        }

        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static bool IsAccessTokenValid()
        {
            AccessTokenResultModel accessToken = accessTokenTable.SelectRecord();

            DateTime creationTime = accessToken.DateCreated;
            DateTime currentTime = DateTime.Now;

            double timeDifference = (currentTime - creationTime).TotalSeconds;
            double expireLength = (double)accessToken.ExpiresIn;

            if (timeDifference <= expireLength)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string GetAccessToken()
        {
            AccessTokenResultModel accessToken = accessTokenTable.SelectRecord();

            return accessToken.AccessToken;
        }

        public static int UpdateAccessToken()
        {

            DateTime updateTime = DateTime.Now;
            IDictionary<string, object> tokenResponse = RequestApplicationAccessToken();

            UpdateAccessTokenModel accessToken = new UpdateAccessTokenModel()
            {
                AccessToken = tokenResponse["access_token"].ToString(),
                ExpiresIn = (int)tokenResponse["expires_in"],
                RefreshToken = tokenResponse["refresh_token"].ToString(),
                DateCreated = updateTime,
                UseType = "ApplicationAccessToken",
                TokenType = (string)tokenResponse["token_type"]
            };

            NonQueryResultModel tokenResult = accessTokenTable.UpdateRecord(accessToken);

            if (tokenResult.Success)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}