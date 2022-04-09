using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokedex.Services;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("pokemon")]
	public class PokedexController : ControllerBase
	{
		public IPokemonSearchService pokemonSearchService;
		public ITranslationService translationService;
		public PokedexController(IPokemonSearchService pokemonSearchService)
		{
			this.pokemonSearchService = pokemonSearchService;
		}

		[HttpGet("{pokemonName}")]
		async public Task<ObjectResult> GetPokemon(string pokemonName, [FromQuery] bool funnyTranslation = false)
		{
			var pokemon = await pokemonSearchService.SearchPokemon(pokemonName);

			if(pokemon != null)
			{
				if (funnyTranslation && (pokemon.isLegendary || pokemon.habitat == "cave")) pokemon.description = "";
				var json = JsonConvert.SerializeObject(pokemon);
				return Ok(json);
			}
			else
			{
				return NotFound($"pokemon {pokemonName} not found in pokedex");
			}
		}
	}
}
