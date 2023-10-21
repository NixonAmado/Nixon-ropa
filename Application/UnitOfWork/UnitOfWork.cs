using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistencia.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly DbAppContext _context;
        private RoleRepository _roles;
        private UserRepository _users;
        private PrendaRepository _prendas; 
        private TipoProteccionRepository _tipoProtecciones;
        private GeneroRepository _generos;
        private EstadoRepository _estados;
        private TipoEstadoRepository _tipoEstados;
        private EmpresaRepository _empresas;
        private MunicipioRepository _municipios;
        private DepartamentoRepository _departamentos;
        private PaisRepository _paises;
        private InsumoRepository _insumos;
        private ProveedorRepository _proveedores;
        private TipoPersonaRepository _tipoPersonas;
        private ClienteRepository _clientes;
        private VentaRepository _ventas; 
        private DetalleOrdenRepository _detalleOrdenes;
        private ColorRepository _colores;
        private OrdenRepository _ordenes;
        private DetalleVentaRepository _detalleVentas;
        private EmpleadoRepository _empleados;
        private CargoRepository _cargos;
        private TallaRepository _tallas;
        private InventarioRepository _inventarios;
        private FormaPagoRepository _formaPagos;
 
        



        public UnitOfWork(DbAppContext context)
        {
            _context = context;
        }

        public IRole Roles
        {
            get
            {
                if(_roles == null)
                {
                    _roles = new RoleRepository(_context);
                }
                return _roles;
            }
        }
        public IUser Users{
            get{
                if(_users == null)
                {
                    _users = new UserRepository(_context); 
                }
                return _users;
            }
        }
        public IPrenda Prendas {
              get
            {
                if(_prendas == null)
                {
                    _prendas = new PrendaRepository(_context);
                }
                return _prendas;
            }
        }
        public ITipoProteccion TipoProtecciones {
            get
            {
                if(_tipoProtecciones == null)
                {
                    _tipoProtecciones = new TipoProteccionRepository(_context);
                }
                return _tipoProtecciones;
            }
        } 
        public IGenero Generos {
            get
            {
                if(_generos == null)
                {
                    _generos = new GeneroRepository(_context);
                }
                return _generos;
            }

        }
        public IEstado Estados {
            get
            {
                if(_estados == null)
                {
                    _estados = new EstadoRepository(_context);
                }
                return _estados;
            }
        }
        public ITipoEstado TipoEstados {
            get
            {
                if(_tipoEstados == null)
                {
                    _tipoEstados = new TipoEstadoRepository(_context);
                }
                return _tipoEstados;
            }
        }
        public IEmpresa Empresas {
            get
            {
                if(_empresas == null)
                {
                    _empresas = new EmpresaRepository(_context);
                }
                return _empresas;
            }
        }
        public IMunicipio Municipios {
            get
            {
                if(_municipios == null)
                {
                    _municipios = new MunicipioRepository(_context);
                }
                return _municipios;
            }
        }
        public IDepartamento Departamentos {
            get
            {
                if(_departamentos == null)
                {
                    _departamentos = new DepartamentoRepository(_context);
                }
                return _departamentos;
            }
        }
        public IPais Paises {
            get
            {
                if(_paises == null)
                {
                    _paises = new PaisRepository(_context);
                }
                return _paises;
            }
        }
        public IInsumo Insumos {
            get
            {
                if(_insumos == null)
                {
                    _insumos = new InsumoRepository(_context);
                }
                return _insumos;
            }
        }
        public IProveedor Proveedores {
            get
            {
                if(_proveedores == null)
                {
                    _proveedores = new ProveedorRepository(_context);
                }
                return _proveedores;
            }
        }
        public ITipoPersona TipoPersonas {
            get
            {
                if(_tipoPersonas == null)
                {
                    _tipoPersonas = new TipoPersonaRepository(_context);
                }
                return _tipoPersonas;
            }
        }
        public ICliente Clientes {
            get
            {
                if(_clientes == null)
                {
                    _clientes = new ClienteRepository(_context);
                }
                return _clientes;
            }
        }            
        
        public IVenta Ventas {
            get
            {
                if(_ventas == null)
                {
                    _ventas = new VentaRepository(_context);
                }
                return _ventas;
            }
        }
        
        public IDetalleOrden DetalleOrdenes {get
            {
                if(_detalleOrdenes == null)
                {
                    _detalleOrdenes = new DetalleOrdenRepository(_context);
                }
                return _detalleOrdenes;
            }
        }
        public IColor Colores {
            get
            {
                if(_colores == null)
                {
                    _colores = new ColorRepository(_context);
                }
                return _colores;
            }
        }
        public IOrden Ordenes {
            get
            {
                if(_ordenes == null)
                {
                    _ordenes = new OrdenRepository(_context);
                }
                return _ordenes;
            }
        }
        public IDetalleVenta DetalleVentas {
            get
            {
                if(_detalleVentas == null)
                {
                    _detalleVentas = new DetalleVentaRepository(_context);
                }
                return _detalleVentas;
            }
        }
        public IEmpleado Empleados {
            get
            {
                if(_empleados == null)
                {
                    _empleados = new EmpleadoRepository(_context);
                }
                return _empleados;
            }
        }
        public ICargo Cargos {
            get
            {
                if(_cargos == null)
                {
                    _cargos = new CargoRepository(_context);
                }
                return _cargos;
            }
        }
        public ITalla Tallas {
            get
            {
                if(_tallas == null)
                {
                    _tallas = new TallaRepository(_context);
                }
                return _tallas;
            }
        }
        public IInventario Inventarios {
            get
            {
                if(_inventarios == null)
                {
                    _inventarios = new InventarioRepository(_context);
                }
                return _inventarios;
            }
        }
        public IFormaPago FormaPagos {
            get
            {
                if(_formaPagos == null)
                {
                    _formaPagos = new FormaPagoRepository(_context);
                }
                return _formaPagos;
            }
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}