using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GridBank
{
    public class GridBankApiAdapter
    {
        private readonly string GridBankApiUrl;

        public GridBankApiAdapter()
        {
            GridBankApiUrl = ConfigurationManager.AppSettings["GridBankApiUrl"];
        }

        public double GetCurrentPower(int siteId)
        {
            double retPower = 0;

            var uri = string.Format(
                "{0}/api/usage/getcurrentpower?siteid={1}",
                GridBankApiUrl,
                siteId);

            var retString = GetRequest(uri).Result;
            double.TryParse(retString, out retPower);

            return retPower;
        }

        public double Drain(int siteId, decimal amount)
        {
            double retPower = 0;

            var uri = string.Format(
                "{0}/api/usage/drain",
                GridBankApiUrl);

            var content = new StringContent(string.Format("siteid={0}&amount={1}", siteId, amount));

            var retString = PostRequest(uri, content).Result;
            double.TryParse(retString, out retPower);

            return retPower;
        }

        public double Charge(int siteId, decimal amount)
        {
            double retPower = 0;

            var uri = string.Format(
                "{0}/api/usage/charge",
                GridBankApiUrl);

            var content = new StringContent(string.Format("siteid={0}&amount={1}", siteId, amount));

            var retString = PostRequest(uri, content).Result;
            double.TryParse(retString, out retPower);

            return retPower;
        }

        private async Task<string> PostRequest(string url, StringContent content)
        {
            string retString = null;

            //Set the media type
            content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

            //Make the WebAPI call to collect data
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
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
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            var response = await httpClient.GetAsync(url).ConfigureAwait(false);
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