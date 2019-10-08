using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Infrastructure.Interfaces
{
    public interface IEntityFactoryStrategy<T> where T : IEntity
    {
        IEntityFactoryProvider<T> Create<TArg1>();
    }
}
