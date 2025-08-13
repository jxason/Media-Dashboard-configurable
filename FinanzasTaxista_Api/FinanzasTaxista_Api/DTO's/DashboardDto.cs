namespace FinanzasTaxista_Api.DTO_s
{
    public class DashboardDto
    {
        public decimal SaldoActual { get; set; }
        public int TotalViajes { get; set; }
        public List<HistorialMesDto> HistorialMes { get; set; }
    }
}
