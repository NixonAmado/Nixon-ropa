using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class InsumoRepository : GenericRepository<Insumo>, IInsumo
    {
        private readonly DbAppContext _context;

        public InsumoRepository(DbAppContext context) : base(context)
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



    //Listar los insumos que pertenecen a una prenda especifica. El usuario debe ingresar el código de la prenda.
    public async Task<IEnumerable<Insumo>> GetAllInsumosByPrenda(int codigo )
    {
        return await _context.Prendas.Where(e => e.IdPrenda == codigo)
            .SelectMany(e => e.Insumos)
            .ToListAsync();
        
    }
    //Listar los Insumos que son vendidos por un determinado proveedor cuyo tipo de persona sea Persona Jurídica. El usuario debe ingresar el Nit de proveedor.
    public async Task<IEnumerable<Insumo>> GetAllByProvedorTipoPersona(string Tpersona, int Nit )
    {
        return await _context.Insumos         
                            .Where(p => p.InsumosProveedores.Any(p => p.Proveedor.TipoPersona.Nombre.ToUpper() == Tpersona.ToUpper() && 
                            p.Proveedor.NitProveedor == Nit))
                            .ToListAsync();
    }
    
    




    
    public override async Task<(int totalRegistros, IEnumerable<Insumo> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Insumos as IQueryable<Insumo>;
    
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