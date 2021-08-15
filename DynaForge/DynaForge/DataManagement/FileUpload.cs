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
    public class FileUpload
    {
        private FileUpload() { }


        /// <returns></returns>
        [MultiReturn(new[] { "BucketKey", "ObjectId", "objectKey", "Location" })]
        public static Dictionary<string, string> Run(string Token, string bucket, string fileURN, string filePath)
        {
            var client = new RestClient("https://developer.api.autodesk.com/oss/v2/buckets/" + bucket + "/objects/" + fileURN);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Content-Type", "text/plain; charset=UTF-8");

            byte[] filecontent;
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                filecontent = new BinaryReader(fs).ReadBytes((int)fs.Length);
            }

            request.AddParameter("text/plain; charset=UTF-8", filecontent, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RootobjectFileUpload deserializedProduct = JsonConvert.DeserializeObject<RootobjectFileUpload>(response.Content);


            if (deserializedProduct != null)
            {
                return new Dictionary<string, string> {
                { "BucketKey", deserializedProduct.bucketKey},
                { "ObjectId", deserializedProduct.objectId},
                { "objectKey", deserializedProduct.objectKey},
                { "Location", deserializedProduct.location}
                };
            }
            else
            {
                return null;
            }

        }
    }


    class RootobjectFileUpload
    {
        public string bucketKey { get; set; }
        public string objectId { get; set; }
        public string objectKey { get; set; }
        public string sha1 { get; set; }
        public int size { get; set; }
        public string contentType { get; set; }
        public string location { get; set; }
    }

}
