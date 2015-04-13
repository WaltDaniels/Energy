using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CentralHubData;
using CentralHubService.Models;
using Newtonsoft.Json.Linq;

namespace CentralHubService
{
    public class CollectUsage
    {
        public async void GetData(int SiteId)
        {
            //Lookup Site's URL
            using (var context = new CentralHubDb())
            {
                var site = context.GridBankSites.Find(SiteId);
                if (site == null) throw new Exception("Site not found!");

                var lastUsage = site.Usages.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
                DateTime lastUpdate = lastUsage != null ? lastUsage.TimeStamp : new DateTime();

                var uri = string.Format(
                    "{0}?siteid={1}&detailsStartingDateTime={2}", 
                    site.ApiUrl, 
                    site.Id,
                    WebUtility.UrlEncode(lastUpdate.ToString()));

                var httpClient = new HttpClient();

                //string content = HttpGet(uri).Wait();
                var content = await RestCall(uri);

                //JObject content = httpClient.GetAsync(uri).Result;


                //return await Task.Run(() => UsageModel.Details.Parse(content));

            }


            //Hit Site's WebAPI

            //Return Data
        }

        private async Task<string> RestCall(string uri)
        {
            string payload = "";

            using (var httpClient = new HttpClient())
            {
                //httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    payload = await response.Content.ReadAsStringAsync();
                    //Product product = await response.Content.ReadAsAsync<Product>();
                    //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                }
            }

            return payload;
        }
    }
}
