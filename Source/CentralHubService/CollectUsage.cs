using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CentralHubData;
using CentralHubService.Models;
using Newtonsoft.Json;

namespace CentralHubService
{
    public class CollectUsage
    {
        public async Task<Details> GetRemoteData(int SiteId)
        {
            Details retDetails = null;

            //Lookup Site's URL
            using (var context = new CentralHubDb())
            {
                var site = context.GridBankSites.Find(SiteId);
                if (site == null) throw new Exception("Site not found!");

                var lastUsage = site.Usages.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
                DateTime lastUpdate = lastUsage != null ? lastUsage.TimeStamp : new DateTime();

                var uri = string.Format(
                    "{0}/api/usage/getupates?siteid={1}&detailsStartingDateTime={2}", 
                    site.ApiUrl, 
                    site.Id,
                    WebUtility.UrlEncode(lastUpdate.ToString()));

                //Make the WebAPI call to collect data
                var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    string jsonSting = await response.Content.ReadAsStringAsync();
                    retDetails = JsonConvert.DeserializeObject<Details>(jsonSting);
                }
                else
                {
                    throw new Exception("Bad response from GridBankApi: " +
                                        (response != null ? response.ReasonPhrase : "<unknown>"));
                }
            }

            return retDetails;
        }

        public void SaveData(Details details)
        {
            using (var context = new CentralHubDb())
            {
                var site = context.GridBankSites.Find(details.SiteId);
                if (site == null) throw new Exception("Site not found!");

                foreach (var entry in details.UsageEntries)
                {
                    //See if this Guid already exists
                    var currentRecord = site.Usages.FirstOrDefault(x => x.IdGuid == entry.IdGuid && x.GridBankSiteId == details.SiteId);
                    if (currentRecord != null)
                    {
                        //Yes, it exists ... update it
                        currentRecord.TimeStamp = entry.TimeStamp;
                        currentRecord.CurrentPower = entry.CurrentPower;
                        currentRecord.IsNew = true;
                    }
                    else
                    {
                        //No, this is a new entry ... add it
                        site.Usages.Add(new Usage()
                        {
                            IdGuid = entry.IdGuid,
                            GridBankSiteId = details.SiteId,
                            TimeStamp = entry.TimeStamp,
                            CurrentPower = entry.CurrentPower,
                            IsNew = true
                        });
                    }
                }

                //Save everything
                context.SaveChanges();
            }
        }

        public List<UsageEntry> GetData(int siteId)
        {
            using (var context = new CentralHubDb())
            {
                var site = context.GridBankSites.Find(siteId);
                if (site == null) throw new Exception("Site not found!");

                return site.Usages
                    .OrderByDescending(x => x.TimeStamp)
                    .Select(x => new UsageEntry
                    {
                        IdGuid = x.IdGuid,
                        TimeStamp = x.TimeStamp,
                        CurrentPower = x.CurrentPower
                    })
                    .ToList();
            }
        }
    }
}
