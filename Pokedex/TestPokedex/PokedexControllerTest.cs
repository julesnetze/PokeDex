using Pokedex.Controllers;
using Xunit;

namespace TestPokedex
{
	public class PokedexControllerTest
	{
		[Fact]
		async public void ShouldReturnPokemon()
		{
			var controller = new PokedexController();
			var name = "mewtwo";
			var description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
			var habitat = "rare";

			var result = await controller.GeneratePokemon(name);

			Assert.Equal(name, result.Name);
			Assert.True(result.IsLegendary);
			Assert.Equal(description, result.Description);
			Assert.Equal(habitat, result.Habitat);
		}
	}
}
