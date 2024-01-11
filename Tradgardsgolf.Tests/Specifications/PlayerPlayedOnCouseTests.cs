using System.Linq;
using System.Threading.Tasks;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.EntityFrameworkCore;
using NUnit.Framework;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Infrastructure.Database;
using SUT = Tradgardsgolf.Infrastructure.Database.Repository<Tradgardsgolf.Core.Entities.Player>;

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
            
            var arrange = Arrange.Dependencies<IRepository<Player>, Repository<Player>>(
                dependencies => {
                   
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var createdBy);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out player1);
                    dependencies.UseEntity<Course, TradgardsgolfContext>(Course.Create(""), out course);
                    dependencies.UseEntity<Round, TradgardsgolfContext>(course.CreateRound(), out var round);
                    dependencies.UseEntity<RoundScore, TradgardsgolfContext>(round.CreateRoundScore(player1, 1, 3));
                });
            
            var repository = arrange.Resolve<IRepository<Player>>();
            var result = await repository.ListAsync(new HasPlayedOnCourse(course.Id));
            
            Assert.That(1, Is.EqualTo(result.Count(x => x.Id == player1.Id)));
        }
        
        [Test]
        public async Task ShouldNotReturnPlayersThatHaveNotPlayedOnCourse()
        {
            Course course = null;
            Player player1 = null;
            
            var arrange = Arrange.Dependencies<SUT, SUT>(
                dependencies => {
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var createdBy);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out player1);
                    dependencies.UseEntity<Player, TradgardsgolfContext>(Player.Create(), out var player2);
                    dependencies.UseEntity<Course, TradgardsgolfContext>(Course.Create(""), out course);
                    dependencies.UseEntity<Round, TradgardsgolfContext>(course.CreateRound(), out var round);
                    dependencies.UseEntity<RoundScore, TradgardsgolfContext>(round.CreateRoundScore(player2, 1, 3));
                });
            
            var repository = arrange.Resolve<SUT>();
            var result = await repository.ListAsync(new HasPlayedOnCourse(course.Id));
            
            Assert.That(0, Is.EqualTo(result.Count(x => x.Id == player1.Id)));
        }
    }
    
    
}