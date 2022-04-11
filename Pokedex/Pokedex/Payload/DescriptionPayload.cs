namespace Pokedex.Payload
{
	public class DescriptionPayload
	{
		public DescriptionPayload(string text)
		{
			this.text = text;
		}
		public string text { get; set; }
	}
}
