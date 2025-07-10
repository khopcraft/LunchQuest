using Newtonsoft.Json;
using RestSharp;

namespace LunchQuest;

public class Token
{
    [JsonProperty ("access_token")]
    public string access_Token = "";
    [JsonProperty ("api_server")]
    public string api_server = "";
    [JsonProperty ("expires_in")]
    public int expires_in = 0;
    [JsonProperty ("refresh_token")]
    public string refresh_Token = "";
    [JsonProperty ("token_type")]
    public string token_type = "";
}

//Questrade generates access tokens with refresh tokens.
//Long story short, we need to get API access information every time we connect to questrade
//We also need to store the refresh token so that we can get the new API access information next time
public class TokenHandler
{
    //Put API token from apphub in Questrade here. This is only used if the questrade.token file 
    //has not been created yet
    string refreshToken = "mF2IDuuXdj3j_k0-M4UajPgHc0zYvlje0";
    
    private async Task<RestResponse> GetAccessToken()
    {
        RestClient client = new RestClient("https://login.questrade.com/oauth2");
        RestRequest request = new RestRequest($@"/token?grant_type=refresh_token&refresh_token={refreshToken}");
        
        RestResponse response = await client.ExecuteGetAsync(request);
        
        return response;
    }
    
    //get rest response, convert it into token object, return token object
    public Token GetToken()
    {
       Token token = new Token();
       
       var response = GetAccessToken();
       response.Wait();
       
       token = JsonConvert.DeserializeObject<Token>(response.Result.Content);
       Console.WriteLine(token.refresh_Token);//Not managing tokens properly yet. Must keep track of refresh token 
       return token;
    }
}
