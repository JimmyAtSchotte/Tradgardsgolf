using System.Linq;
using NUnit.Framework;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Infrastructure.Tests
{
    [TestFixture]
    public class CreateScorecard
    {
        private Course course1;
        
        private Player player1;
        private Player player2;
        private Player createdBy;
        

        [SetUp]
        public void Setup()
        {
            createdBy = Player.Create(p => p.Id = 10);
            course1 = Course.Create("");

            player1 = Player.Create(p => p.Id = 1);
            player2= Player.Create(p => p.Id = 2);
        }

        [Test]
        public void AddOneScore()
        {
            var round = course1.CreateRound();
            round.AddScore(player1, 5);

            var score = round.RoundScores.FirstOrDefault();

            Assert.Multiple(() => {
                
                Assert.That(1, Is.EqualTo(score.Hole));
                Assert.That(player1.Id, Is.EqualTo(score.PlayerId));
                Assert.That(player1, Is.EqualTo(score.Player));
                Assert.That(5, Is.EqualTo(score.Score));
            });
        }
        
        [Test]
        public void AddMultipleScoreScoresOnePlayer()
        {
            var round = course1.CreateRound();
            round.AddScore(player1, 5);
            round.AddScore(player1, 6);
            round.AddScore(player1, 7);

            var scores = round.RoundScores.ToArray();

            Assert.Multiple(() => 
            {
                Assert.That(scores.All(s => s.PlayerId == player1.Id && 
                                            s.Player == player1), Is.True);
                
                Assert.That(1, Is.EqualTo(scores[0].Hole));
                Assert.That(5, Is.EqualTo(scores[0].Score));
                Assert.That(2, Is.EqualTo(scores[1].Hole));
                Assert.That(6, Is.EqualTo(scores[1].Score));
                Assert.That(3, Is.EqualTo(scores[2].Hole));
                Assert.That(7, Is.EqualTo(scores[2].Score));
            });
        }
        
        [Test]
        public void AddMultiplePlayers()
        {
            var round = course1.CreateRound();
            round.AddScore(player1, 5);
            round.AddScore(player2, 3);

            var scores = round.RoundScores;

            Assert.Multiple(() => {
                
                Assert.That(1, Is.EqualTo(scores.Count(x => x.PlayerId == player1.Id &&
                                                 x.Hole == 1 &&
                                                 x.Score == 5)));
                
                Assert.That(1, Is.EqualTo(scores.Count(x => x.PlayerId == player2.Id &&
                                                            x.Hole == 1 &&
                                                            x.Score == 3)));
                
                Assert.That(2, Is.EqualTo(scores.Count(x => x.Hole == 1)));
           });
        }
    }
}