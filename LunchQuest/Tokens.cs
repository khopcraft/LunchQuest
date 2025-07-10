using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Text;

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
    string refreshToken = "K90CTtOF2LVt3KTfgLiSo_8HD6SAPeF70";
    string tokenPath = "./questrade.token";
    private async Task<RestResponse> GetAccessToken()
    {
        //set refreshToken to stored refreshToken if questrade.token exists
        if (File.Exists(tokenPath))
        {
            using (StreamReader sr = new StreamReader(tokenPath))
            {
                refreshToken = sr.ReadToEnd();
            }
        }
        
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
       
       
       //Write refresh token to questrade.token file
       using (StreamWriter sw = new StreamWriter(tokenPath))
       {
           sw.WriteLine(token.refresh_Token);
       }
       return token;
    }
}
