using LunchQuest;

//Prepare our WebAPI handlers
var lunch = new LunchMoneyHandler();
QuestradeHandler quest =  new QuestradeHandler();

//Get Questrade accounts and convert them to assets
List<Asset> assets = quest.GetAssets();
//Create or update assets on LunchMoney
var thing = lunch.PutAssets(assets);