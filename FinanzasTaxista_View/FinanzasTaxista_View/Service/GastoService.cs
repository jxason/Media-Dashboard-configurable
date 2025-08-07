using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;

namespace FinanzasTaxista_View.Service
{
    public class GastoService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public GastoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Gasto/";
        }

        // Método para obtener todos los gastos.

        public async Task<List<GastoModel>> GetGastosAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<GastoModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<GastoModel>();
            }
            return new List<GastoModel>();
        }

        public async Task<bool> AddGastoAsync(GastoModel gasto)
        {

            var jsonGasto = JsonSerializer.Serialize(gasto);
            var content = new StringContent(jsonGasto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }

        // Método para actualizar un viaje por ID.
        public async Task<bool> UpdateGastoAsync(GastoModel gasto)
        {

            var jsonGasto = JsonSerializer.Serialize(gasto);
            var content = new StringContent(jsonGasto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{gasto.id}", content);

            return response.IsSuccessStatusCode;

        }

    }

}
