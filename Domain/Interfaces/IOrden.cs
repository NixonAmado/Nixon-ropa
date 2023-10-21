using Domain.Entities;

namespace Domain.Interfaces;
public interface IOrden : IGenericRepository<Orden>
{
    Task<IEnumerable<OrdenCliente>> GetAllOrdenesbyIdCliente(int idCliente);
}
