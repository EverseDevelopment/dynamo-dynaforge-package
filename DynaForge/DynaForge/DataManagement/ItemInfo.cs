using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class ItemInfo
    {
        //Empty contstructor to avoid import in Dynamo
        private ItemInfo() { }

        /// <summary>
        /// This is a test method
        /// </summary>
        /// <returns></returns>
        public static List<string> Get(string Token, string ProjectId, string ItemId)
        {
            var client = new RestClient("https://developer.api.autodesk.com/data/v1/projects/" + ProjectId + "/items/" + ItemId);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Acc", "application/zip");
            request.AddHeader("Cookie", "PF=a4JlAERuHOUkuL1gToj07k");
            IRestResponse response = client.Execute(request);

            RootobjectItemInfo deserializedProduct = JsonConvert.DeserializeObject<RootobjectItemInfo>(response.Content);


            if (deserializedProduct != null)
            {
                List<string> Properties = new List<string>();
                Properties.Add("id: " + deserializedProduct.data.id);
                Properties.Add("displayName: " + deserializedProduct.data.attributes.displayName);
                Properties.Add("createTime: " + deserializedProduct.data.attributes.createTime);
                Properties.Add("createUserId: " + deserializedProduct.data.attributes.createUserId);
                Properties.Add("createUserName: " + deserializedProduct.data.attributes.createUserName);
                Properties.Add("lastModifiedTime: " + deserializedProduct.data.attributes.lastModifiedTime);
                Properties.Add("lastModifiedUserId: " + deserializedProduct.data.attributes.lastModifiedUserId);
                Properties.Add("lastModifiedUserName: " + deserializedProduct.data.attributes.lastModifiedUserName);
                Properties.Add("type: " + deserializedProduct.data.attributes.extension.type);
                Properties.Add("version: " + deserializedProduct.included.Count().ToString());
                Properties.Add("sourceFileName: " + deserializedProduct.data.attributes.extension.data.sourceFileName);

                return Properties;
            }
            else
            {
                return null;
            }
        }
    }


     class RootobjectItemInfo
    {
        public JsonapiItemInfo jsonapi { get; set; }
        public LinksItemInfo links { get; set; }
        public DataItemInfo data { get; set; }
        public IncludedItemInfo[] included { get; set; }
    }

     class JsonapiItemInfo
    {
        public string version { get; set; }
    }

     class LinksItemInfo
    {
        public SelfItemInfo self { get; set; }
    }

     class SelfItemInfo
    {
        public string href { get; set; }
    }

     class DataItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
        public AttributesItemInfo attributes { get; set; }
        public Links1ItemInfo links { get; set; }
        public RelationshipsItemInfo relationships { get; set; }
    }

     class AttributesItemInfo
    {
        public string displayName { get; set; }
        public DateTime createTime { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public DateTime lastModifiedTime { get; set; }
        public string lastModifiedUserId { get; set; }
        public string lastModifiedUserName { get; set; }
        public bool hidden { get; set; }
        public bool reserved { get; set; }
        public ExtensionItemInfo extension { get; set; }
    }

     class ExtensionItemInfo
    {
        public string type { get; set; }
        public string version { get; set; }
        public SchemaItemInfo schema { get; set; }
        public Data1ItemInfo data { get; set; }
    }

     class SchemaItemInfo
    {
        public string href { get; set; }
    }

     class Data1ItemInfo
    {
        public string sourceFileName { get; set; }
    }

     class Links1ItemInfo
    {
        public Self1ItemInfo self { get; set; }
        public WebviewItemInfo webView { get; set; }
    }

     class Self1ItemInfo
    {
        public string href { get; set; }
    }

     class WebviewItemInfo
    {
        public string href { get; set; }
    }

     class RelationshipsItemInfo
    {
        public TipItemInfo tip { get; set; }
        public VersionsItemInfo versions { get; set; }
        public ParentItemInfo parent { get; set; }
        public RefsItemInfo refs { get; set; }
        public Links6ItemInfo links { get; set; }
    }

     class TipItemInfo
    {
        public Data2ItemInfo data { get; set; }
        public Links2ItemInfo links { get; set; }
    }

     class Data2ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Links2ItemInfo
    {
        public RelatedItemInfo related { get; set; }
    }

     class RelatedItemInfo
    {
        public string href { get; set; }
    }

     class VersionsItemInfo
    {
        public Links3ItemInfo links { get; set; }
    }

     class Links3ItemInfo
    {
        public Related1ItemInfo related { get; set; }
    }

     class Related1ItemInfo
    {
        public string href { get; set; }
    }

     class ParentItemInfo
    {
        public Data3ItemInfo data { get; set; }
        public Links4ItemInfo links { get; set; }
    }

     class Data3ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Links4ItemInfo
    {
        public Related2ItemInfo related { get; set; }
    }

     class Related2ItemInfo
    {
        public string href { get; set; }
    }

     class RefsItemInfo
    {
        public Links5ItemInfo links { get; set; }
    }

     class Links5ItemInfo
    {
        public Self2ItemInfo self { get; set; }
        public Related3ItemInfo related { get; set; }
    }

     class Self2ItemInfo
    {
        public string href { get; set; }
    }

     class Related3ItemInfo
    {
        public string href { get; set; }
    }

     class Links6ItemInfo
    {
        public Links7ItemInfo links { get; set; }
    }

     class Links7ItemInfo
    {
        public Self3ItemInfo self { get; set; }
    }

     class Self3ItemInfo
    {
        public string href { get; set; }
    }

     class IncludedItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes1ItemInfo attributes { get; set; }
        public Links8ItemInfo links { get; set; }
        public Relationships1ItemInfo relationships { get; set; }
    }

     class Attributes1ItemInfo
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public DateTime createTime { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public DateTime lastModifiedTime { get; set; }
        public string lastModifiedUserId { get; set; }
        public string lastModifiedUserName { get; set; }
        public int versionNumber { get; set; }
        public int storageSize { get; set; }
        public string fileType { get; set; }
        public Extension1ItemInfo extension { get; set; }
    }

     class Extension1ItemInfo
    {
        public string type { get; set; }
        public string version { get; set; }
        public Schema1ItemInfo schema { get; set; }
        public Data4ItemInfo data { get; set; }
    }

     class Schema1ItemInfo
    {
        public string href { get; set; }
    }

     class Data4ItemInfo
    {
        public string processState { get; set; }
        public string extractionState { get; set; }
        public string splittingState { get; set; }
        public string reviewState { get; set; }
        public string revisionDisplayLabel { get; set; }
        public string sourceFileName { get; set; }
    }

     class Links8ItemInfo
    {
        public Self4ItemInfo self { get; set; }
        public Webview1ItemInfo webView { get; set; }
    }

     class Self4ItemInfo
    {
        public string href { get; set; }
    }

     class Webview1ItemInfo
    {
        public string href { get; set; }
    }

     class Relationships1ItemInfo
    {
        public ItemItemInfo item { get; set; }
        public Links10ItemInfo links { get; set; }
        public Refs1ItemInfo refs { get; set; }
        public DownloadformatsItemInfo downloadFormats { get; set; }
        public DerivativesItemInfo derivatives { get; set; }
        public ThumbnailsItemInfo thumbnails { get; set; }
        public StorageItemInfo storage { get; set; }
    }

     class ItemItemInfo
    {
        public Data5ItemInfo data { get; set; }
        public Links9ItemInfo links { get; set; }
    }

     class Data5ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Links9ItemInfo
    {
        public Related4ItemInfo related { get; set; }
    }

     class Related4ItemInfo
    {
        public string href { get; set; }
    }

     class Links10ItemInfo
    {
        public Links11ItemInfo links { get; set; }
    }

     class Links11ItemInfo
    {
        public Self5ItemInfo self { get; set; }
    }

     class Self5ItemInfo
    {
        public string href { get; set; }
    }

     class Refs1ItemInfo
    {
        public Links12ItemInfo links { get; set; }
    }

     class Links12ItemInfo
    {
        public Self6ItemInfo self { get; set; }
        public Related5ItemInfo related { get; set; }
    }

     class Self6ItemInfo
    {
        public string href { get; set; }
    }

     class Related5ItemInfo
    {
        public string href { get; set; }
    }

     class DownloadformatsItemInfo
    {
        public Links13ItemInfo links { get; set; }
    }

     class Links13ItemInfo
    {
        public Related6ItemInfo related { get; set; }
    }

     class Related6ItemInfo
    {
        public string href { get; set; }
    }

     class DerivativesItemInfo
    {
        public Data6ItemInfo data { get; set; }
        public MetaItemInfo meta { get; set; }
    }

     class Data6ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class MetaItemInfo
    {
        public LinkItemInfo link { get; set; }
    }

     class LinkItemInfo
    {
        public string href { get; set; }
    }

     class ThumbnailsItemInfo
    {
        public Data7ItemInfo data { get; set; }
        public Meta1ItemInfo meta { get; set; }
    }

     class Data7ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta1ItemInfo
    {
        public Link1ItemInfo link { get; set; }
    }

     class Link1ItemInfo
    {
        public string href { get; set; }
    }

     class StorageItemInfo
    {
        public Data8ItemInfo data { get; set; }
        public Meta2ItemInfo meta { get; set; }
    }

     class Data8ItemInfo
    {
        public string type { get; set; }
        public string id { get; set; }
    }

     class Meta2ItemInfo
    {
        public Link2ItemInfo link { get; set; }
    }

     class Link2ItemInfo
    {
        public string href { get; set; }
    }

}
