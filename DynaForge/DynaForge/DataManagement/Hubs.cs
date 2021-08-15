using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;

namespace DataManagement
{
    public class Hubs
    {
        private Hubs() { }

        /// <returns></returns>
        [MultiReturn(new[] { "name", "id" })]
        public static Dictionary<string, List<string>> Get(string Token)
        {

            var client = new RestClient("https://developer.api.autodesk.com/project/v1/hubs");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=OMbS0dHEDsBCecDAesyAws");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RootobjectHubs deserializedProduct = JsonConvert.DeserializeObject<RootobjectHubs>(response.Content);


            if(deserializedProduct != null)
            {
                List<string> hubNames = new List<string>();
                List<string> hubIds = new List<string>();

                foreach (DatumHubs i in deserializedProduct.data)
                {
                    hubNames.Add(i.attributes.name);
                    hubIds.Add(i.id);
                }

                return new Dictionary<string, List<string>> {
                { "name", hubNames },
                { "id", hubIds }
                };
            }
            else
            {
                return null;
            }
          
        }
    }


    class RootobjectHubs
    {
        public JsonapiHubs jsonapi { get; set; }
        public LinksHubs links { get; set; }
        public DatumHubs[] data { get; set; }
        public MetaHubs meta { get; set; }
    }

     class JsonapiHubs
    {
        public string version { get; set; }
    }

     class LinksHubs
    {
        public SelfHubs self { get; set; }
    }

     class SelfHubs
    {
        public string href { get; set; }
    }

     class MetaHubs
    {
        public WarningHubs[] warnings { get; set; }
    }

     class WarningHubs
    {
        public object Id { get; set; }
        public string HttpStatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public object AboutLink { get; set; }
        public object[] Source { get; set; }
        public object[] meta { get; set; }
    }

     class DatumHubs
    {
        public string type { get; set; }
        public string id { get; set; }
        public AttributesHubs attributes { get; set; }
        public Links1Hubs links { get; set; }
        public RelationshipsHubs relationships { get; set; }
    }

     class AttributesHubs
    {
        public string name { get; set; }
        public ExtensionHubs extension { get; set; }
        public string region { get; set; }
    }

     class ExtensionHubs
    {
        public string type { get; set; }
        public string version { get; set; }
        public SchemaHubs schema { get; set; }
        public DataHubs data { get; set; }
    }

     class SchemaHubs
    {
        public string href { get; set; }
    }

     class DataHubs
    {
    }

     class Links1Hubs
    {
        public Self1Hubs self { get; set; }
    }

     class Self1Hubs
    {
        public string href { get; set; }
    }

     class RelationshipsHubs
    {
        public ProjectsHubs projects { get; set; }
    }

     class ProjectsHubs
    {
        public Links2Hubs links { get; set; }
    }

     class Links2Hubs
    {
        public RelatedHubs related { get; set; }
    }

     class RelatedHubs
    {
        public string href { get; set; }
    }

}
