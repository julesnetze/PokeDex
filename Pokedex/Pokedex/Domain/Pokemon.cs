namespace Pokedex.Domain
{
	public class Pokemon
	{
		public string name;
		public string description;
		public string habitat;
		public bool isLegendary;

		public Pokemon(string name, string description, string habitat, bool isLegendary)
		{
			this.name = name;
			this.description = description;
			this.habitat = habitat;
			this.isLegendary = isLegendary;
		}
	}
}
