using Tradgardsgolf.Core.BusinessRules.Round;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.BusinessRules.Round
{
    [TestFixture]
    public class AddScore
    {
        private Course _course1;
        
        private Player _player1;
        private Player _player2;


        [SetUp]
        public void Setup()
        {
            Player.Create(p => p.Id = 10);
            _course1 = Course.Create(Guid.NewGuid());

            _player1 = Player.Create(p => p.Id = 1);
            _player2= Player.Create(p => p.Id = 2);
        }

        [Test]
        public void AddOneScore()
        {
            var round = _course1.CreateRound();
            round.AddScore(_player1, 5);

            var score = round.RoundScores.FirstOrDefault();

            Assert.Multiple(() => {
                
                Assert.That(1, Is.EqualTo(score.Hole));
                Assert.That(_player1.Id, Is.EqualTo(score.PlayerId));
                Assert.That(_player1, Is.EqualTo(score.Player));
                Assert.That(5, Is.EqualTo(score.Score));
            });
        }
        
        [Test]
        public void AddMultipleScoreScoresOnePlayer()
        {
            var round = _course1.CreateRound();
            round.AddScore(_player1, 5);
            round.AddScore(_player1, 6);
            round.AddScore(_player1, 7);

            var scores = round.RoundScores.ToArray();

            Assert.Multiple(() => 
            {
                Assert.That(scores.All(s => s.PlayerId == _player1.Id && 
                                            s.Player == _player1), Is.True);
                
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
            var round = _course1.CreateRound();
            round.AddScore(_player1, 5);
            round.AddScore(_player2, 3);

            var scores = round.RoundScores;

            Assert.Multiple(() => {
                
                Assert.That(1, Is.EqualTo(scores.Count(x => x.PlayerId == _player1.Id &&
                                                 x.Hole == 1 &&
                                                 x.Score == 5)));
                
                Assert.That(1, Is.EqualTo(scores.Count(x => x.PlayerId == _player2.Id &&
                                                            x.Hole == 1 &&
                                                            x.Score == 3)));
                
                Assert.That(2, Is.EqualTo(scores.Count(x => x.Hole == 1)));
           });
        }
    }
}