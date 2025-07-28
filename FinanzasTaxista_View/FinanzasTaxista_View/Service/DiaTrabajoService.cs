using FinanzasTaxista_View.Models;
using System.Text;
using System.Text.Json;

namespace FinanzasTaxista_View.Service
{
    public class DiaTrabajoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public DiaTrabajoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "DiaTrabajo/";
        }

        // Método para obtener todos los dias de trabajo.

        public async Task<List<DiaTrabajoModel>> GetDiasTrabajoAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<DiaTrabajoModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DiaTrabajoModel>();
            }
            return new List<DiaTrabajoModel>();
        }

        public async Task<bool> AddDiaTrabajoAsync(DiaTrabajoModel diaTrabajo)
        {

            var jsonDia = JsonSerializer.Serialize(diaTrabajo);
            var content = new StringContent(jsonDia, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateDiaTrabajoAsync(DiaTrabajoModel diaTrabajo)
        {
            var jsonDia = JsonSerializer.Serialize(diaTrabajo);
            var content = new StringContent(jsonDia, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{diaTrabajo.id}", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> DeleteDiaTrabajoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            return response.IsSuccessStatusCode;
        }
    
    }
}
