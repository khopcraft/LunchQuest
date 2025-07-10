using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace LunchQuest;

public class QuestradeHandler
{
    private TokenHandler tokens = new TokenHandler();
    
    private async Task<RestResponse> GetAssetsAsync()
    {
        Token token = tokens.GetToken();
        
        RestClientOptions options = new RestClientOptions(token.api_server)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token.access_Token, token.token_type)
        };
        
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("v1/accounts");

        var response = await client.ExecuteGetAsync(request);
        
        
        return response;
    }

    public void GetAssets()
    {
        var response = GetAssetsAsync();
        response.Wait();
        Console.WriteLine(response.Result.Content);
    }
}