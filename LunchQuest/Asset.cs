using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LunchQuest;

//Helper class to easily grab a list of Assets
public class Assets
{
    [JsonProperty("assets")]
    public Asset[] assets { get; set; }
}
public class Asset
{
    [JsonProperty("id")] 
    public int id { get; set; } = -1;
        
    [JsonProperty("type_name")]
    public string type_name { get; set; }
    
    [JsonProperty("subtype_name")]
    public string subtype_name { get; set; }
    
    [JsonProperty("name")]
    public string name { get; set; }
    
    [JsonProperty("display_name")]
    public string display_name { get; set; }
    
    [JsonProperty("description")]
    public string description { get; set; }
    
    [JsonProperty("balance")]
    public string balance { get; set; }
    
    [JsonProperty("to_base")]
    public string to_base { get; set; }
    
    [JsonProperty("balance_as_of")]
    public string balance_as_of { get; set; }
    
    [JsonProperty("currency")]
    public string currency { get; set; }
    
    [JsonProperty("institution_name")]
    public string institution_name { get; set; }
    
    [JsonProperty("closed_on")]
    public string closed_on { get; set; }

    [JsonProperty("exclude_transactions")] 
    public bool exclude_transactions { get; set; } = true;

    public Asset(string typeName, string subtypeName, string name, string balance, 
        string currency, string institution_name, bool excludeTransactions)
    {
        this.type_name = typeName;
        this.subtype_name = subtypeName;
        this.name = name;
        this.balance = balance;
        this.currency = currency;
        this.institution_name = institution_name;
        this.exclude_transactions = excludeTransactions;
    }

    //Returns a string ready for updating or creating new asset for lunchmoney
    public String LunchReadyAsset()
    {
        string asset = " ";
        JObject obj = JObject.FromObject(this);
        obj.Remove("id");
        obj.Remove("to_base");
        obj.Remove("description");
        asset = obj.ToString();
        return asset;
    }
}