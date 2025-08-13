using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using FinanzasTaxista_View.Models;

public class DashboardModel : PageModel
{
    private readonly IDashboardService _dashboardService;

    public DashboardResponse DashboardData { get; set; }


    public DashboardModel(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task OnGetAsync()
    {
        // Trae la data del dashboard (viajes, gastos, etc.)
        DashboardData = await _dashboardService.GetDashboardAsync(1);

    }
}
