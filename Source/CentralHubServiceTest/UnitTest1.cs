﻿using CentralHubService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CentralHubServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var service = new CollectUsage();
            //var response = service.GetRemoteData(1);
            ServiceGetdata(1);
        }

        private async void ServiceGetdata(int SiteId)
        {
            var service = new CollectUsage();
            //var response = await 
            service.GetRemoteData(1);
        }
    }
}