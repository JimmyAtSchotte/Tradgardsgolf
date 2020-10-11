using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Infrastructure.Scorecard;
using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure.Scorecard
{
    public class ScorecardRepository : BaseRepository, IScorecardRepository
    {
        public ScorecardRepository(TradgardsgolfContext db) : base(db)
        {
        }

        public void Add(IScorecardDto dto)
        {
            var course = db.Course.Find(dto.CourseId);

            var round = course.CreateRound();
            db.Round.Add(round);
            db.SaveChanges();

            foreach (var playerScore in dto.PlayerScores)
            {
                var player = db.Player.Find(playerScore.PlayerId);
                var hole = 1;

                foreach (var score in playerScore.Scores)
                {
                    db.RoundScore.Add(round.CreateRoundScore(player, hole, score));
                    hole++;
                }                
            }

            db.SaveChanges();

        }
    }
}
