using System;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient.Authentication;

namespace Tradgardsgolf.ApiClient.CreateLogin
{
    public static class CreateLoginExtension
    {
        public static async Task<IResponse> CreateLoginAsync(this TradgradsgolfApiClient client, CreateLoginModel model)
        {
            try
            {
                model.Validate();

                var response = await client.PostAsync("Login/Create", model);

                return await client.Response(response);
            }
            catch (Exception ex)
            {
                return client.Response(ex);
            }
        }      
    }
}
