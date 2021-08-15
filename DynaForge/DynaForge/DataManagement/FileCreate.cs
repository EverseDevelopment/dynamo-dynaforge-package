using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace DataManagement
{
    public class FileCreate
    {
        private FileCreate() { }

        [MultiReturn(new[] { "DisplayName", "Type", "Version", "URN" })]
        public static Dictionary<string, string> Run(string Token, string projectId, string filename, string fileURN, string folderURN)
        {

            var client = new RestClient("https://developer.api.autodesk.com/data/v1/projects/" + projectId + "/items");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Content-Type", "application/vnd.api+json");
            request.AddHeader("Accept", "application/vnd.api+json");
            request.AddParameter("application/vnd.api+json", generateContent(filename, fileURN, folderURN), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RootobjectFileCrtResponse deserializedProduct = JsonConvert.DeserializeObject<RootobjectFileCrtResponse>(response.Content);

            if (deserializedProduct != null)
            {
                IncludedFileCrtResponse lastItem = deserializedProduct.included[deserializedProduct.included.Length - 1];

                return new Dictionary<string, string> {
                { "DisplayName", deserializedProduct.data.attributes.displayName},
                { "Type", deserializedProduct.data.attributes.extension.type},
                { "Version", deserializedProduct.included.Length.ToString()},
                { "URN", lastItem.id}
                };
            }
            else
            {
                return null;
            }

        }

        private static string generateContent(string filename, string fileURN, string folderURN)
        {
            RequestFileCreateFileCrtInput requestFileCreate = new RequestFileCreateFileCrtInput();
            JsonapiFileCrtInput jsonapi = new JsonapiFileCrtInput();
            jsonapi.version = "1.0";
            requestFileCreate.jsonapi = jsonapi;

            DataFileCrtInput data = new DataFileCrtInput();
            data.type = "items";

            AttributesFileCrtInput attributes = new AttributesFileCrtInput();
            attributes.displayName = filename;

            ExtensionFileCrtInput extension = new ExtensionFileCrtInput();
            extension.type = "items:autodesk.bim360:File";
            extension.version = "1.0";
            attributes.extension = extension;
            data.attributes = attributes;

            RelationshipsFileCrtInput relationships = new RelationshipsFileCrtInput();
            TipFileCrtInput tip = new TipFileCrtInput();
            Data1FileCrtInput data1 = new Data1FileCrtInput();
            data1.type = "versions";
            data1.id = "1";
            tip.data = data1;
            relationships.tip = tip;
            ParentFileCrtInput parent = new ParentFileCrtInput();
            Data2FileCrtInput data2 = new Data2FileCrtInput();
            data2.type = "folders";
            data2.id = folderURN;
            parent.data = data2;
            relationships.parent = parent;
            data.relationships = relationships;
            requestFileCreate.data = data;

            IncludedFileCrtInput included = new IncludedFileCrtInput();
            included.type = "versions";
            included.id = "1";

            Attributes1FileCrtInput attributes1 = new Attributes1FileCrtInput();
            attributes1.name = filename;

            Extension1FileCrtInput extension1 = new Extension1FileCrtInput();
            extension1.type = "versions:autodesk.bim360:File";
            extension1.version = "1.0";
            attributes1.extension = extension1;
            included.attributes = attributes1;

            Relationships1FileCrtInput relationships1 = new Relationships1FileCrtInput();
            StorageFileCrtInput storage = new StorageFileCrtInput();
            Data3FileCrtInput data3 = new Data3FileCrtInput();
            data3.type = "objects";
            data3.id = fileURN;
            storage.data = data3;
            relationships1.storage = storage;
            included.relationships = relationships1;
            IncludedFileCrtInput[] includedarray = new IncludedFileCrtInput[] { included };
            requestFileCreate.included = includedarray;

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(requestFileCreate);

            return result;
        }
    }

    class RequestFileCreateFileCrtInput
    {
        public JsonapiFileCrtInput jsonapi { get; set; }
        public DataFileCrtInput data { get; set; }
        public IncludedFileCrtInput[] included { get; set; }
    }

    class JsonapiFileCrtInput
    {
        public string version { get; set; }
    }

    class DataFileCrtInput
    {
        public string type { get; set; }
        public AttributesFileCrtInput attributes { get; set; }
        public RelationshipsFileCrtInput relationships { get; set; }
    }

    class AttributesFileCrtInput
    {
        public string displayName { get; set; }
        public ExtensionFileCrtInput extension { get; set; }
    }

    class ExtensionFileCrtInput
    {
        public string type { get; set; }
        public string version { get; set; }
    }

    class RelationshipsFileCrtInput
    {
        public TipFileCrtInput tip { get; set; }
        public ParentFileCrtInput parent { get; set; }
    }

    class TipFileCrtInput
    {
        public Data1FileCrtInput data { get; set; }
    }

    class Data1FileCrtInput
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    class ParentFileCrtInput
    {
        public Data2FileCrtInput data { get; set; }
    }

    class Data2FileCrtInput
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    class IncludedFileCrtInput
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes1FileCrtInput attributes { get; set; }
        public Relationships1FileCrtInput relationships { get; set; }
    }

    class Attributes1FileCrtInput
    {
        public string name { get; set; }
        public Extension1FileCrtInput extension { get; set; }
    }

    class Extension1FileCrtInput
    {
        public string type { get; set; }
        public string version { get; set; }
    }

    class Relationships1FileCrtInput
    {
        public StorageFileCrtInput storage { get; set; }
    }

    class StorageFileCrtInput
    {
        public Data3FileCrtInput data { get; set; }
    }

    class Data3FileCrtInput
    {
        public string type { get; set; }
        public string id { get; set; }
    }




    class RootobjectFileCrtResponse
    {
        public JsonapiFileCrtResponse jsonapi { get; set; }
        public DataFileCrtResponse data { get; set; }
        public IncludedFileCrtResponse[] included { get; set; }
    }

    class JsonapiFileCrtResponse
    {
        public string version { get; set; }
    }

    class DataFileCrtResponse
    {
        public string type { get; set; }
        public AttributesFileCrtResponse attributes { get; set; }
        public RelationshipsFileCrtResponse relationships { get; set; }
    }

    class AttributesFileCrtResponse
    {
        public string displayName { get; set; }
        public ExtensionFileCrtResponse extension { get; set; }
    }

    class ExtensionFileCrtResponse
    {
        public string type { get; set; }
        public string version { get; set; }
    }

    class RelationshipsFileCrtResponse
    {
        public TipFileCrtResponse tip { get; set; }
        public ParentFileCrtResponse parent { get; set; }
    }

    class TipFileCrtResponse
    {
        public Data1FileCrtResponse data { get; set; }
    }

    class Data1FileCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    class ParentFileCrtResponse
    {
        public Data2FileCrtResponse data { get; set; }
    }

    class Data2FileCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    class IncludedFileCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes1FileCrtResponse attributes { get; set; }
        public Relationships1FileCrtResponse relationships { get; set; }
    }

    class Attributes1FileCrtResponse
    {
        public string name { get; set; }
        public Extension1FileCrtResponse extension { get; set; }
    }

    class Extension1FileCrtResponse
    {
        public string type { get; set; }
        public string version { get; set; }
    }

    class Relationships1FileCrtResponse
    {
        public StorageFileCrtResponse storage { get; set; }
    }

    class StorageFileCrtResponse
    {
        public Data3FileCrtResponse data { get; set; }
    }

    class Data3FileCrtResponse
    {
        public string type { get; set; }
        public string id { get; set; }
    }

}
