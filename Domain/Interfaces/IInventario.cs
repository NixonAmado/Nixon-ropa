using Domain.Entities;

namespace Domain.Interfaces;
public interface IInventario : IGenericRepository<Inventario>
{
    Task<IEnumerable<Inventario>> GetProductoByInv();
}
