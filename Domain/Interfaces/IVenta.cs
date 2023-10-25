using Domain.Entities;

namespace Domain.Interfaces;
public interface IVenta : IGenericRepository<Venta>
{
    Task<IEnumerable<VentaEmpleado>> GetVentasByIdEmpleado (int idEmpleado );
}
