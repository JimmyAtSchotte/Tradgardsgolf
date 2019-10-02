using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.EntityBuilder
{
    public class RoundScoreBuilder : BaseEntityBuilder<RoundScore>
    {
        internal RoundScoreBuilder(RoundScore entity) : base(entity)
        {
        }

        public RoundScoreBuilder Round(Round round)
        {
            _entity.Round = round;
            return this;
        }

        public RoundScoreBuilder Hole(int hole)
        {
            _entity.Hole = hole;
            return this;
        }

        public RoundScoreBuilder Score(int score)
        {
            _entity.Score = score;
            return this;
        }

        public RoundScoreBuilder Player(Player player)
        {
            _entity.PlayerId = player.Id;
            return this;
        }
    }
}
