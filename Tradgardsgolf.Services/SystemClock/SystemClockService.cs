using System;
using Tradgardsgolf.Core.Services.SystemClock;

namespace Tradgardsgolf.SystemClock
{
    public class SystemClockService : ISystemClockService
    {
        public DateTime CurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
