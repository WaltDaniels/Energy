using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GridBank
{
    public class GridBankApiAdapter
    {
        private string GridBankApiUrl;

        public GridBankApiAdapter()
        {
            GridBankApiUrl = ConfigurationManager.AppSettings["GridBankApiUrl"];
        }

        public decimal GetCurrentPower(int siteId)
        {
            decimal retPower = 0;

            var uri = string.Format(
                    "{0}/api/usage/getcurrentpower?siteid={1}",
                    GridBankApiUrl,
                    siteId);

            string retString = GetRequest(uri).Result;
            decimal.TryParse(retString, out retPower);

            return retPower;
        }

        public decimal Drain(int siteId)
        {
            decimal retPower = 0;

            var uri = string.Format(
                "{0}/api/usage/drain",
                GridBankApiUrl);

            var content = new StringContent("siteid=" + siteId);

            string retString = PostRequest(uri, content).Result;
            decimal.TryParse(retString, out retPower);

            return retPower;
        }

        public decimal Charge(int siteId)
        {
            decimal retPower = 0;

            var uri = string.Format(
                "{0}/api/usage/charge",
                GridBankApiUrl);

            var content = new StringContent("siteid=" + siteId);

            string retString = PostRequest(uri, content).Result;
            decimal.TryParse(retString, out retPower);

            return retPower;
        }

        private async Task<string> PostRequest(string url, StringContent content)
        {
            string retString = null;

            //Make the WebAPI call to collect data
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                retString = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Bad response from GridBankApi: " +
                                    (response != null ? response.ReasonPhrase : "<unknown>"));
            }
            return retString;
        }

        private async Task<string> GetRequest(string url)
        {
            string retString = null;

            //Make the WebAPI call to collect data
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                retString = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Bad response from GridBankApi: " +
                                    (response != null ? response.ReasonPhrase : "<unknown>"));
            }
            return retString;
        }

    }
}
