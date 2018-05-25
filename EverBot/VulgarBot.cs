using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EverBot
{
    public class VulgarBot
    {
        static string BaseLink = "http://www.foaas.com/";

        /// <summary>
        /// Get a random Fuck for your stupid fucking application that isn't worth a single fuck.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> RandomAsync(
            string Company ,string From , string Name, string Do , string Something,
            string Reference, string Reaction , string Behavior, string Thing, string Language)
        {
            var Responses = new List<string>
            {
                $"anyway/{Company}/{From}",
                $"asshole/{From}",
                $"awesome/{From}",
                $"back/{Name}/{From}",
                $"bag/{From}",
                $"ballmer/{Name}/{Company}/{From}",
                $"bday/{Name}/{From}",
                $"because/{From}",
                $"blackadder/{Name}/{From}",
                $"bm/{Name}/{From}",
                $"bucket/{From}",
                $"bus/{Name}/{From}",
                $"bye/{From}",
                $"caniuse/tool/{From}",
                $"chainsaw/{Name}/{From}",
                $"cocksplat/{Name}/{From}",
                $"cool/{From}",
                $"cup/{From}",
                $"dalton/{Name}/{From}",
                $"deraadt/{Name}/{From}",
                $"diabetes/{From}",
                $"donut/{Name}/{From}",
                $"dosomething/{Do}/{Something}/{From}",
                $"everyone/{From}",
                $"everything/{From}",
                $"fascinating/{From}",
                $"field/{Name}/{From}/{Reference}",
                $"flying/{From}",
                $"fyyff/{From}",
                $"gfy/{Name}/{From}",
                $"give/{From}",
                $"greed/noun/{From}",
                $"horse/{From}",
                $"immensity/{From}",
                $"ing/{Name}/{From}",
                $"keep/{Name}/{From}",
                $"keepcalm/{Reaction}/{From}",
                $"king/{Name}/{From}",
                $"life/{From}",
                $"linus/{Name}/{From}",
                $"look/{Name}/{From}",
                $"looking/{From}",
                $"madison/{Name}/{From}",
                $"maybe/{From}",
                $"me/{From}",
                $"mornin/{From}",
                $"no/{From}",
                $"nugget/{Name}/{From}",
                $"off/{Name}/{From}",
                $"off-with/{Behavior}/{From}",
                $"outside/{Name}/{From}",
                $"particular/{Thing}/{From}",
                $"pink/{From}",
                $"problem/{Name}/{From}",
                $"programmer/{From}",
                $"pulp/{Language}/{From}",
                $"retard/{From}",
                $"ridiculous/{From}",
                $"rtfm/{From}",
                $"sake/{From}",
                $"shakespeare/{Name}/{From}",
                $"shit/{From}",
                $"shutup/{Name}/{From}",
                $"single/{From}",
                $"thanks/{From}",
                $"that/{From}",
                $"think/{Name}/{From}",
                $"thinking/{Name}/{From}",
                $"this/{From}",
                $"thumbs/{Name}/{From}",
                $"too/{From}",
                $"tucker/{From}",
                $"what/{From}",
                $"xmas/{Name}/{From}",
                $"yoda/{Name}/{From}",
                $"you/{Name}/{From}",
                $"zayn/{From}",
                $"zero/{From}"
            };
            var RandomResponse = $"{BaseLink}{Responses[new Random().Next(0, Responses.Count)]}";
            var Request = new HttpRequestMessage()
            {
                RequestUri = new Uri(RandomResponse),
                Method = HttpMethod.Get
            };
            var Client = new HttpClient();
            Request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var Response = await Client.SendAsync(Request).ConfigureAwait(false);
            var Content = Response.Content;
            return await Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}