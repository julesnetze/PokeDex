using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokedex.Domain;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("[pokemon]")]
	public class PokedexController : ControllerBase
	{
		[HttpGet]
		public Pokemon GetPokemon([FromHeader] string pokemonName)
		{
			var controller = new PokedexController();

			return GeneratePokemon(pokemonName, false).Result;
		}

		async public Task<Pokemon> GeneratePokemon(string name, bool translated)
		{
			var client = new HttpClient();
			var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{name}");
			var cont = await response.Content.ReadAsStringAsync();

			var json = JsonConvert.DeserializeObject<PokeObject>(cont);
			var text = json.flavor_text_entries.Find(entry => entry.language.name == "en").flavor_text;
			var description = Regex.Replace(text, @"[^0-9a-zA-ZÀ-ȕ:,.]+", " ");
			
			var pokemon = new Pokemon(json.name, description, json.habitat.name, json.is_legendary);
			if (translated)
			{
				pokemon.Description = await Translate(client, pokemon);
			}

			response.Dispose();
			client.Dispose();

			return pokemon;
		}

		async public Task<string> Translate(HttpClient client, Pokemon pokemon)
		{
			var requestBody = JsonConvert.SerializeObject(new Payload(pokemon.Description));
			var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

			string url;
			if (pokemon.IsLegendary || pokemon.Habitat == "cave")
			{
				url = "https://api.funtranslations.com/translate/yoda.json";
			} 
			else
			{
				url = "https://api.funtranslations.com/translate/shakespeare.json";
			}

			var response = await client.PostAsync(url, content);
			var cont = await response.Content.ReadAsStringAsync();
			var json = JsonConvert.DeserializeObject<TranslationResponse>(cont);

			response.Dispose();
			content.Dispose();

			return json.contents.translated;
		}
	}
}
