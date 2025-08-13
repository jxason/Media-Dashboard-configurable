using System.Threading.Tasks;
using FinanzasTaxista_View.Models; 

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync(int idUsuario);
}
