using JetBrains.Annotations;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure
{
    public class RoundRepository : BaseRepository<Round>, IRoundRepository
    {
        public RoundRepository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
}