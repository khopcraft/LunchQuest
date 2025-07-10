using Newtonsoft.Json;

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
    public int id { get; set; }
    
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
    public bool exclude_transactions { get; set; }

    public Asset(string typeName, string subtypeName, string name, string displayName,
        string balance, string to_base, string balance_as_of, string currency, string institution_name,
        string closed_on, bool excludeTransactions)
    {
        this.type_name = typeName;
        this.subtype_name = subtypeName;
        this.name = name;
        this.display_name = displayName;
        this.balance = balance;
        this.to_base = to_base;
        this.balance_as_of = balance_as_of;
        this.currency = currency;
        this.institution_name = institution_name;
        this.closed_on = closed_on;
        this.exclude_transactions = excludeTransactions;
    }
}