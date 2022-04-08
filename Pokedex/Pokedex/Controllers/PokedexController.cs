using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokedex.Domain;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("[pokemon]")]
	public class PokedexController : ControllerBase
	{
		[HttpGet]
		public Pokemon Get(string pokemonName)
		{
			return GeneratePokemon(pokemonName).Result;
		}

		async public Task<Pokemon> GeneratePokemon(string name)
		{
			var client = new HttpClient();
			var response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{name}");

			var cont = await response.Content.ReadAsStringAsync();

			var json = JsonConvert.DeserializeObject<PokeObject>(cont);
			var description = json.flavor_text_entries.Find(entry => entry.language.name == "en").flavor_text;

			var test = Regex.Replace(description, @"[^0-9a-zA-Z:,.]+", " ");

			response.Dispose();
			client.Dispose();

			return new Pokemon(json.name, test, json.habitat.name, json.is_legendary);
		}
	}
}
