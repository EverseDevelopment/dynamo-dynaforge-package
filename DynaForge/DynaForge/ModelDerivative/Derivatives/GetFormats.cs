using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace ModelDerivative.Derivatives
{
    public class GetFormats
    {
        private GetFormats() { }

        public static List<string[]> Get(string token)
        {
            List<string[]> listformats = new List<string[]>();
            var client = new RestClient("https://developer.api.autodesk.com/modelderivative/v2/designdata/formats");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", token);
            request.AddHeader("Cookie", "PF=2PkYhRg1bGqGmTGB9qcxl5");
            IRestResponse response = client.Execute(request);
            Rootobject deserializedProduct = JsonConvert.DeserializeObject<Rootobject>(response.Content);

            if (deserializedProduct != null)
            {
                listformats.Append(deserializedProduct.formats.dwg);
                listformats.Append(deserializedProduct.formats.fbx);
                listformats.Append(deserializedProduct.formats.ifc);
                listformats.Append(deserializedProduct.formats.iges);
                listformats.Append(deserializedProduct.formats.obj);
                listformats.Append(deserializedProduct.formats.step);
                listformats.Append(deserializedProduct.formats.stl);
                listformats.Append(deserializedProduct.formats.svf);
                listformats.Append(deserializedProduct.formats.svf2);
                listformats.Append(deserializedProduct.formats.thumbnail);
                return listformats;
            }
            else
            {
                return listformats;
            }
        }
    }


    class Rootobject
    {
        public Formats formats { get; set; }
    }

    class Formats
    {
        public string[] dwg { get; set; }
        public string[] fbx { get; set; }
        public string[] ifc { get; set; }
        public string[] iges { get; set; }
        public string[] obj { get; set; }
        public string[] step { get; set; }
        public string[] stl { get; set; }
        public string[] svf { get; set; }
        public string[] svf2 { get; set; }
        public string[] thumbnail { get; set; }
    }

}
