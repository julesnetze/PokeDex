using Pokedex.Domain;
using System.Threading.Tasks;

namespace Pokedex.Services
{
	public interface ITranslationService
	{
		public Task<string> TranslatePokemonDescription(Pokemon pokemon);
	}
}
