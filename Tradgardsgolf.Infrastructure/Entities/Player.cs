using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tradgardsgolf.Core.Interfaces;
using Tradgardsgolf.Core.Interfaces.Services;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Entities
{
    public class Player : BaseEntity<Player>
    {
        [Key]
        public int Id { get; private set; }
        [Column("strEmail")]
        public string Email { get; private set; }
        [Column("strPassword")]
        public string Password { get; private set; }
        [Column("strKey")]
        public string Key { get; private set; }
        [Column("strName")]
        public string Name { get; private set; }
        [Column("dtmCreated")]
        public DateTime Created { get; private set; }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPassword(string password, ICryptoService cryptoService)
        {
            Password = cryptoService.Encrypt(password);
        }

        public void SetKey(string key)
        {
            Key = key;
        }

        public void Setname(string name)
        {
            Name = name;
        }
                
        public override void OnCreate(ISystemClockService systemClockService)
        {
            Created = systemClockService.CurrentDateTime();
        }
    }
}
