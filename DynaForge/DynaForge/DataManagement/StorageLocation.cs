using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace DataManagement
{
    public class StorageLocation
    {
        private StorageLocation() { }

        /// <returns></returns>
        [MultiReturn(new[] { "bucket", "urn",  "id" })]
        public static Dictionary<string, string> Create(string Token, string projectId, string filename, string folderURN)
        {

            var client = new RestClient("https://developer.api.autodesk.com/data/v1/projects/" + projectId + "/storage");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", generateContent(filename, folderURN), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RootobjectStrCrtResponse deserializedProduct = JsonConvert.DeserializeObject<RootobjectStrCrtResponse>(response.Content);
            

            if (deserializedProduct != null)
            {
                string dataDes = deserializedProduct.data.id;
                string[] dataFiltered = dataDes.Replace("urn:adsk.objects:os.object:", "").Split(new string[] { "/" }, StringSplitOptions.None);

                return new Dictionary<string, string> {
                { "bucket", dataFiltered[0]},
                { "urn", dataFiltered[1]},
                { "id", dataDes }
                };
            }
            else
            {
                return null;
            }

        }

        private static string generateContent(string filename, string folderURN)
        {
            RequestServiceStorage requestServiceStorage = new RequestServiceStorage();
            JsonapiStorageCreate jsonapi = new JsonapiStorageCreate();
            jsonapi.version = "1.0";
            requestServiceStorage.jsonapi = jsonapi;

            DataStorageCreate data = new DataStorageCreate();
            data.type = "objects";

            AttributesStorageCreate attributes = new AttributesStorageCreate();
            attributes.name = filename;
            data.attributes = attributes;

            RelationshipsStorageCreate relationships = new RelationshipsStorageCreate();
            TargetStorageCreate target = new TargetStorageCreate();
            Data1StorageCreate data1 = new Data1StorageCreate();
            data1.type = "folders";
            data1.id = folderURN;
            target.data = data1;
            relationships.target = target;
            data.relationships = relationships;

            requestServiceStorage.data = data;


            string result = Newtonsoft.Json.JsonConvert.SerializeObject(requestServiceStorage);

            return result;
        }
    }


    class RequestServiceStorage
    {
        public JsonapiStorageCreate jsonapi { get; set; }
        public DataStorageCreate data { get; set; }
    }

    class JsonapiStorageCreate
    {
        public string version { get; set; }
    }

    class DataStorageCreate
    {
        public string type { get; set; }
        public AttributesStorageCreate attributes { get; set; }
        public RelationshipsStorageCreate relationships { get; set; }
    }

    class AttributesStorageCreate
    {
        public string name { get; set; }
    }

    class RelationshipsStorageCreate
    {
        public TargetStorageCreate target { get; set; }
    }

    class TargetStorageCreate
    {
        public Data1StorageCreate data { get; set; }
    }

    class Data1StorageCreate
    {
        public string type { get; set; }
        public string id { get; set; }
    }




     class RootobjectStrCrtResponse
    {
        public JsonapiStrCrtResponse jsonapi { get; set; }
        public DataStrCrtResponse data { get; set; }
    }

     class JsonapiStrCrtResponse
    {
        public string version { get; set; }
    }

     class DataStrCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
        public RelationshipsStrCrtResponse relationships { get; set; }
    }

     class RelationshipsStrCrtResponse
    {
        public TargetStrCrtResponse target { get; set; }
    }

     class TargetStrCrtResponse
    {
        public Data1StrCrtResponse data { get; set; }
        public LinksStrCrtResponse links { get; set; }
    }

     class Data1StrCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class LinksStrCrtResponse
    {
        public RelatedStrCrtResponse related { get; set; }
    }

     class RelatedStrCrtResponse
    {
        public string href { get; set; }
    }

}
