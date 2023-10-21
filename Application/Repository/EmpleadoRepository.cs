using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
    {
        private readonly DbAppContext _context;

        public EmpleadoRepository(DbAppContext context) : base(context)
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

    //Listar los empleados por un cargo especifico. Los cargos que se encuentran en la empresa son: Auxiliar de Bodega, Jefe de Producción, Corte, Jefe de bodega, Secretaria, Jefe de IT.
    public async Task<IEnumerable<Empleado>> GetAllEmpleadosByCargo(string cargo)
    {
        return await _context.Empleados    
                            .Where(p => p.Cargo.Descripcion.ToUpper() == cargo.ToUpper())
                            .ToListAsync();
    }    
    
    public override async Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Empleados as IQueryable<Empleado>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Nombre.ToString() == search);
                }
    
                query = query.OrderBy(p => p.Nombre);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }

    }
}