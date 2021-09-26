using System.Linq;
using System.Threading.Tasks;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.EntityFrameworkCore;
using NUnit.Framework;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Infrastructure.Tests.Specifications
{
    [TestFixture]
    public class PlayerPlayedOnCouseTests
    {
        [Test]
        public async Task ShouldReturnPlayersPlayedOnCourse()
        {
            Course course = null;
            Player player1 = null;
            
            var arrange = Arrange.Dependencies<IPlayerRepository, PlayerRepository>(
                dependencies => {
                   
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var createdBy);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out player1);
                    dependencies.UseEntity<Course, TradgardsgolfContext>(createdBy.CreateCourse(), out course);
                    dependencies.UseEntity<Round, TradgardsgolfContext>(course.CreateRound(), out var round);
                    dependencies.UseEntity<RoundScore, TradgardsgolfContext>(round.CreateRoundScore(player1, 1, 3));
                });
            
            var repository = arrange.Resolve<IPlayerRepository>();
            var result = await repository.ListAsync(new HasPlayedOnCourse(course.Id));
            
            Assert.AreEqual(1, result.Count(x => x.Id == player1.Id));
        }
        
        [Test]
        public async Task ShouldNotReturnPlayersThatHaveNotPlayedOnCourse()
        {
            Course course = null;
            Player player1 = null;
            
            var arrange = Arrange.Dependencies<IPlayerRepository, PlayerRepository>(
                dependencies => {
                   
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var createdBy);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out player1);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var player2);
                    dependencies.UseEntity<Course, TradgardsgolfContext>(createdBy.CreateCourse(), out course);
                    dependencies.UseEntity<Round, TradgardsgolfContext>(course.CreateRound(), out var round);
                    dependencies.UseEntity<RoundScore, TradgardsgolfContext>(round.CreateRoundScore(player2, 1, 3));
                });
            
            var repository = arrange.Resolve<IPlayerRepository>();
            var result = await repository.ListAsync(new HasPlayedOnCourse(course.Id));
            
            Assert.AreEqual(0, result.Count(x => x.Id == player1.Id));
        }
    }
    
    
}