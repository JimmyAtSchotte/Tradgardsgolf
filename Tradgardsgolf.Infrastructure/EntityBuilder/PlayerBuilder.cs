using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.EntityBuilder
{
    public class PlayerBuilder : BaseEntityBuilder<Player>
    {
        internal PlayerBuilder(Player entity) : base(entity)
        {

        }

        public PlayerBuilder Name(string name)
        {
            _entity.Name = name;
            return this;
        }

        public PlayerBuilder Email(string name)
        {
            _entity.Email = name;
            return this;
        }

        public PlayerBuilder Password(string name)
        {
            _entity.Password = name;
            return this;
        }   
    }
}
