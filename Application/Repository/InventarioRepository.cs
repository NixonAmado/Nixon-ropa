using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class InventarioRepository : GenericRepository<Inventario>, IInventario
    {
        private readonly DbAppContext _context;

        public InventarioRepository(DbAppContext context) : base(context)
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
    //Listar los productos y tallas del inventario. La consulta debe mostrar el id del inventario, nombre del producto, tallas y cantidad de cada talla.
    public async Task<IEnumerable<Inventario>> GetProductoByInv()
    {
        return await _context.Inventarios
                .Include(p => p.Prenda)
                .ToListAsync();
                            

    }    

    public override async Task<(int totalRegistros, IEnumerable<Inventario> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Inventarios as IQueryable<Inventario>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.CodInventario.ToString() == search);
                }
    
                query = query.OrderBy(p => p.CodInventario);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }

    }
}