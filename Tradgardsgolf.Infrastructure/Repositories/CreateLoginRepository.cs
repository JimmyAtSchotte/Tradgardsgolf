using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Models;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.Interfaces;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Repositories
{
    public class CreateLoginRepository : BaseRepository, ICreateLoginRepository
    {
        private readonly IEntityFactory<Player, ICreateLoginModel> _createLoginFactory;

        public CreateLoginRepository(TradgardsgolfContext db, IEntityFactory<Player, ICreateLoginModel> createLoginFactory) : base(db)
        {
            _createLoginFactory = createLoginFactory;
        }

        public void CreateLogin(ICreateLoginModel createLoginModel)
        {
            var entity = _createLoginFactory.Create(createLoginModel);

            db.Add(entity);
            db.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return db.Player.Any(x => x.Email == email);
        }
    }
}
