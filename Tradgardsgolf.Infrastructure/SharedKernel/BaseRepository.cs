using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Infrastructure.SharedKernel
{
    public abstract class BaseRepository
    {
        protected readonly TradgardsgolfContext db;

        public BaseRepository(TradgardsgolfContext db)
        {
            this.db = db;
        }

    }
}
