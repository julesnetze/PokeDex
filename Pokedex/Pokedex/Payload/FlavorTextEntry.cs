namespace Pokedex.Payload
{
	public class FlavorTextEntry
	{
		public string flavor_text { get; set; }
		public FlavorTextEntryLanguage language { get; set; }
		public FlavorTextEntryVersion version { get; set; }
	}
}
