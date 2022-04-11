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
		public PokedexController(IPokemonSearchService pokemonSearchService, ITranslationService translationService)
		{
			this.pokemonSearchService = pokemonSearchService;
			this.translationService   = translationService;
		}

		[HttpGet("{pokemonName}")]
		async public Task<ObjectResult> GetPokemon(string pokemonName, [FromQuery] bool translation = false)
		{
			var pokemon = await pokemonSearchService.SearchPokemon(pokemonName);
			if(pokemon != null)
			{
				if(translation) pokemon.description = await translationService.TranslatePokemonDescription(pokemon);
				return Ok(JsonConvert.SerializeObject(pokemon));
			}
			else
			{
				return NotFound($"pokemon {pokemonName} not found in pokedex");
			}
		}
	}
}
