using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Infrastructure.Interfaces
{
    public interface IEntityFactoryFactory<T> where T : IEntity
    {
        bool AppliesTo<TArg1>();

        IEntityFactoryProvider<T> Create();
        
    }
}
