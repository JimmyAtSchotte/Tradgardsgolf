using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.Interfaces;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.EntityFactories
{
    public class CreateLoginFactory : BaseEntityFactoryFactory<Player>
    {
        private readonly ICryptoService _cryptoService;
        private readonly ISystemClockService _systemClockService;


        public CreateLoginFactory(ICryptoService cryptoService, ISystemClockService systemClockService)
        {
            _cryptoService = cryptoService;
            _systemClockService = systemClockService;
        }

        public override bool AppliesTo<TArg1>()
        {
            return typeof(TArg1) == typeof(ICreateLoginModel);
        }

        public override IEntityFactoryProvider<Player> Create()
        {
            return new CreateLogin(_cryptoService, _systemClockService);
        }
    }

    public class CreateLogin : BaseEntityFactoryProvider<Player>
    {
        private readonly ICryptoService _cryptoService;


        public CreateLogin(ICryptoService cryptoService, ISystemClockService systemClockService) : base(systemClockService)
        {
            _cryptoService = cryptoService;
        }


        protected override Player TemplateCreate<TArg1>(TArg1 arg1)
        {
            var createLogin = arg1 as ICreateLoginModel;

            var player = new Player();
            player.SetEmail(createLogin.Email);
            player.SetPassword(createLogin.Password, _cryptoService);

            return player;
        }
    }
}
