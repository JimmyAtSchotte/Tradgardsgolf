using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
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

        public Player()
        {
            Created = DateTime.Now;
        }
    }
}
