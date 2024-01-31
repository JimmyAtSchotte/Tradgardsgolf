using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("player")]

    public class Player : BaseEntity<Player>
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; private set; }
        
        public ICollection<RoundScore> RoundScores { get; set; }

        private Player()
        {
            
        }

        private Player(DateTime created)
        {
            Created = created;
        }

        public static Player Create(Action<Player> properties = null)
        {
            var player = new Player(DateTime.Now);
            properties?.Invoke(player);
            return player;
        }
    }
}
