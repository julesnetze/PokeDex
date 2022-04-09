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
		public IPokedexService pokedexService;
		public PokedexController(IPokedexService pokedexService)
		{
			this.pokedexService = pokedexService;
		}

		[HttpGet("{pokemonName}")]
		async public Task<ObjectResult> GetPokemon(string pokemonName, [FromQuery] bool funnyTranslation = false)
		{
			var pokemon = await pokedexService.GeneratePokemon(pokemonName, funnyTranslation);

			if(pokemon != null)
			{
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
