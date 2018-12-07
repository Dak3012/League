using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LOLBR.classes
{
    public class DataService
    {
        public static async Task<JObject> GetDataFromServerAsync(string QueryStryng)
        {
            var objects = new object();
            using (var Client = new HttpClient())
            {
                using (var response = await Client.GetAsync(QueryStryng))
                {
                    if (response != null)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        var obj = JsonConvert.DeserializeObject<JObject>(json);
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
