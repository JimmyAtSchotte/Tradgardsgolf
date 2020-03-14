using System.Linq;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure.CreateLogin
{
    public class CreateLoginRepository : BaseRepository, ICreateLoginRepository
    {
        public CreateLoginRepository(TradgardsgolfContext db) : base(db)
        {
        }

        public void CreateLogin(ICreateLoginDto createLoginModel)
        {
            var player = new Player();

            player.Email = createLoginModel.Email;
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
