using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;


namespace FinanzasTaxista_View.Service
{
    public class RolService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public RolService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Rol/";
        }

        // Método para obtener todos los roles.

        public async Task<List<RolModel>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<RolModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<RolModel>();
            }
            return new List<RolModel>();
        }

        public async Task<bool> AddRolAsync(RolModel rol)
        {

            var jsonRol = JsonSerializer.Serialize(rol);
            var content = new StringContent(jsonRol, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateRolAsync(RolModel rol)
        {
            var jsonRol = JsonSerializer.Serialize(rol);
            var content = new StringContent(jsonRol, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{rol.id}", content);

            return response.IsSuccessStatusCode;

        }


    }
}