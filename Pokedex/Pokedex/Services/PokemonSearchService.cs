using Newtonsoft.Json;
using Pokedex.Domain;
using Pokedex.Payload;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pokedex.Services
{
	public class PokemonSearchService : IPokemonSearchService
	{
		async public Task<Pokemon> SearchPokemon(string name)
		{
			var client = new HttpClient();
			var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{name}");
			var cont = await response.Content.ReadAsStringAsync();
			response.Dispose();
			client.Dispose();

			if(response.IsSuccessStatusCode)
			{
				var json = JsonConvert.DeserializeObject<PokemonPayload>(cont);
				var text = json.flavor_text_entries.Find(entry => entry.language.name == "en").flavor_text;
				var description = Regex.Replace(text, @"[^0-9a-zA-ZÀ-ȕ:,.]+", " ");

				var pokemon = new Pokemon(json.name, description, json.habitat.name, json.is_legendary);
				return pokemon;
			}
			else
			{
				return null;
			}
		}
	}
}
