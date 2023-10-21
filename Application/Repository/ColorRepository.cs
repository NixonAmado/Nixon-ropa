using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class ColorRepository : GenericRepository<Color>, IColor
    {
        private readonly DbAppContext _context;

        public ColorRepository(DbAppContext context) : base(context)
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

    public override async Task<(int totalRegistros, IEnumerable<Color> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Colores as IQueryable<Color>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Descripcion.ToString() == search);
                }
    
                query = query.OrderBy(p => p.Descripcion);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }

    }
}