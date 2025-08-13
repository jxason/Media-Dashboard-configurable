using Microsoft.AspNetCore.Mvc;
using Finansastaxista_API.Models;
using System;
using FinanzasTaxista_Api.DBContext;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using FinanzasTaxista_Api.DTO_s;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public DashboardController(ApplicationDBContext context)
    {
        _context = context;
    }
    private const int TestUserId = 1;

    [HttpGet("{idUsuario}")]
    public IActionResult GetDashboard(int idUsuario)
    {
        int userid = TestUserId;  

        var usuario = _context.usuario
            .Where(u => u.id == userid)
            .Select(u => new
            {
                u.id,
                NombreCompleto = $"{u.nombre_usuario} {u.apellido1} {u.apellido2}",
                Rol = u.id_rol,
                u.correo_electronico
            })
            .FirstOrDefault();

        if (usuario == null)
            return NotFound("Usuario no encontrado.");

        var totalViajes = _context.viaje.Where(v => v.id_usuario == userid).Count();
        var totalIngresos = _context.viaje.Where(v => v.id_usuario == userid).Sum(v => (decimal?)v.monto) ?? 0;
        var totalGastos = _context.gasto.Where(g => g.id_usuario == userid).Sum(g => (decimal?)g.monto) ?? 0;
        var diasTrabajados = _context.dia_trabajo
            .Count(dt => _context.viaje.Any(v => v.id_dia == dt.id && v.id_usuario == userid));

        var fechaInicioMes = DateTime.Now.AddMonths(-1).Date;
        var historialMes = _context.dia_trabajo
            .Where(dt => dt.fecha >= fechaInicioMes)
            .Select(dt => new
            {
                Fecha = dt.fecha,
                Ingresos = _context.viaje
                    .Where(v => v.id_usuario == userid && v.id_dia == dt.id)
                    .Sum(v => (decimal?)v.monto) ?? 0,
                Gastos = _context.gasto
                    .Where(g => g.id_usuario == userid && g.id_dia == dt.id)
                    .Sum(g => (decimal?)g.monto) ?? 0
            })
            .OrderBy(h => h.Fecha)
            .ToList();

        var ingresosPorCategoria = _context.categoria
            .Select(c => new
            {
                Categoria = c.nombre_categoria,
                Monto = _context.viaje
                    .Where(v => v.id_usuario == userid && v.id_categoria == c.id)
                    .Sum(v => (decimal?)v.monto) ?? 0
            })
            .ToList();

        var gastosPorCategoria = _context.categoria
            .Select(c => new
            {
                Categoria = c.nombre_categoria,
                Monto = _context.gasto
                    .Where(g => g.id_usuario == userid && g.id_categoria == c.id)
                    .Sum(g => (decimal?)g.monto) ?? 0
            })
            .ToList();

        var ultimosViajes = _context.viaje
            .Where(v => v.id_usuario == userid)
            .OrderByDescending(v => v.id)
            .Take(5)
            .Select(v => new
            {
                Fecha = _context.dia_trabajo.FirstOrDefault(dt => dt.id == v.id_dia).fecha,
                v.ubicacion,
                v.monto
            })
            .ToList();

        var ultimosGastos = _context.gasto
            .Where(g => g.id_usuario == userid)
            .OrderByDescending(g => g.id)
            .Take(5)
            .Select(g => new
            {
                Fecha = _context.dia_trabajo.FirstOrDefault(dt => dt.id == g.id_dia).fecha,
                Categoria = _context.categoria.FirstOrDefault(c => c.id == g.id_categoria).nombre_categoria,
                g.monto
            })
            .ToList();

        var response = new
        {
            Usuario = usuario,
            Resumen = new
            {
                SaldoActual = totalIngresos - totalGastos,
                TotalViajes = totalViajes,
                TotalGastos = totalGastos,
                PromedioPorViaje = totalViajes > 0 ? totalIngresos / totalViajes : 0,
                DiasTrabajados = diasTrabajados
            },
            HistorialMes = historialMes,
            Categorias = new
            {
                IngresosPorCategoria = ingresosPorCategoria,
                GastosPorCategoria = gastosPorCategoria
            },
            UltimosRegistros = new
            {
                UltimosViajes = ultimosViajes,
                UltimosGastos = ultimosGastos
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> SaveDashboard([FromBody] DashboardLayoutDto dto)
    {
        // Aquí hacemos lo mismo, usar un Id fijo para pruebas
        var dash = await _context.Dashboards.FirstOrDefaultAsync(d => d.Id == TestUserId);
        if (dash == null)
        {
            dash = new Dashboard
            {
                UsuarioId = dto.IdUsuario,  
                Name = "Dashboard Usuario 1",
                JsonData = dto.JsonData
            };
            _context.Dashboards.Add(dash);
        }
        else
        {
            dash.JsonData = dto.JsonData;
            _context.Dashboards.Update(dash);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

}
