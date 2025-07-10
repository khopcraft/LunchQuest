
# LunchQuest


Sync your Questrade account balances to LunchMoney automatically. This tool will update existing Questrade accounts and automatically create new ones as needed.

To get this working for you, change the API keys in the LunchMoneyHandler and QuestradeHandler to the corresponding keys for the webapps.

You can do that here for LunchMoney: https://my.lunchmoney.app/developers

And here, under "Activating IQ API centre", for Questrade: https://www.questrade.com/api/documentation/getting-started



This release is Version 1.0. There are likely bugs as I am not a developer. I have not implemented all the features I would like to for this app. The following is a TODO list for features I will add when I can be bothered another time.

#TODO
---------
- - 

- Check for LunchMoney users currency and use that as a default for Questrade accounts instead of defaulting to CAD always
- Implement proper async programming, not because it is necessary but because I would like to learn 
- Implement user inputs for setting up API Keys for the first time
- Compile binaries for Windows, Mac, and at least 1 linux distro
- -

#

Credits to the creators of Restsharp for making it super easy to deal with Restful Web requests.

Credits to the creators of JSON.net for their JSON serializer/deserializer. It made dealing with JSON very easy.

