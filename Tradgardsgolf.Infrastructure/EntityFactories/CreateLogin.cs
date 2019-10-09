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
    public class CreateLoginFactory : BaseEntityFactory<Player, ICreateLoginModel>
    {
        private readonly ICryptoService _cryptoService;


        public CreateLoginFactory(ICryptoService cryptoService, ISystemClockService systemClockService) : base(systemClockService)
        {
            _cryptoService = cryptoService;
        }

        protected override Player TemplateCreate(ICreateLoginModel createLogin)
        {
            var player = new Player();
            player.SetEmail(createLogin.Email);
            player.SetPassword(createLogin.Password, _cryptoService);

            return player;
        }
    }
}
