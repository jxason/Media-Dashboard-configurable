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
            try
            {
                // USAR POST CON QUERY PARAMETERS COMO EN SWAGGER
                var url = $"{_apiUrl}?nombre_categoria={Uri.EscapeDataString(categoria.nombre_categoria)}";

                // POST con body vacío (como en el curl de Swagger)
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                return false;
            }
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
