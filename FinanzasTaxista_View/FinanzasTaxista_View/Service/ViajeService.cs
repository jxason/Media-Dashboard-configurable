using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;
using FinanzasTaxista_View.DTO_s;
using FinanzasTaxista_View.Models.DTO_s;


namespace FinanzasTaxista_View.Service
{
    public class ViajeService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public ViajeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Viaje/";
        }

        // Método para obtener todos los viajes.

        public async Task<List<ViajeModel>> GetViajesAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ViajeModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ViajeModel>();
            }
            return new List<ViajeModel>();
        }

        public async Task<bool> AddViajeAsync(ViajeModel viaje)
        {

            var jsonViaje = JsonSerializer.Serialize(viaje);
            var content = new StringContent(jsonViaje, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }
        
        // Método para actualizar un viaje por ID.
        public async Task<bool> UpdateViajeAsync(ViajeModel viaje)
        {
            
            var jsonViaje = JsonSerializer.Serialize(viaje);
            var content = new StringContent(jsonViaje, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{viaje.id}", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteViajeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            return response.IsSuccessStatusCode;
        }

    }

}
