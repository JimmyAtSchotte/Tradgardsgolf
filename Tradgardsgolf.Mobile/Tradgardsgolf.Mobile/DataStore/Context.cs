using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tradgardsgolf.Mobile.DataStore
{
    public delegate Task AuthenticationSucceded();
    public delegate Task Unauthorized();

    public interface IContext
    {
        event AuthenticationSucceded AuthenticationSucceded;
        event Unauthorized Unauthorized;

        Authentication Authentication { get; }

        Task OnAuthenticationSucceded();
        Task OnUnauthorized();
    }

    public class Context : IContext
    {
        public event AuthenticationSucceded AuthenticationSucceded;
        public event Unauthorized Unauthorized;

        public Authentication Authentication { get; }        

        public Context(IApiRepository apiRepository)
        {
            Authentication = new Authentication(this, apiRepository);
        }

        public async Task OnAuthenticationSucceded()
        {
            await AuthenticationSucceded?.Invoke();
        }

        public async Task OnUnauthorized()
        {
            await Unauthorized?.Invoke();
        }
    }


    public class Authentication
    {
        private readonly IContext _context;
        private readonly IApiRepository _apiRepository;
        
        public Authentication(IContext context, IApiRepository apiRepository)
        {
            _context = context;
            _apiRepository = apiRepository;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var credentials = new ApiClient.Models.CredentialsModel(email, password);

            if (await _apiRepository.AuthenticateAsync(credentials))
            {
                await _context.OnAuthenticationSucceded();
                return true;
            }

            return false;
        }

        public async Task<bool> IsAuthorizedAsync()
        {
            var isAuthroized = await _apiRepository.IsAuthorizedAsync();
            
            if(!isAuthroized)
                await _context.OnUnauthorized();            

            return isAuthroized;            
        }
    } 
}
