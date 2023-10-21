using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private readonly DbAppContext _context;

        public ProveedorRepository(DbAppContext context) : base(context)
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

    public override async Task<(int totalRegistros, IEnumerable<Proveedor> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Proveedores as IQueryable<Proveedor>;
    
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