using Newtonsoft.Json;
using SkrinTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SkrinTool.Services
{
    public class GiphyService
    {
        private static readonly string _baseUrl = "http://api.giphy.com/";
        private static readonly HttpClientHandler _handler = new HttpClientHandler { UseProxy = true, UseDefaultCredentials = true };
        private static readonly HttpClient _client = new HttpClient(_handler);

        public async Task<GiphyResponseModel> Search(string term, int limit)
        {
            GiphyResponseModel result = new GiphyResponseModel();

            var url = $"{_baseUrl}v1/gifs/search?q={term}&api_key=JWtDnRjuiytHG0f4GmBdN47wRrMUzBNy&limit={limit}";
            var response = await _client
                .GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<GiphyResponseModel>(content);
            }

            return result;
        }
    }
}
