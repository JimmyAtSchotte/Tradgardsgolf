using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Infrastructure.Interfaces
{
    public interface IEntityFactory<T, TArg1> where T : IEntity
    {
        T Create(TArg1 arg1);
    }
}
