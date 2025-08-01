using System.Text.Json;
using FinanzasTaxista_View.Models;
using System.Text;
using FinanzasTaxista_View.DTO_s;
using FinanzasTaxista_View.Models.DTO_s;
using FinanzasTaxista_View.Service;


namespace FinanzasTaxista_View.Service
{
    public class UsuarioService
    {
        // Declaración de las variables que se van a utilizar para conseguir la data.

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly RolService _rolService;// Servicio para obtener roles dinámicamente

        public UsuarioService(HttpClient httpClient, IConfiguration configuration, RolService rolService/*Obtener el rol dinamicamente*/)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiUrl"] + "Usuario/";
            _rolService = rolService;// Obtener el rol dinamicamente
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

        // Metodo para registrar un nuevo usuario.
        public async Task<ResultadoRegistro> RegisterAsync(UsuarioRegisterDTO dto, string nombreRol = "invitado")
        {
            // Buscar rol dinamicamente por nombre
            var rol = await _rolService.GetRolPorNombreAsync(nombreRol);
            if (rol == null)
            {
                return new ResultadoRegistro
                {
                    Exito = false,
                    Mensaje = $"No se pudo obtener el rol '{nombreRol}' desde la API."
                };
            }

            // Crear objeto UsuarioModel con el ID del rol encontrado
            var usuario = new UsuarioModel
            {
                nombre_usuario = dto.nombre_usuario,
                apellido1 = dto.apellido1,
                apellido2 = dto.apellido2 ?? "",
                correo_electronico = dto.correo_electronico,
                contrasena = dto.contrasena,
                id_rol = rol.id
            };

            var jsonUser = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return new ResultadoRegistro { Exito = false, Mensaje = error };
                }

                return new ResultadoRegistro { Exito = true };
            }
            catch (Exception ex)
            {
                return new ResultadoRegistro { Exito = false, Mensaje = ex.Message };
            }
        }



    }

}
