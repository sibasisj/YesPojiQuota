﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesPojiQuota.CoreLibs.Interfaces;
using YesPojiQuota.CoreLibs.Models;

namespace YesPojiQuota.CoreLibs.Services
{
    public class QuotaServiceMock : IQuotaService
    {
        private double maxQuota = 20480.00;
        public Task<double> GetQuota(string a)
        {
            Random b = new Random();

            return Task.Run(() => b.NextDouble() * maxQuota);
        }

        public double GetMaxQuota(string username)
        {
            return maxQuota;
        }

        public Task<double> GetQuota(Account a)
        {
            Random b = new Random();

            return Task.Run(() => b.NextDouble() * maxQuota);
        }
    }
}
