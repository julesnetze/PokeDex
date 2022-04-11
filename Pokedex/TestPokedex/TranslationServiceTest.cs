using Pokedex.Domain;
using Pokedex.Services;
using Xunit;

namespace TestPokedex
{
	public class TranslationServiceTest
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

		public TranslationService service;

		public TranslationServiceTest()
		{
			service = new();
		}

		[Fact]
		async public void ShouldApplyYodaTranslationToMewtwo()
		{
			var mewtwo = new Pokemon(mewtwoName, mewtwoDescription, mewtwoHabitat, true);

			var translation = await service.TranslatePokemonDescription(mewtwo);

			Assert.Equal("Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.", translation);
		}

		[Fact]
		async public void ShouldApplyYodaTranslationToSteelix()
		{
			var steelix = new Pokemon(steelixName, steelixDescription, steelixHabitat, false);

			var translation = await service.TranslatePokemonDescription(steelix);

			Assert.Equal("With the soil it swallowed transformed its body and made it harder than diamonds,  the iron it ingested.", translation);
		}

		[Fact]
		async public void ShouldApplyShakespeareTranslationToPikachu()
		{
			var pikachu = new Pokemon(pikachuName, pikachuDescription, pikachuHabitat, false);

			var translation = await service.TranslatePokemonDescription(pikachu);

			Assert.Equal("At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms.", translation);
		}
	}
}
