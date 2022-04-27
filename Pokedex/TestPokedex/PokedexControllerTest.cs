using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using Pokedex.Controllers;
using Pokedex.Services;
using Pokedex.Domain;
using Newtonsoft.Json;

namespace TestPokedex
{
	public class PokedexControllerTest
	{
		public string mewtwoName = "mewtwo";
		public string mewtwoDescription = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
		public string mewtwoHabitat = "rare";
		public Pokemon mewtwo;
		public string mewtwoDescriptionYodaTranslation = "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";

		public PokedexController controller;
		public Mock<IPokemonSearchService> mockedPokemonSearchService = new();
		public Mock<ITranslationService> mockedTranslationService     = new();

		public PokedexControllerTest()
		{
			controller = new(mockedPokemonSearchService.Object, mockedTranslationService.Object);
			mewtwo = new(mewtwoName, mewtwoDescription, mewtwoHabitat, true);
		}

		[Fact]
		async public void ShouldReturn200GivenExistingPokemon()
		{
			mockedPokemonSearchService.Setup(x => x.SearchPokemon(mewtwoName)).ReturnsAsync(mewtwo);

			var response = await controller.GetPokemon(mewtwoName);

			Assert.Equal(JsonConvert.SerializeObject(mewtwo), response.Value);
			Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
		}

		[Fact]
		async public void ShouldReturn200WithTranslatedPokemonDescriptionGivenExistingPokemonAndTranslationSetToTrue()
		{
			mockedPokemonSearchService.Setup(x => x.SearchPokemon(mewtwoName)).ReturnsAsync(mewtwo);
			mockedTranslationService.Setup(x => x.TranslatePokemonDescription(mewtwo)).ReturnsAsync(mewtwoDescriptionYodaTranslation);

			var response = await controller.GetPokemon(mewtwoName, true);

			Assert.Equal(JsonConvert.SerializeObject(mewtwo), response.Value);
			Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
		}

		[Fact]
		async public void ShouldReturn404GivenInexistingPokemon()
		{
			var inexistentPokemon = "a not existing pokemon";
			mockedPokemonSearchService.Setup(x => x.SearchPokemon(inexistentPokemon));

			var response = await controller.GetPokemon(inexistentPokemon);

			Assert.Equal($"pokemon {inexistentPokemon} not found in pokedex", response.Value);
			Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
		}
	}
}
