﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesPojiQuota.Core.Helpers.Exceptions
{
    public class YesNotConnectedException : Exception
    {
        public YesNotConnectedException(string message) : base(message)
        {
        }
    }
}
