using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tradgardsgolf.Infrastructure.EntityBuilder;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Entities
{
    public class Player : BaseEntity<Player>

    {
        [Key]
        public int Id { get; internal set; }
        [Column("strEmail")]
        public string Email { get; internal set; }
        [Column("strPassword")]
        public string Password { get; internal set; }
        [Column("strKey")]
        public string Key { get; private set; }
        [Column("strName")]
        public string Name { get; internal set; }
        [Column("dtmCreated")]
        public DateTime Created { get; internal set; }

        private Player() {
            Created = DateTime.Now;
        }

        internal void SetKey()
        {
            Key = Guid.NewGuid().ToString();
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public static Player Create(Action<PlayerBuilder> options = null)
        {
            var player = new Player();
            player.SetOptions(options);

            return player;
        }

    }
}
