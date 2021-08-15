using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace TokenFlex
{
    public class ContractNumber
    {
        private ContractNumber() { }

        public static List<Contracts> Get(string Token)
        {

            var client = new RestClient("https://developer.api.autodesk.com/tokenflex/v1/contract");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=dA1dOj5zo1AhsRo55Ccip1");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            RootobjectContract deserializedProduct = JsonConvert.DeserializeObject<RootobjectContract>(response.Content);

            if (deserializedProduct != null)
            {
                return deserializedProduct.Property1.ToList();
            }
            else
            {
                return new List<Contracts>();
            }

        }
    }


    public class RootobjectContract
    {
        public Contracts[] Property1 { get; set; }
    }

    public class Contracts
    {
        public string contractNumber { get; set; }
        public string contractName { get; set; }
        public string contractStartDate { get; set; }
        public string contractEndDate { get; set; }
        public bool isActive { get; set; }
    }


}
