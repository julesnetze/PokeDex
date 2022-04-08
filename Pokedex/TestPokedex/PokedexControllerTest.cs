using Pokedex.Controllers;
using Pokedex.Domain;
using System.Net.Http;
using Xunit;

namespace TestPokedex
{
	public class PokedexControllerTest
	{
		public string mewtwoName = "mewtwo";
		public string mewtwoDescription = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
		public string mewtwoHabitat = "rare";

		public string steelixName = "steelix";
		public string steelixDescription = "The iron it ingested with the soil it swallowed transformed its body and made it harder than diamonds.";
		public string steelixHabitat = "cave";

		public string pikachuName = "pikachu";
		public string pikachuDescription = "When several of these POKéMON gather, their electricity could build and cause lightning storms.";
		public string pikachuHabitat = "forest";

		public PokedexController controller;

		public PokedexControllerTest()
		{
			controller = new();
		}

		[Fact]
		async public void ShouldGenerateMewtwo()
		{
			var pokemon = await controller.GeneratePokemon(mewtwoName, false);

			Assert.Equal(mewtwoName, pokemon.Name);
			Assert.True(pokemon.IsLegendary);
			Assert.Equal(mewtwoDescription, pokemon.Description);
			Assert.Equal(mewtwoHabitat, pokemon.Habitat);
		}

		[Fact]
		async public void ShouldGenerateSteelix()
		{
			var pokemon = await controller.GeneratePokemon(steelixName, false);

			Assert.Equal(steelixName, pokemon.Name);
			Assert.False(pokemon.IsLegendary);
			Assert.Equal(steelixDescription, pokemon.Description);
			Assert.Equal(steelixHabitat, pokemon.Habitat);
		}

		[Fact]
		async public void ShouldGeneratePikachu()
		{
			var pokemon = await controller.GeneratePokemon(pikachuName, false);

			Assert.Equal(pikachuName, pokemon.Name);
			Assert.False(pokemon.IsLegendary);
			Assert.Equal(pikachuDescription, pokemon.Description);
			Assert.Equal(pikachuHabitat, pokemon.Habitat);
		}

		[Fact]
		async public void ShouldApplyYodaTranslationToMewtwo()
		{
			var client = new HttpClient();
			var mewtwo = new Pokemon(mewtwoName, mewtwoDescription, mewtwoHabitat, true);

			var translation = await controller.Translate(client, mewtwo);
			client.Dispose();
			Assert.Equal("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", translation);
		}

		[Fact]
		async public void ShouldApplyYodaTranslationToSteelix()
		{
			var client = new HttpClient();
			var steelix = new Pokemon(steelixName, steelixDescription, steelixHabitat, false);

			var translation = await controller.Translate(client, steelix);
			client.Dispose();

			Assert.Equal("With the soil it swallowed transformed its body and made it harder than diamonds,  the iron it ingested.", translation);
		}

		[Fact]
		async public void ShouldApplyShakespeareTranslationToPikachu()
		{
			var client = new HttpClient();
			var steelix = new Pokemon(pikachuName, pikachuDescription, pikachuHabitat, false);

			var translation = await controller.Translate(client, steelix);
			client.Dispose();

			Assert.Equal("At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms.", translation);
		}
	}
}
