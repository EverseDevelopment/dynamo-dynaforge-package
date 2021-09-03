using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using RestSharp;

namespace BIM360
{
    public class Projects
    {
        private Projects() { }

        [MultiReturn(new[] { "name", "id" })]
        public static Dictionary<string, List<string>> Get(string Token, string client_Id)
        {

            var client = new RestClient("https://developer.api.autodesk.com/hq/v1/accounts/" + client_Id + "/projects");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddHeader("Cookie", "PF=MvmCkfcwqjGEZLLP0CP2Vj");
            IRestResponse response = client.Execute(request);

            var deserializedProduct = JsonConvert.DeserializeObject<Class1[]>(response.Content);

            if (deserializedProduct != null)
            {
                List<string> projectNames = new List<string>();
                List<string> projectIds = new List<string>();

                foreach (Class1 i in deserializedProduct)
                {
                    projectNames.Add(i.name);
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


    class Class1
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public object value { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string job_number { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string city { get; set; }
        public string state_or_province { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public object business_unit_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string project_type { get; set; }
        public string timezone { get; set; }
        public string language { get; set; }
        public string construction_type { get; set; }
        public object contract_type { get; set; }
        public DateTime? last_sign_in { get; set; }
    }




}
