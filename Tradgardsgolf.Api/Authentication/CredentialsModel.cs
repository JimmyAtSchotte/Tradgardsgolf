using System.ComponentModel.DataAnnotations;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Api.Authentication
{
    public partial class AuthenticationController
    {
        public class CredentialsModel : ICredentialsModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}