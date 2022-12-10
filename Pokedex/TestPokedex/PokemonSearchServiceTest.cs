using Pokedex.Services;
using Xunit;

namespace TestPokedex
{
	public class PokemonSearchServiceTest
	{
		public string mewtwoName = "mewtwo";
		public string mewtwoDescription = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
		public string mewtwoHabitat = "rare";

		public string steelixName = "steelix";
		public string steelixDescription = "It is thought its body transformed as a result of iron accumulating internally from swallowing soil.";
		public string steelixHabitat = "cave";

		public string pikachuName = "pikachu";
		public string pikachuDescription = "When several of these POKéMON gather, their electricity could build and cause lightning storms.";
		public string pikachuHabitat = "forest";

		public PokemonSearchService service;

		public PokemonSearchServiceTest()
		{
			service = new();
		}

		[Fact]
		async public void ShouldFindMewtwo()
		{
			var pokemon = await service.SearchPokemon(mewtwoName);

			Assert.Equal(mewtwoName, pokemon.name);
			Assert.True(pokemon.isLegendary);
			Assert.Equal(mewtwoDescription, pokemon.description);
			Assert.Equal(mewtwoHabitat, pokemon.habitat);
		}

		[Fact]
		async public void ShouldFindSteelix()
		{
			var pokemon = await service.SearchPokemon(steelixName);

			Assert.Equal(steelixName, pokemon.name);
			Assert.False(pokemon.isLegendary);
			Assert.Equal(steelixDescription, pokemon.description);
			Assert.Equal(steelixHabitat, pokemon.habitat);
		}

		[Fact]
		async public void ShouldFindPikachu()
		{
			var pokemon = await service.SearchPokemon(pikachuName);

			Assert.Equal(pikachuName, pokemon.name);
			Assert.False(pokemon.isLegendary);
			Assert.Equal(pikachuDescription, pokemon.description);
			Assert.Equal(pikachuHabitat, pokemon.habitat);
		}

		[Fact]
		async public void ShouldNotFindPokemonGivenInexistentPokemon()
		{
			var pokemon = await service.SearchPokemon("does not exist");

			Assert.Null(pokemon);
		}
	}
}
