using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class VentaRepository : GenericRepository<Venta>, IVenta
    {
        private readonly DbAppContext _context;

        public VentaRepository(DbAppContext context) : base(context)
        {
            _context = context;
        }

    // public override Task<Entity> GetByIdAsync(id)
    // {
    //     return await _context.Entity
    //                         .Include()
    //                         .FirstaAsync();
    // }
    
    // public override Task<IEnumerable<Entity>> GetAllAsync()
    // {
    //     return await _context.Entity
    //                         .Include()
    //                         .ToListAsync();
    // }

    
    // Listar las ventas realizadas por un empleado especifico. El usuario debe ingresar el Id del empleado y mostrar la siguiente información.
    // 1. Id Empleado
    // 2. Nombre del empleado
    // 3. Facturas: Nro Factura, fecha y total de la factura.
    public async Task<IEnumerable<VentaEmpleado>> GetVentasByIdEmpleado (int idEmpleado )
    {
        return await _context.Ventas
                            .Where(v => v.Cliente.Id == idEmpleado)
                            .Select(p => new VentaEmpleado
                            {
                                Id = p.Id,
                                Empleado = p.Empleado,
                                Fecha = p.Fecha,
                                TotalFactura = p.DetalleVentas.Sum(p => p.Cantidad * p.ValorUnit)
                            })
                            .ToListAsync();
    }
    public override async Task<(int totalRegistros, IEnumerable<Venta> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Ventas as IQueryable<Venta>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search);
                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }

    }
}