using System;
using System.Reflection;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public class BaseEntity<T> : IEntity where T : class
    {
        public virtual void OnCreate(ISystemClockService systemClockService) { }

        public virtual void OnModified(ISystemClockService systemClockService) { }        
    }
}
