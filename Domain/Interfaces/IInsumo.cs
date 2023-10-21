using Domain.Entities;

namespace Domain.Interfaces;
public interface IInsumo : IGenericRepository<Insumo>
{
    Task<IEnumerable<Insumo>> GetAllInsumosByPrenda(int codigo );
    Task<IEnumerable<Insumo>> GetAllByProvedorTipoPersona(string proveedor, string Tpersona, int Nit );
}
