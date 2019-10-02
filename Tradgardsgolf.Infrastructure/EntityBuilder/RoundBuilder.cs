using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.EntityBuilder
{
    public class RoundBuilder : BaseEntityBuilder<Round>
    {
        internal RoundBuilder(Round entity) : base(entity)
        {

        }

        public RoundBuilder Course(Course course)
        {
            _entity.CourseId = course.Id;
            return this;
        }

        public RoundBuilder AddRoundScores(Player player, params int[] scores)
        {
            var hole = 1;

            foreach (var score in scores)
            {
                _entity.RoundScores.Add(RoundScore.Create(options =>
                {
                    options.Player(player)
                    .Round(_entity)
                    .Hole(hole)
                    .Score(score);
                }));

                hole += 1;
            }

            return this;
        }
    }
}
