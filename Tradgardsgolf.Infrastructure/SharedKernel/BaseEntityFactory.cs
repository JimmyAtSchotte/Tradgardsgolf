using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Infrastructure.Interfaces;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public abstract class BaseEntityFactory<T, TArg1> : IEntityFactory<T, TArg1> where T : IEntity
    {
        private readonly ISystemClockService _systemClockService;
        public BaseEntityFactory(ISystemClockService systemClockService)
        {
            _systemClockService = systemClockService;
        }

        public T Create(TArg1 arg1)
        {
            var entity = TemplateCreate(arg1);
            entity.OnCreate(_systemClockService);
            return entity;
        }

        protected abstract T TemplateCreate(TArg1 arg1);      

    }
}
