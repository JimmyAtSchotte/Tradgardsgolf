using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.ApiClient.Models;
using Xamarin.Essentials;

namespace Tradgardsgolf.Mobile
{
    public interface IApiRepository
    {
        Task<bool> AuthenticateAsync(CredentialsModel credentialsModel);
        Task<bool> IsAuthorizedAsync();
    }

    public class ApiRepository : IApiRepository
    {
        private readonly TradgardsgolfAPI _api;
        private readonly Dictionary<string, List<string>> _authorization;

        private AuthenticationResponse _authentication;
        
        private  bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ApiRepository()
        {
            _api = new TradgardsgolfAPI(new Uri(App.ApiUrl), new FakeServiceClientCredentials());
            _authorization = new Dictionary<string, List<string>>();
        }

        public async Task<bool> AuthenticateAsync(CredentialsModel credentialsModel)
        {
            try
            {
                _authentication = (await _api.AuthenticateWithHttpMessagesAsync(credentialsModel)).Body;

                _authorization.Clear();
                _authorization.Add("Authorization", new List<string> { $"Bearer {_authentication.Token}" });

                return true;
            }
            catch
            {

            }

            return false;
        }

        public async Task<bool> IsAuthorizedAsync()
        {
            try
            {                
                await _api.IsAuthorizedWithHttpMessagesAsync(_authorization);

                return true;
            }
            catch
            {

            }

            return false;
        }


        private class FakeServiceClientCredentials : ServiceClientCredentials {}
    }
}
