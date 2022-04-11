using Pokedex.Domain;
using System.Threading.Tasks;

namespace Pokedex.Services
{
	public interface IPokemonSearchService
	{
		public Task<Pokemon> SearchPokemon(string name);
	}
}
