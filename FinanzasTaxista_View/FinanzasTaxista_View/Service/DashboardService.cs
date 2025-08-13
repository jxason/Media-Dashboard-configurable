using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanzasTaxista_View.Models;

public class DashboardService : IDashboardService
{
    private readonly HttpClient _http;

    public DashboardService(HttpClient httpClient)
    {
        _http = httpClient;
        _http.BaseAddress = new Uri("https://localhost:7270/"); // API local
    }

    public async Task<DashboardResponse> GetDashboardAsync(int idUsuario)
    {
        return await _http.GetFromJsonAsync<DashboardResponse>($"api/dashboard/{idUsuario}");
    }
}
