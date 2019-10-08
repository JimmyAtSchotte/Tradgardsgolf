using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Infrastructure.Interfaces
{
    public interface IEntityFactoryProvider<T> where T : IEntity
    {
        T Create<TArg1>(TArg1 arg1);
    }
}
