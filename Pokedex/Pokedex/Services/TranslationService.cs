using Newtonsoft.Json;
using Pokedex.Domain;
using Pokedex.Enums;
using Pokedex.Payload;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Services
{
	public class TranslationService : ITranslationService
	{
		public string baseUrl = "https://api.funtranslations.com/translate";
		async public Task<string> TranslatePokemonDescription(Pokemon pokemon)
		{
			if (pokemon.isLegendary || pokemon.habitat == "cave")
			{
				return await GetTranslation(pokemon.description, TranslationType.Yoda);
			}
			else
			{
				return await GetTranslation(pokemon.description, TranslationType.Shakespeare);
			}
		}

		async public Task<string> GetTranslation(string textToTranslate, TranslationType translationType)
		{
			var client = new HttpClient();
			StringContent content = CreateContent(textToTranslate);
			var url = CreateUrl(translationType);

			var response = await client.PostAsync(url, content);
			var responseContent = await response.Content.ReadAsStringAsync();
			var json = JsonConvert.DeserializeObject<TranslationPayload>(responseContent);

			client.Dispose();
			response.Dispose();
			content.Dispose();

			return json.contents.translated;
		}

		private string CreateUrl(TranslationType translationType)
		{
			var url = string.Empty;
			if (translationType == TranslationType.Yoda)
			{
				url = $"{baseUrl}/yoda.json";
			}
			else if (translationType == TranslationType.Shakespeare)
			{
				url = $"{baseUrl}/shakespeare.json";
			}
			return url;
		}

		private StringContent CreateContent(string textToTranslate)
		{
			string requestBody = JsonConvert.SerializeObject(new DescriptionPayload(textToTranslate));
			var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
			return content;
		}
	}
}
