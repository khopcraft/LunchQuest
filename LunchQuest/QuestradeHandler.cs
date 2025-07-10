using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace LunchQuest;

public class QuestradeHandler
{
    private TokenHandler tokens = new TokenHandler();
    Token token;
    private RestClientOptions options;
    private RestClient client;

    public QuestradeHandler()
    {
        token = tokens.GetToken();
        options = new RestClientOptions(token.api_server)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token.access_Token, token.token_type)
        };
        
        client = new RestClient(options);
    }
    
    private async Task<RestResponse> GetAssetsAsync()
    {
        RestRequest request = new RestRequest("v1/accounts");
        var response = await client.ExecuteGetAsync(request);
        return response;
    }

    //Need account number from account we are looking up 
    private async Task<RestResponse> GetBalanceAsync(string account_number)
    {
        RestRequest request =  new RestRequest($@"v1/accounts/{account_number}/balances");
        var response = await client.ExecuteGetAsync(request);
        return response;
    }
    
    //Get QuestAssets from Questrade, convert them to Assets and return
    public List<Asset> GetAssets()
    {
        List<Asset> assets  = new List<Asset>();
        
        var response = GetAssetsAsync();
        response.Wait();
        QuestAssets qassets;
        qassets = JsonConvert.DeserializeObject<QuestAssets>(response.Result.Content);
        foreach (QuestAsset a in qassets.assets)
        {
            QuestAsset q = new QuestAsset(a.type, a.number, a.status, a.clientAccountType);
            response = GetBalanceAsync(a.number);
            response.Wait();
            
            //Parse combined balances, then filter out non CAD balances
            Balances balances =  JsonConvert.DeserializeObject<Balances>(response.Result.Content);
            string balance = "";
            foreach (var b in balances.balances)
            {
                if (b.currency == "CAD")
                {
                    balance = b.totalEquity;
                    break;
                }
            }
            
            Asset asset = new Asset("investment", q.number, q.type, balance, 
                q.currency, "Questrade", false);
            assets.Add(asset);
        }
        
        return assets;
    }
}

class QuestAssets
{
    [JsonProperty("accounts")]
    public QuestAsset[] assets { get; set; }
}
class QuestAsset
{
    [JsonProperty("type")]
    public string type { get; set; }
    [JsonProperty("number")]
    public string number { get; set; }
    [JsonProperty("status")]
    public string status { get; set; }
    [JsonProperty("clientAccountType")]
    public string clientAccountType { get; set; }

    public string name { get; set; } = "";

    public string balance { get; set; } = "";

    public string currency { get; set; } = "";
    public QuestAsset(string  type, string number, string status, string clientAccountType)
    {
        this.type = type;
        this.number = number;
        this.status = status;
        this.clientAccountType = clientAccountType;
        this.name = clientAccountType + " " + type;
        
        //Get combined balance of account and currency:
        this.balance = "0";
        this.currency = "cad";
    }

    private string getName()
    {
        
        return name;
    }

    private string getBalance()
    {
        balance = "test";
        return balance;
    }
}

class Balances
{
    [JsonProperty("combinedBalances")]
    public Balance[] balances;
}
class Balance
{
    [JsonProperty("currency")]
    public string currency = "";
    [JsonProperty("totalEquity")]
    public string totalEquity = "";
}