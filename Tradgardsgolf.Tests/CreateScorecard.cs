﻿using System.Linq;
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
            course1 = createdBy.CreateCourse();

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
                
                Assert.AreEqual(1, score.Hole);
                Assert.AreEqual(player1.Id, score.PlayerId);
                Assert.AreEqual(player1, score.Player);
                Assert.AreEqual(5, score.Score);
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
                Assert.IsTrue(scores.All(s => s.PlayerId == player1.Id && 
                                              s.Player == player1));
                
                Assert.AreEqual(1, scores[0].Hole);
                Assert.AreEqual(5, scores[0].Score);
                Assert.AreEqual(2, scores[1].Hole);
                Assert.AreEqual(6, scores[1].Score);
                Assert.AreEqual(3, scores[2].Hole);
                Assert.AreEqual(7, scores[2].Score);
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
                
                Assert.AreEqual(1, scores.Count(x => x.PlayerId == player1.Id &&
                                                     x.Hole == 1 &&
                                                     x.Score == 5));
                
                Assert.AreEqual(1, scores.Count(x => x.PlayerId == player2.Id &&
                                                     x.Hole == 1 &&
                                                     x.Score == 3));
                
                Assert.AreEqual(2, scores.Count(x => x.Hole == 1));
           });
        }
    }
}