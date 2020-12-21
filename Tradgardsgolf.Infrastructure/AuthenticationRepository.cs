using System;
using System.Linq;
using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Infrastructure
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(TradgardsgolfContext db) : base(db)
        {

        }

        public IAuthenticateDtoResult CredentialsAuthentication(ICredentialsDto dto)
        {
            var player = db.Player.FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password.Value);

            if(player == null)
                return new AuthenticationFailedDtoResult();

            player.Key = Guid.NewGuid().ToString();
            db.SaveChanges();

            return new AuthenticationSucceedDtoResult()
            {
                Id = player.Id,
                Email = player.Email,
                Name = player.Name,
                Key = player.Key
            };
        }

        public IAuthenticateDtoResult KeyAuthentication(IKeyAuthenticationDto dto)
        {
            var player = db.Player.FirstOrDefault(x => x.Id == dto.Id && x.Key == dto.Key);

            if (player == null)
                return new AuthenticationFailedDtoResult();
            
            return new AuthenticationSucceedDtoResult()
            {
                Id = player.Id,
                Email = player.Email,
                Name = player.Name,
                Key = player.Key
            };
        }
    }
}
