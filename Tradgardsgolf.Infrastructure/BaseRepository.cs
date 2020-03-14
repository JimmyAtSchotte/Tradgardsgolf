using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure
{
    public abstract class BaseRepository
    {
        protected readonly TradgardsgolfContext db;

        protected BaseRepository(TradgardsgolfContext db)
        {
            this.db = db;
        }

    }
}
