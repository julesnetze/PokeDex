using System.Collections.Generic;

namespace Pokedex.Payload
{
	public class PokemonPayload
	{
		public string name { get; set; }
		public bool is_legendary { get; set; }
		public Habitat habitat { get; set; }
		public List<FlavorTextEntry> flavor_text_entries { get; set; }
	}
}