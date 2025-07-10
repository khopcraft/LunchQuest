namespace LunchQuest;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

public class LunchMoneyHandler
{
    private string token = "dbef811497a2911f4225eed040858d4d0c2090e0bb281c6341";
    private RestClientOptions options;
    private RestClient client;
    public LunchMoneyHandler()
    {
        options = new RestClientOptions("https://dev.lunchmoney.app/v1/")
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
        };
        client = new RestClient(options);
    }

    //This gets the assets from lunchmoney using RestSharp
    private async Task<RestResponse> GetAssets()
    {
        RestRequest request = new RestRequest("assets");

        var assets = await client.ExecuteGetAsync(request);
        return assets;
    }

    private async Task<RestResponse> UpdateAsset(Asset asset)
    {
        Console.WriteLine("Updating asset");
        RestRequest request = new RestRequest($@"assets/{asset.id}", Method.Put);
        request.AddJsonBody(asset.LunchReadyAsset());
        
        var response = await client.ExecutePutAsync(request);
        
        return response;
        
    }
    
    private async Task<RestResponse> InsertAsset(Asset asset)
    {
        RestRequest request = new RestRequest($@"assets", Method.Post);
        //Console.WriteLine(asset.LunchReadyAsset());
        request.AddBody(asset.LunchReadyAsset());
        
        var response = await client.ExecutePostAsync(request);
        
        return response;
        
    }

    //We use this method to update, or create a new asset on lunchmoney
    public String PutAssets(List<Asset> qAssets)
    {
        //This is a run until finished app. Async unnessessary 
        //Will do Async version at some point as I would like to learn how
        var response = GetAssets();
        response.Wait();

        var result = response.Result.Content;
        Console.WriteLine(result);
        
        Assets lAssets = JsonConvert.DeserializeObject<Assets>(result);
        
        //Iterate through all assets and find match based on type_name and subtype_name (which we set as account #)
        //Update if we have a match, insert if we do not.
        //A better thing to do here if we are so inclined is to prompt for permission to add new accounts.
        foreach (Asset qa in qAssets)
        {
            bool hasMatch = false;
            foreach (Asset la in lAssets.assets)
            {
                if (qa.type_name == la.type_name && qa.subtype_name == la.subtype_name)
                {
                    hasMatch = true;
                    qa.id = la.id;
                    break;
                }
            }

            if (hasMatch)
            {
                try
                {
                    response = UpdateAsset(qa);
                    response.Wait();
                } catch (Exception e) {Console.WriteLine(e);}
            }
            else
            {
                try
                {
                    response = InsertAsset(qa);
                    response.Wait();
                }catch(Exception e){Console.WriteLine(e);}
            }
        }
        
        return result;
    }
}