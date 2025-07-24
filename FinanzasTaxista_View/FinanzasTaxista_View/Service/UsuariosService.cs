using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;


namespace FinanzasTaxista_View.Service
{
    public class UsuariosService
    {
        //declaracion de las variables que se van a utilizar para conseguir la data
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public UsuariosService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Usuarios/";
        }



        // Metodo para obtener todos los usuarios

        public async Task<List<UsuariosModels>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<UsuariosModels>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<UsuariosModels>();
            }
            return new List<UsuariosModels>();
        }


    }
}
