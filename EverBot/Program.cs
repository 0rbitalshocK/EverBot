using System;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using OpenWeatherMap.Standard;
using ICanHazDadJoke.NET;
using System.Linq;
using SeeShahp;











// i've put a whole lot of lines between different sections so you can focus on small parts to better understand what they are doing. 
//the very first class you see "String extensions" is an extension string i googled, because in programming...ignoring case (i.e MoRgAn) is difficult. this allows me to ignore case for dad jokes below.

//the second class is a list of Commands. They consist of three major parts. 
//First the [Command("commandhere")] portion. Whatever is in the "" is what the user will type to initiate that command. 
//Second, the Public async Task, which is just for coding...you can call the task whatever you like but i would call it whatever you called the command above it.
//third, the Await where you respond with something. ctx is just what i chose to call the Commandcontext but you can call it whatever you like. Commandcontext (in the cases below, ctx) contain all the stuff from that message....who sent it, when did they send it, etc. etc. 
//access that information by doing ctx. and look at the list of available things (with wrenches next to them).  for whatever reason the username is commandcontext.User.Mention not just user.
//i think what this is... is that if i type in morgusborg in the chat it doesn't mention you it just types your username. adding .mention to the end does the hashtag at the front. 



namespace MyFirstBot
{

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
        public class MyCommands
    {
        //Hello Command
        [Command("hi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
        }
        //Generates a random number
        [Command("roll")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(min, max)}");
        }
        [Command ("Sheff")]
        public async Task SheffJoke(CommandContext ctx)
        {
            await ctx.RespondAsync($"Hah, 1080pleb");
        }
        [Command ("Weather")]
        public async Task Weather(CommandContext ctx, string zip)
        {
            string key = "2ac2e41aef5c35d7850af6871d5daa69";
            Forecast forecast = new Forecast();
            int zipCode;
            //morgan - a bool is a boolean, which is a true/false. in c# you can instantly set a bool to be either true or false by testing something that returns true or false. int.tryparse attempts to take a string, and convert it to an integer. if it succeeds then isNumeric will be true.
            //below, is an if where if it's an integer..then it's clearly a zip code so it runs the first half. otherwise it assumes it's a city/state with a comma in the middle. 
            bool isNumeric = int.TryParse(zip, out zipCode);
            if (isNumeric)
            {
                WeatherData data = null;
                Task getWeather = Task.Run(async () => { data = await forecast.GetWeatherDataByZipAsync(key, zip, "us", WeatherUnits.metric); });
                getWeather.Wait();
                await ctx.RespondAsync($"{ctx.User.Mention}, here's the deets: {data.weather[0].description}\n Low of {((9.0 / 5.0) * data.main.temp_min) + 32} with a high of {((9.0 / 5.0) * data.main.temp_max) + 32}");
                Main main = new Main();
                Console.WriteLine($"weather: {data.weather[0].main}\n {data.weather[0].description}");
                
            }
            else
            {
                WeatherData dataCity = null;
                //split city and state and pray they used a damn comma!!


                string city = zip.Substring(0,zip.IndexOf(","));
                string state = zip.Substring(zip.LastIndexOf(",")+1);
                Console.WriteLine($"City: {city} \nState:{state}");
                Task getWeatherCity = Task.Run(async () => { dataCity = await forecast.GetWeatherDataByCityNameAsync(key,city,"us",WeatherUnits.imperial); });
                getWeatherCity.Wait();
                await ctx.RespondAsync($"{ctx.User.Mention}, here's the deets: {dataCity.weather[0].description}\n Low of {dataCity.main.temp_min} with a high of {dataCity.main.temp_max}");
            }
        }
    }

class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
      

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var libraryName = "dadJokesForOrbitalshocKsDiscord";
            var contactUri = "bronco8622@gmail.com";
            int SheffCount = 0;        
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "NDQ4MDA4NDQxNzk4MTk3MjQ5.DeP4bQ.kcBpbZbpA6FjdvFtsGIkKtdhDwY",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
            












            //TW - Leave this here for an example of how solution "listens" for certain phrases that don't use the commands up above. 
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!");
            };
            //END











            //listen for dad joke request
            discord.MessageCreated += async e =>
            {
                bool DadJokeRequested = e.Message.Content.Contains("Dad Joke", StringComparison.OrdinalIgnoreCase);

                if (DadJokeRequested)
                {
                    //Create a connection to joke site
                    var client = new DadJokeClient(libraryName, contactUri);
                    // getting a dad joke
                    string dadJoke = await client.GetRandomJokeStringAsync();
                    await e.Message.RespondAsync(dadJoke);
                }
            };




            //troll sheff. make him have to do something 3 times haha. increase integer above
            //discord.MessageCreated += async e =>
            //{
            //    if (e.Author.Username.Contains("Orbitalshock", StringComparison.OrdinalIgnoreCase))
            //    {
            //        string lols = LOLSPEAK.GIMIE(e.Message.Content);
            //        await Task.Delay(10000);
            //        await e.Message.RespondAsync("**" + lols.ToUpper() + "**");
            //        //switch (SheffCount)
            //        //{
            //        //    case 0:
            //        //        {
            //        //            await e.Message.RespondAsync($"{e.Author.Mention}, There seems to be something wrong with your monitor. :MasterRace:");
            //        //            SheffCount++;
            //        //            break;
            //        //        }
            //        //    case 1:
            //        //        {
            //        //            await e.Message.RespondAsync($"{e.Author.Mention}, What do 1080P and a garbage truck have in common? They both belong at the dump! :MasterRace:");
            //        //            SheffCount++;
            //        //            break;
            //        //        }
            //        //    default:
            //        //        {
            //        //            break;
            //        //        }

            //        //}
            //    }
            //};




            // super easy example of three VERY common things in c#. this is a very lazy request but will work for most cases. WebClient is something that does a "GET" to an API, among hundreds of other things.
            //Here, i've done a GET to http://api.icndb.com/jokes/random  . The second thing you see is something that is used in countless millions of apps written in c#. it's called Newtonsoft.json  it deserializes json.
            //notice the <> that has dynamic written in it. what this means...is that as long as i spelled it right and put it in the right order.....the c# will assume i know the structure of the json that's being returned. this means i don't have to create a class, or do any code 
            // to tell the system what JSON Structure that API will return. navigate to the above link and you'll see what i mean. first there's a value, and under each value is a joke. the reason it is organized this way is you can actually ask for 5 jokes...where each value would have 1 joke attached to it. 
            // here is an example of 5 jokes. notice the brackets after the "Value" segment. that denotes that everything after it is 'a child' of the value....similar to how a menu on a website works (i.e Eletronics will have children menu items such as cameras, tvs, home audio, appliances, sex toys, etc.).

            //the httputility.htmldecode gets rid of things like &quot; instead of " that you see in programming when returning something from html.
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.Contains("chuck", StringComparison.OrdinalIgnoreCase) || e.Message.Content.Contains("norris", StringComparison.OrdinalIgnoreCase))
                {
                    //if (e.Message.Author.IsBot)
                    //{

                    //}
                    //else
                    //{
                    //    WebClient chuckClient = new WebClient();
                    //    var chuckRequest = chuckClient.DownloadString("http://api.icndb.com/jokes/random");
                    //    var chuckJokeString = JsonConvert.DeserializeObject<dynamic>(chuckRequest);
                    //    var chuckJoke = chuckJokeString.value.joke;
                    //    string chuckJokeFormatted = HttpUtility.HtmlDecode(chuckJoke);
                    //    await e.Message.RespondAsync($"{chuckJokeString.value.joke}");

                    //}




                    WebClient chuckClient = new WebClient();
                    var chuckJoke = chuckClient.DownloadString("http://api.icndb.com/jokes/random");
                    var jokes = JsonConvert.DeserializeObject<dynamic>(chuckJoke);
                    //var serializer = new json
                    Console.WriteLine(jokes.value.joke);
                    var returnableJoke = jokes.value.joke;
                    await e.Message.RespondAsync($"{returnableJoke}");
                }
            };















            //Registers the commands created above in the Public Class MyCommands section at the very top of solution
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = ";;",                
            });
            commands.RegisterCommands<MyCommands>();
            await discord.ConnectAsync();
            await Task.Delay(-1);
            //END
        }
        //handles errors and displays them on console if it doesn't crash the entire thing.
        private static Task asynce(DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

