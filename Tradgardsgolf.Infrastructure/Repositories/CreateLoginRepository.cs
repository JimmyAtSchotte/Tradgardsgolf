using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Repositories
{
    public class CreateLoginRepository : BaseRepository, ICreateLoginRepository
    {
        public CreateLoginRepository(TradgardsgolfContext db) : base(db)
        {

        }

        public void CreateLogin(ICreateLoginModel createLoginModel)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email)
        {
            return db.Player.Any(x => x.Email == email);
        }
    }
}
