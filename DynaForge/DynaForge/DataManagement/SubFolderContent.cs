using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class SubFolderContent
    {
        private SubFolderContent() { }

        /// <returns></returns>
        [MultiReturn(new[] { "name", "id" })]
        public static Dictionary<string, List<string>> Get(string Token, string ProjectId, string FolderURN)
        {
            var client = new RestClient("https://developer.api.autodesk.com/data/v1/projects/" + ProjectId + "/folders/" + FolderURN + "/contents");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=OMbS0dHEDsBCecDAesyAws");
            IRestResponse response = client.Execute(request);

            FolderExists deserializedProduct = JsonConvert.DeserializeObject<FolderExists>(response.Content);

            if (deserializedProduct != null)
            {
                List<string> projectNames = new List<string>();
                List<string> projectIds = new List<string>();

                foreach (Datum i in deserializedProduct.data)
                {
                    projectNames.Add(i.attributes.name);
                    projectIds.Add(i.id);
                }

                return new Dictionary<string, List<string>> {
                { "name", projectNames },
                { "id", projectIds }
                };
            }
            else
            {
                return null;
            }

        }
    }


    internal class FolderExists
    {
        public Jsonapi jsonapi { get; set; }
        public Links links { get; set; }
        public Datum[] data { get; set; }
    }

    internal class Jsonapi
    {
        public string version { get; set; }
    }

    internal class Links
    {
        public Self self { get; set; }
    }

    internal class Self
    {
        public string href { get; set; }
    }

    internal class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Links1 links { get; set; }
        public Relationships relationships { get; set; }
    }

    internal class Attributes
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public DateTime createTime { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public DateTime lastModifiedTime { get; set; }
        public string lastModifiedUserId { get; set; }
        public string lastModifiedUserName { get; set; }
        public DateTime lastModifiedTimeRollup { get; set; }
        public int objectCount { get; set; }
        public bool hidden { get; set; }
        public Extension extension { get; set; }
    }

    internal class Extension
    {
        public string type { get; set; }
        public string version { get; set; }
        public Schema schema { get; set; }
        public Data data { get; set; }
    }

    internal class Schema
    {
        public string href { get; set; }
    }

    internal class Data
    {
        public string[] visibleTypes { get; set; }
        public string[] actions { get; set; }
        public string[] allowedTypes { get; set; }
    }

    internal class Links1
    {
        public Self1 self { get; set; }
        public Webview webView { get; set; }
    }

    internal class Self1
    {
        public string href { get; set; }
    }

    internal class Webview
    {
        public string href { get; set; }
    }

    internal class Relationships
    {
        public Contents contents { get; set; }
        public Parent parent { get; set; }
        public Refs refs { get; set; }
        public Links5 links { get; set; }
    }

    internal class Contents
    {
        public Links2 links { get; set; }
    }

    internal class Links2
    {
        public Related related { get; set; }
    }

    internal class Related
    {
        public string href { get; set; }
    }

    internal class Parent
    {
        public Data1 data { get; set; }
        public Links3 links { get; set; }
    }

    internal class Data1
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    internal class Links3
    {
        public Related1 related { get; set; }
    }

    internal class Related1
    {
        public string href { get; set; }
    }

    internal class Refs
    {
        public Links4 links { get; set; }
    }

    internal class Links4
    {
        public Self2 self { get; set; }
        public Related2 related { get; set; }
    }

    internal class Self2
    {
        public string href { get; set; }
    }

    internal class Related2
    {
        public string href { get; set; }
    }

    internal class Links5
    {
        public Links6 links { get; set; }
    }

    internal class Links6
    {
        public Self3 self { get; set; }
    }

    internal class Self3
    {
        public string href { get; set; }
    }
}
