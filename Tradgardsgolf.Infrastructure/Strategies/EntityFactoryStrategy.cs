using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Infrastructure.Strategies
{
    public class EntityFactoryStrategy<T> : IEntityFactoryStrategy<T> where T : IEntity
    {
        private readonly IEntityFactoryFactory<T>[] _factories;

        public EntityFactoryStrategy(IEntityFactoryFactory<T>[] factories)
        {
            _factories = factories;
        }

        public IEntityFactoryProvider<T> Create<TArg1>()
        {
            var factory = _factories.FirstOrDefault(x => x.AppliesTo<TArg1>());

            return factory?.Create();
        }
    }
}
