using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public abstract class BaseEntityFactoryProvider<T> : IEntityFactoryProvider<T> where T : IEntity
    {
        private readonly ISystemClockService _systemClockService;
        public BaseEntityFactoryProvider(ISystemClockService systemClockService)
        {
            _systemClockService = systemClockService;
        }

        public T Create<TArg1>(TArg1 arg1)
        {
            var entity = TemplateCreate(arg1);
            entity.OnCreate(_systemClockService);
            return entity;
        }


        protected virtual T TemplateCreate<TArg1>(TArg1 arg1)
        {
            throw new NotImplementedException();
        }
    }
}
