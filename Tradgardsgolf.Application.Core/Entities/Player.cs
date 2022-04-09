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
        [Column("strEmail")]
        public string Email { get; set; }
        [Column("strPassword")]
        public string Password { get; set; }
        [Column("strKey")]
        public string Key { get; set; }
        [Column("strName")]
        public string Name { get; set; }
        [Column("dtmCreated")]
        public DateTime Created { get; private set; }
        
        public virtual ICollection<RoundScore> RoundScores { get; set; }

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

        public Course CreateCourse(Action<Course> properties = null)
        {
           return Course.Create(this);
        }
    }
}
