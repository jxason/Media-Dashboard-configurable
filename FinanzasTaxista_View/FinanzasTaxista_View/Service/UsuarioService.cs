using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;


namespace FinanzasTaxista_View.Service
{
    public class UsuarioService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.
        
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public UsuarioService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Usuario/";
        }
        
        // Método para obtener todos los usuarios.

        public async Task<List<UsuarioModel>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<UsuarioModel>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UsuarioModel>();
            }
            return new List<UsuarioModel>();
        }

        public async Task<bool> AddUsuarioAsync(UsuarioModel usuario)
        {
            
            var jsonUser = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);

            return response.IsSuccessStatusCode;

        }
        
        public async Task<bool> UpdateUsuarioAsync(UsuarioModel usuario)
        {
            var jsonUser = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{usuario.id}", content);

            return response.IsSuccessStatusCode;

        }


    }
}
