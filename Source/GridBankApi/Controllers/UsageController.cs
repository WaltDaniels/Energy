﻿using System;
using System.Web.Http;
using GridBankCommon.Models;
using GridBankService;

namespace GridBankApi.Controllers
{
    public class UsageController : ApiController
    {
        public int Get()
        {
            return 27;
        }

        [Route("api/usage/drain")]
        [AcceptVerbs("POST")]
        [HttpPost]
        public decimal Drain(DrainModel body)
        {
            var service = new PowerUsage();
            var currentPower = service.Drain(body.SiteId, body.Amount);

            PushUpdate.BroadcastPowerLevel(currentPower);
            return currentPower;
        }

        [Route("api/usage/charge")]
        [AcceptVerbs("POST")]
        [HttpPost]
        public decimal Charge(ChargeModel body)
        {
            var service = new PowerUsage();
            var currentPower = service.Charge(body.SiteId, body.Amount);

            PushUpdate.BroadcastPowerLevel(currentPower);
            return currentPower;
        }

        [Route("api/usage/getcurrentpower")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public decimal GetCurrentPower(int siteId)
        {
            var service = new PowerUsage();
            var currentPower = service.GetCurrentPower(siteId);

            PushUpdate.BroadcastPowerLevel(currentPower);
            return currentPower;
        }

        [Route("api/usage/getupates")]
        [AcceptVerbs("GET")]
        [HttpGet]
        public Details GetUpdates(int siteId, DateTime detailsStartingDateTime)
        {
            var service = new PowerUsage();
            var details = new Details
            {
                SiteId = siteId,
                UsageEntries = service.GetEntries(siteId, detailsStartingDateTime)
            };

            return details;
        }
    }
}