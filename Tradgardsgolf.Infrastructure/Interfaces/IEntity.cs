using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Services;

namespace Tradgardsgolf.Infrastructure.Interfaces
{
    public interface IEntity
    {
        void OnModified(ISystemClockService systemClockService);

        void OnCreate(ISystemClockService systemClockService);
    }
}
