using System;
using System.Collections.Generic;
using System.Linq;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(TradgardsgolfContext db) : base(db)
        {
        }
    }
}
