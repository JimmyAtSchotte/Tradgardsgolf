using System;
using System.Threading.Tasks;

namespace Tradgardsgolf.ApiClient.Authentication
{
    public static class AuthenticationExtension
    {
        public static async Task<IResponse<AuthenticationResponse>> AuthenticateAsync(this TradgradsgolfApiClient client, CredentialsModel model)
        {
            try
            {
                model.Validate();

                var response = await client.PostAsync("Authentication", model);
                var result = await client.Response<AuthenticationResponse>(response);

                if(result.StatusCode == System.Net.HttpStatusCode.OK)                
                    client.SetAuthenticationHeaderValue(new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Result.Token));
                
                return result;
            }
            catch (Exception ex)
            {
                return client.Response<AuthenticationResponse>(ex);
            }
        }

        public static async Task<IResponse> IsAuthorizedAsync(this TradgradsgolfApiClient client)
        {
            try
            {
                var response = await client.GetAsync("Authorization/IsAuthorized");

                return await client.Response(response);
            }
            catch (Exception ex)
            {
                return client.Response(ex);
            }
        }
    }
}
