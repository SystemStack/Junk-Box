using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class AccessTokenTable
    {
        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static AccessTokenModel GetAccessToken()
        {
            string query = "SELECT * FROM AccessToken WHERE UseType='ApplicationAccessToken'";

            IDictionary<string, object> result = dataAccess.Select(query, null).First();

            AccessTokenModel payload = new AccessTokenModel() {
                AccessToken = (string)result["AccessToken"],
                ExpiresIn = (int)result["ExpiresIn"],
                RefreshToken = (string)result["RefreshToken"],
                TokenType = (string)result["TokenType"],
                UseType = (string)result["UseType"],
                DateCreated = (DateTime)result["DateCreated"]
            };

            return payload;
        }

        public static NonQueryResultModel UpdateAccessToken(AccessTokenModel tokenData)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@UseType", tokenData.UseType },
                { "@AccessToken", tokenData.AccessToken },
                { "@ExpiresIn", tokenData.ExpiresIn },
                { "@RefreshToken", tokenData.RefreshToken },
                { "@TokenType", tokenData.TokenType },
                { "@DateCreated", tokenData.DateCreated }
            };

            //UPDATE table_name SET column1 = value, column2 = value2,... WHERE some_column = some_value
            string query = "UPDATE AccessToken SET AccessToken=@AccessToken, ExpiresIn=@ExpiresIn, RefreshToken=@RefreshToken, TokenType=@TokenType, DateCreated=@DateCreated WHERE UseType=@UseType";

            int result = dataAccess.Update(query, parameters);

            bool succeeded = false;
            if (result == 1)
            {
                succeeded = true;
            }

            NonQueryResultModel payload = new NonQueryResultModel()
            {
                Success = succeeded
            };

            return payload;
        }
    }
}