using System.Linq;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure.Login;

namespace Tradgardsgolf.Infrastructure
{
    public class CreateLoginRepository : BaseRepository, ICreateLoginRepository
    {
        public CreateLoginRepository(TradgardsgolfContext db) : base(db)
        {
        }

        public void CreateLogin(ICreateLoginDto createLoginModel)
        {
            var player = Player.Create();

            player.Email = createLoginModel.Email.Value;
            player.Password = createLoginModel.Password.Value;
            
            db.Add(player);
            db.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return db.Player.Any(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
