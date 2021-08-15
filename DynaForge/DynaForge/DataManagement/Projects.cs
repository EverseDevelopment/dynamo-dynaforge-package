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
    public class Projects
    {
        private Projects() { }

        /// <returns></returns>
        [MultiReturn(new[] { "name", "id" })]
        public static Dictionary<string, List<string>> Get(string Token, string hubId)
        {
            var client = new RestClient("https://developer.api.autodesk.com/project/v1/hubs/" + hubId + "/projects");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=a4JlAERuHOUkuL1gToj07k");
            IRestResponse response = client.Execute(request);

            RootobjectProjects deserializedProduct = JsonConvert.DeserializeObject<RootobjectProjects>(response.Content);


            if (deserializedProduct != null)
            {
                List<string> projectNames = new List<string>();
                List<string> projectIds = new List<string>();

                foreach (DatumProjects i in deserializedProduct.data)
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

    class RootobjectProjects
    {
        public JsonapiProjects jsonapi { get; set; }
        public LinksProjects links { get; set; }
        public DatumProjects[] data { get; set; }
    }

     class JsonapiProjects
    {
        public string version { get; set; }
    }

     class LinksProjects
    {
        public SelfProjects self { get; set; }
    }

     class SelfProjects
    {
        public string href { get; set; }
    }

     internal class DatumProjects
    {
        public string type { get; set; }
        public string id { get; set; }
        public AttributesProjects attributes { get; set; }
        public Links1Projects links { get; set; }
        public RelationshipsProjects relationships { get; set; }
    }

     class AttributesProjects
    {
        public string name { get; set; }
        public string[] scopes { get; set; }
        public ExtensionProjects extension { get; set; }
    }

     class ExtensionProjects
    {
        public string type { get; set; }
        public string version { get; set; }
        public SchemaProjects schema { get; set; }
        public DataProjects data { get; set; }
    }

     class SchemaProjects
    {
        public string href { get; set; }
    }

     class DataProjects
    {
        public string projectType { get; set; }
    }

     class Links1Projects
    {
        public Self1Projects self { get; set; }
        public WebviewProjects webView { get; set; }
    }

     class Self1Projects
    {
        public string href { get; set; }
    }

     class WebviewProjects
    {
        public string href { get; set; }
    }

     class RelationshipsProjects
    {
        public HubProjects hub { get; set; }
        public RootfolderProjects rootFolder { get; set; }
        public TopfoldersProjects topFolders { get; set; }
        public IssuesProjects issues { get; set; }
        public SubmittalsProjects submittals { get; set; }
        public RfisProjects rfis { get; set; }
        public MarkupsProjects markups { get; set; }
        public ChecklistsProjects checklists { get; set; }
        public CostProjects cost { get; set; }
        public LocationsProjects locations { get; set; }
    }

     class HubProjects
    {
        public Data1Projects data { get; set; }
        public Links2Projects links { get; set; }
    }

     class Data1Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Links2Projects
    {
        public RelatedProjects related { get; set; }
    }

     class RelatedProjects
    {
        public string href { get; set; }
    }

     class RootfolderProjects
    {
        public Data2Projects data { get; set; }
        public MetaProjects meta { get; set; }
    }

     class Data2Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class MetaProjects
    {
        public LinkProjects link { get; set; }
    }

     class LinkProjects
    {
        public string href { get; set; }
    }

     class TopfoldersProjects
    {
        public Links3Projects links { get; set; }
    }

     class Links3Projects
    {
        public Related1Projects related { get; set; }
    }

     class Related1Projects
    {
        public string href { get; set; }
    }

     class IssuesProjects
    {
        public Data3Projects data { get; set; }
        public Meta1Projects meta { get; set; }
    }

     class Data3Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta1Projects
    {
        public Link1Projects link { get; set; }
    }

     class Link1Projects
    {
        public string href { get; set; }
    }

     class SubmittalsProjects
    {
        public Data4Projects data { get; set; }
        public Meta2Projects meta { get; set; }
    }

     class Data4Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta2Projects
    {
        public Link2Projects link { get; set; }
    }

     class Link2Projects
    {
        public string href { get; set; }
    }

     class RfisProjects
    {
        public Data5Projects data { get; set; }
        public Meta3Projects meta { get; set; }
    }

     class Data5Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta3Projects
    {
        public Link3Projects link { get; set; }
    }

     class Link3Projects
    {
        public string href { get; set; }
    }

     class MarkupsProjects
    {
        public Data6Projects data { get; set; }
        public Meta4Projects meta { get; set; }
    }

     class Data6Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta4Projects
    {
        public Link4Projects link { get; set; }
    }

     class Link4Projects
    {
        public string href { get; set; }
    }

     class ChecklistsProjects
    {
        public Data7Projects data { get; set; }
        public Meta5Projects meta { get; set; }
    }

     class Data7Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta5Projects
    {
        public Link5Projects link { get; set; }
    }

     class Link5Projects
    {
        public string href { get; set; }
    }

     class CostProjects
    {
        public Data8Projects data { get; set; }
        public Meta6Projects meta { get; set; }
    }

     class Data8Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta6Projects
    {
        public Link6Projects link { get; set; }
    }

     class Link6Projects
    {
        public string href { get; set; }
    }

     class LocationsProjects
    {
        public Data9Projects data { get; set; }
        public Meta7Projects meta { get; set; }
    }

     class Data9Projects
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta7Projects
    {
        public Link7Projects link { get; set; }
    }

     class Link7Projects
    {
        public string href { get; set; }
    }

}
