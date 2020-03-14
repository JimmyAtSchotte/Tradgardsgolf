using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.Interfaces;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Repositories
{
    public class CreateLoginRepository : BaseRepository, ICreateLoginRepository
    {
        private readonly IEntityFactory<Player, ICreateLoginDto> _createLoginFactory;

        public CreateLoginRepository(TradgardsgolfContext db, IEntityFactory<Player, ICreateLoginDto> createLoginFactory) : base(db)
        {
            _createLoginFactory = createLoginFactory;
        }

        public void CreateLogin(ICreateLoginDto createLoginModel)
        {
            var entity = _createLoginFactory.Create(createLoginModel);

            db.Add(entity);
            db.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return db.Player.Any(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
