namespace Pokedex.Domain
{
	public class Pokemon
	{
		public string Name;
		public string Description;
		public string Habitat;
		public bool IsLegendary;

		public Pokemon(string name, string description, string habitat, bool isLegendary)
		{
			Name = name;
			Description = description;
			Habitat = habitat;
			IsLegendary = isLegendary;
		}
	}
}
