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
    public class TopFolderContent
    {
        private TopFolderContent() { }

        /// <returns></returns>
        [MultiReturn(new[] { "name", "URN" })]
        public static Dictionary<string, List<string>> Get(string Token, string hubId, string projectId)
        {
            var client = new RestClient("https://developer.api.autodesk.com/project/v1/hubs/" + hubId + "/projects/" + projectId + "/topFolders");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=a4JlAERuHOUkuL1gToj07k");
            IRestResponse response = client.Execute(request);

            RootobjectTopFolder deserializedProduct = JsonConvert.DeserializeObject<RootobjectTopFolder>(response.Content);


            if (deserializedProduct != null)
            {
                List<string> projectNames = new List<string>();
                List<string> projectIds = new List<string>();

                foreach (DatumTopFolder i in deserializedProduct.data)
                {
                    projectNames.Add(i.attributes.name);
                    projectIds.Add(i.id);
                }

                return new Dictionary<string, List<string>> {
                { "name", projectNames },
                { "URN", projectIds }
                };
            }
            else
            {
                return null;
            }

        }
    }



     class RootobjectTopFolder
    {
        public JsonapiTopFolder jsonapi { get; set; }
        public LinksTopFolder links { get; set; }
        public DatumTopFolder[] data { get; set; }
    }

     class JsonapiTopFolder
    {
        public string version { get; set; }
    }

     class LinksTopFolder
    {
        public SelfTopFolder self { get; set; }
    }

     class SelfTopFolder
    {
        public string href { get; set; }
    }

     class DatumTopFolder
    {
        public string type { get; set; }
        public string id { get; set; }
        public AttributesTopFolder attributes { get; set; }
        public Links1TopFolder links { get; set; }
        public RelationshipsTopFolder relationships { get; set; }
    }

     class AttributesTopFolder
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
        public ExtensionTopFolder extension { get; set; }
    }

     class ExtensionTopFolder
    {
        public string type { get; set; }
        public string version { get; set; }
        public SchemaTopFolder schema { get; set; }
        public DataTopFolder data { get; set; }
    }

     class SchemaTopFolder
    {
        public string href { get; set; }
    }

     class DataTopFolder
    {
        public string[] visibleTypes { get; set; }
        public string[] actions { get; set; }
        public string[] allowedTypes { get; set; }
    }

     class Links1TopFolder
    {
        public Self1TopFolder self { get; set; }
        public WebviewTopFolder webView { get; set; }
    }

     class Self1TopFolder
    {
        public string href { get; set; }
    }

     class WebviewTopFolder
    {
        public string href { get; set; }
    }

     class RelationshipsTopFolder
    {
        public ContentsTopFolder contents { get; set; }
        public ParentTopFolder parent { get; set; }
        public RefsTopFolder refs { get; set; }
        public Links5TopFolder links { get; set; }
    }

     class ContentsTopFolder
    {
        public Links2 links { get; set; }
    }

     class Links2TopFolder
    {
        public RelatedTopFolder related { get; set; }
    }

     class RelatedTopFolder
    {
        public string href { get; set; }
    }

     class ParentTopFolder
    {
        public Data1TopFolder data { get; set; }
        public Links3TopFolder links { get; set; }
    }

     class Data1TopFolder
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Links3TopFolder
    {
        public Related1TopFolder related { get; set; }
    }

     class Related1TopFolder
    {
        public string href { get; set; }
    }

     class RefsTopFolder
    {
        public Links4TopFolder links { get; set; }
    }

     class Links4TopFolder
    {
        public Self2TopFolder self { get; set; }
        public Related2TopFolder related { get; set; }
    }

     class Self2TopFolder
    {
        public string href { get; set; }
    }

     class Related2TopFolder
    {
        public string href { get; set; }
    }

     class Links5TopFolder
    {
        public Links6TopFolder links { get; set; }
    }

     class Links6TopFolder
    {
        public Self3TopFolder self { get; set; }
    }

     class Self3TopFolder
    {
        public string href { get; set; }
    }

}
