using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class AccessTokenTable : DataTable, IDataTable<AccessTokenResultModel, SelectAccessTokenModel, NonQueryResultModel, InsertAccessTokenModel, NonQueryResultModel, UpdateAccessTokenModel, NonQueryResultModel, DeleteAccessTokenModel>
    {
        private static AccessTokenTable instance = null;

        private AccessTokenTable(IDataAccess access) : base(access)
        {

        }

        public static AccessTokenTable Instance(IDataAccess access)
        {
            if (instance == null)
            {
                instance = new AccessTokenTable(access);
            }

            return instance;
        }

        public NonQueryResultModel DeleteRecord(DeleteAccessTokenModel parameters)
        {
            throw new NotImplementedException();
        }

        public NonQueryResultModel InsertRecord(InsertAccessTokenModel parameters)
        {
            throw new NotImplementedException();
        }

        public AccessTokenResultModel SelectRecord(SelectAccessTokenModel parameters)
        {
            throw new NotImplementedException();
        }

        public AccessTokenResultModel SelectRecord()
        {
            string query = "SELECT * FROM AccessToken WHERE UseType='ApplicationAccessToken'";

            IDictionary<string, object> result = dataAccess.Select(query, null).First();

            AccessTokenResultModel payload = new AccessTokenResultModel()
            {
                AccessToken = (string)result["AccessToken"],
                ExpiresIn = (int)result["ExpiresIn"],
                RefreshToken = (string)result["RefreshToken"],
                TokenType = (string)result["TokenType"],
                UseType = (string)result["UseType"],
                DateCreated = (DateTime)result["DateCreated"]
            };

            return payload;
        }

        public NonQueryResultModel UpdateRecord(UpdateAccessTokenModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@UseType", parameters.UseType },
                { "@AccessToken", parameters.AccessToken },
                { "@ExpiresIn", parameters.ExpiresIn },
                { "@RefreshToken", parameters.RefreshToken },
                { "@TokenType", parameters.TokenType },
                { "@DateCreated", parameters.DateCreated }
            };

            string query = "UPDATE AccessToken SET AccessToken=@AccessToken, ExpiresIn=@ExpiresIn, RefreshToken=@RefreshToken, TokenType=@TokenType, DateCreated=@DateCreated WHERE UseType=@UseType";

            int result = dataAccess.Update(query, param);

            return PrepareNonQueryResult(result);
        }

    }
}