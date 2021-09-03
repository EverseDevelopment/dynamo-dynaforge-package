using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class Authenticate
    {
        //Empty contstructor to avoid import in Dynamo
        private Authenticate() { }

        /// <summary>
        /// This is a test method
        /// </summary>
        /// <returns></returns>
        public static string Auth(string ClientId, string ClientSecret, string scope)
        {
            var client = new RestClient("https://developer.api.autodesk.com/authentication/v1/authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            var FORGE_CLIENT_ID = ClientId;
            var FORGE_CLIENT_SECRET = ClientSecret;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", FORGE_CLIENT_ID);
            request.AddParameter("client_secret", FORGE_CLIENT_SECRET);
            request.AddParameter("scope", scope);
            if (FORGE_CLIENT_ID == null || FORGE_CLIENT_SECRET == null)
            {
                return null;
            }
            else
            {
                IRestResponse response = client.Execute(request);
                Token deserializedProduct = JsonConvert.DeserializeObject<Token>(response.Content);
                string token = "Bearer " + deserializedProduct.access_token;
                return token;
            }
        }
    }

    class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
