﻿
namespace YesPojiQuota.Core.Models
{
    public class Quota
    {
        public int QuotaId { get; set; }
        public decimal Available { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
