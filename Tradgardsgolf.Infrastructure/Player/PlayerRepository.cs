using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradgardsgolf.Core.Infrastructure.Player;
using Tradgardsgolf.Infrastructure.Context;

namespace Tradgardsgolf.Infrastructure.Player
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(TradgardsgolfContext db) : base(db)
        {
        }

        public IPlayerDtoResult Add(IAddPlayerDto dto)
        {
            var player = Context.Player.Create(x =>  x.Name = dto.Name);

            db.Player.Add(player);
            db.SaveChanges();

            return new PlayerDtoResult()
            {
                Name = player.Name,
                Id = player.Id
            };
        }

        public bool CheackIfMailExists(string email)
        {
            throw new NotImplementedException();
        }

        public IPlayerDtoResult Find(IFindPlayerPlayedOnCourseDto dto)
        {
            return (from roundscore in db.RoundScore
                    join round in db.Round on roundscore.RoundId equals round.Id
                    join player in db.Player on roundscore.PlayerId equals player.Id
                    where round.CourseId == dto.CourseId &&
                          player.Name == dto.Name
                    select new PlayerDtoResult()
                    {
                        Name = player.Name,
                        Id = player.Id
                    }).FirstOrDefault();
        }

    
        public IPlayerDtoResult GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlayerDtoResult> GetPlayersThatHasPlayedOnCourse(int courseId)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerDtoResult : IPlayerDtoResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
