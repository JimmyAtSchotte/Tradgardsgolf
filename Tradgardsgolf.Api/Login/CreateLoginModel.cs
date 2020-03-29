using System.ComponentModel.DataAnnotations;
using Tradgardsgolf.Core.Services.CreateLogin;

namespace Tradgardsgolf.Api.Login
{
    public class CreateLoginModel : ICreateLoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}