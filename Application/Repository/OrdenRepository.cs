using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class OrdenRepository : GenericRepository<Orden>, IOrden
    {
        private readonly DbAppContext _context;

        public OrdenRepository(DbAppContext context) : base(context)
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
    //Listar todas las ordenes de producción cuyo estado sea en proceso.   
    public async Task<IEnumerable<Orden>> GetAllOrdenesProduccionByEstado(string estado)
    {
        return await _context.Ordenes         
                            .Where(p => p.Estado.TipoEstado.Descripcion == estado)
                            .ToListAsync();
    }    
    /*Listar las ordenes de producción que pertenecen a un cliente especifico. El usuario debe ingresar el IdCliente y debe obtener la siguiente información:

1. IdCliente, Nombre, Municipio donde se encuentra ubicado.
2. Nro de orden de producción, fecha y el estado de la orden de producción (Se debe mostrar la descripción del estado, código del estado, valor total de la orden de producción.
3. Detalle de orden: Nombre de la prenda, Código de la prenda, Cantidad, Valor total en pesos y en dólares.
    */
    public async Task<IEnumerable<OrdenCliente>> GetAllOrdenesbyIdCliente(int idCliente)
    {
        return await _context.Ordenes         
                            .Where(p => p.Cliente.Identificacion.Equals(idCliente))
                            .Include(p => p.Cliente)
                            .ThenInclude(p => p.Municipio)
                            .Include(p => p.DetalleOrdenes)
                            .ThenInclude(p => p.Prenda)
                            .Select(p => new OrdenCliente
                            {
                                Fecha = p.Fecha,
                                Cliente = p.Cliente,
                                Estado = p.Estado,
                                ValorTotalOrden = (decimal)p.DetalleOrdenes.Select(p => p.CantidadProducida * p.Prenda.ValorUnitCop).FirstOrDefault(),
                                DetalleOrdenes = p.DetalleOrdenes
                            })
                            .ToListAsync();
    }    
        

    public override async Task<(int totalRegistros, IEnumerable<Orden> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Ordenes as IQueryable<Orden>;
    
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