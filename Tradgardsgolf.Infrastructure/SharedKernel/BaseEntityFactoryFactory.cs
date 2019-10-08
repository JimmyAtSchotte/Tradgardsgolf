using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public abstract class BaseEntityFactoryFactory<T> : IEntityFactoryFactory<T> where T : IEntity
    {
        public virtual bool AppliesTo<TArg1>()
        {
            return false;
        }

        public abstract IEntityFactoryProvider<T> Create();
        
    }
}
