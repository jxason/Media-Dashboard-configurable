using FinanzasTaxista_View.Models;
using System.Text.Json;
using System.Text;

namespace FinanzasTaxista_View.Service
{
    public class CategoriaService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public CategoriaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Categoria/";
        }

        // Método para obtener todos las categorias.

        public async Task<List<CategoriaModel>> GetCategoriasAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<CategoriaModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoriaModel>();
            }
            return new List<CategoriaModel>();
        }

        public async Task<bool> AddCategoriaAsync(CategoriaModel categoria)
        {

            var jsonUser = JsonSerializer.Serialize(categoria);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> UpdateCategoriaAsync(CategoriaModel categoria)
        {
            var jsonUser = JsonSerializer.Serialize(categoria);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{categoria.id}", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
