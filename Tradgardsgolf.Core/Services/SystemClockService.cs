using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Services;

namespace Tradgardsgolf.Core.Services
{
    public class SystemClockService : ISystemClockService
    {
        public DateTime CurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
