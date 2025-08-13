using System;
using System.Collections.Generic;

public class DashboardResponse
{
    public UsuarioDto Usuario { get; set; }
    public ResumenDto Resumen { get; set; }
    public List<HistorialMesDto> HistorialMes { get; set; }
    public CategoriasDto Categorias { get; set; }
    public UltimosRegistrosDto UltimosRegistros { get; set; }
}

public class UsuarioDto
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public int Rol { get; set; }
    public string CorreoElectronico { get; set; }
}

public class ResumenDto
{
    public decimal SaldoActual { get; set; }
    public int TotalViajes { get; set; }
    public decimal TotalGastos { get; set; }
    public decimal PromedioPorViaje { get; set; }
    public int DiasTrabajados { get; set; }
}

public class HistorialMesDto
{
    public DateTime Fecha { get; set; }
    public decimal Ingresos { get; set; }
    public decimal Gastos { get; set; }
}

public class CategoriasDto
{
    public List<CategoriaMontoDto> IngresosPorCategoria { get; set; }
    public List<CategoriaMontoDto> GastosPorCategoria { get; set; }
}

public class CategoriaMontoDto
{
    public string Categoria { get; set; }
    public decimal Monto { get; set; }
}

public class UltimosRegistrosDto
{
    public List<ViajeDto> UltimosViajes { get; set; }
    public List<GastoDto> UltimosGastos { get; set; }
}

public class ViajeDto
{
    public DateTime Fecha { get; set; }
    public string Ubicacion { get; set; }
    public decimal Monto { get; set; }
}

public class GastoDto
{
    public DateTime Fecha { get; set; }
    public string Categoria { get; set; }
    public decimal Monto { get; set; }
}
