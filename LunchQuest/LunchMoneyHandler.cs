namespace LunchQuest;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

public class LunchMoneyHandler
{
    private string token = "dbef811497a2911f4225eed040858d4d0c2090e0bb281c6341";

    //This gets the assets from lunchmoney using RestSharp
    private async Task<RestResponse> GetAssets()
    {
        RestClientOptions options = new RestClientOptions("https://dev.lunchmoney.app/v1/")
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
        };
        
        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("assets");

        var assets = await client.ExecuteGetAsync(request);
        return assets;
    }

    //We use this method to update, or create a new asset on lunchmoney
    public String PutAsset()
    {
        //This is a run until finished app. Async unnessessary 
        //Will do Async version at some point as I would like to learn how
        var response = GetAssets();
        response.Wait();

        var result = response.Result.Content;
        Console.WriteLine(result);
        
        Assets assets = JsonConvert.DeserializeObject<Assets>(result);
        
        //Iterate through all assets and find match based on type_name and subtype_name (which we set as account #)
        //Update if we have a match, insert if we do not.
        //A better thing to do here if we are so inclined is to prompt for permission to add new accounts.
        foreach (var asset in assets.assets)
        {
            Console.WriteLine(asset.name);
            Console.WriteLine(asset.balance);
        }
        
        return result;
    }
}