using System.Collections.Generic;
using Ardalis.Specification;
using JetBrains.Annotations;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure
{
    public class RoundScoreRepository : BaseRepository<RoundScore>, IRoundScoreRepository
    {
        public RoundScoreRepository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
}