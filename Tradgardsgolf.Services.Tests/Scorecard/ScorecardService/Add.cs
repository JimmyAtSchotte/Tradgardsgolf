using ArrangeDependencies.Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Services.Scorecard;

namespace Tradgardsgolf.Tests.Scorecard.ScorecardService
{
    [TestFixture]
    public class Add
    {

        [Test]
        public void Nothing()
        {
            var arrange = Arrange.Dependencies<IScorecardService, Tradgardsgolf.Services.Scorecard.ScorecardService >();

            var scorecardService = arrange.Resolve<IScorecardService>();

            scorecardService.Add(new ScorecardModelStub());
        }
    }

    public class ScorecardModelStub : IScorecardModel
    {
        public int CourseId { get; }
        public IEnumerable<IPlayerScoreModel> PlayerScores { get; }

        public ScorecardModelStub(int courseId = default, params IPlayerScoreModel[] playerScores)
        {
            CourseId = courseId;
            PlayerScores = playerScores;
        }
    }

    public class PlayerScoreModel : IPlayerScoreModel
    {
        public string Name { get; }
        public int[] Scores { get; }

        public PlayerScoreModel(string name = null, params int[] scores)
        {
            Name = name;
            Scores = scores;
        }
    }
}
