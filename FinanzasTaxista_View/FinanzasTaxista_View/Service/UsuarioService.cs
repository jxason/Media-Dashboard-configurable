using System.Text.Json;
using FinanzasTaxista_View.Models; 
using System.Text;
using FinanzasTaxista_View.DTO_s;
using FinanzasTaxista_View.Models.DTO_s;


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

        // Método para obtener un usuario por ID.
        public async Task<bool> AddUsuarioAsync(UsuarioModel usuario)
        {

            if (usuario.id_rol <= 0)
            {
                return false;
            }
            // Validación de datos del usuario antes de enviar la solicitud.
            try
            {
                var jsonUser = JsonSerializer.Serialize(usuario);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Puedes agregar logging aquí si usas ILogger
                return false;
            }
        }
        // Método para obtener un usuario por ID.
        public async Task<bool> UpdateUsuarioAsync(UsuarioModel usuario)
        {
            var jsonUser = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{usuario.id}", content);

            return response.IsSuccessStatusCode;

        }

        // Método para eliminar un usuario por ID.
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            return response.IsSuccessStatusCode;
        }


        // Método para iniciar sesión de un usuario.
        public async Task<HttpResponseMessage> LoginAsync(UsuarioLoginDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}login", dto);
            return response;
        }

        // Método para registrar un nuevo usuario.
        public async Task<bool> RegisterAsync(UsuarioRegisterDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl + "register", dto);
            return response.IsSuccessStatusCode;
        }


    }

}
