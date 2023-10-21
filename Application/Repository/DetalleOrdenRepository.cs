using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class DetalleOrdenRepository : GenericRepository<DetalleOrden>, IDetalleOrden
    {
        private readonly DbAppContext _context;

        public DetalleOrdenRepository(DbAppContext context) : base(context)
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




    public override async Task<(int totalRegistros, IEnumerable<DetalleOrden> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.DetalleOrdenes as IQueryable<DetalleOrden>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.IdOrden.ToString() == search);
                }
    
                query = query.OrderBy(p => p.IdOrden);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }

    }
}