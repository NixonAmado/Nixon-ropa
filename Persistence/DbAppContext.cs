using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class DbAppContext: DbContext
{
    public DbAppContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Prenda> Prendas {get;set;}
    public DbSet<TipoProteccion> TipoProtecciones {get;set;}
    public DbSet<Genero> Generos {get;set;}
    public DbSet<Estado> Estados {get;set;}
    public DbSet<TipoEstado> TipoEstados {get;set;}
    public DbSet<Empresa> Empresas {get;set;}
    public DbSet<Municipio> Municipios {get;set;}
    public DbSet<Departamento> Departamentos {get;set;}
    public DbSet<Pais> Paises {get;set;}
    public DbSet<Insumo> Insumos {get;set;}
    public DbSet<Proveedor> Proveedores {get;set;}
    public DbSet<TipoPersona> TipoPersonas {get;set;}
    public DbSet<Cliente> Clientes {get;set;}
    public DbSet<Venta> Ventas {get;set;}
    public DbSet<DetalleOrden> DetalleOrdenes {get;set;}
    public DbSet<Color> Colores {get;set;}
    public DbSet<Orden> Ordenes {get;set;}
    public DbSet<DetalleVenta> DetalleVentas {get;set;}
    public DbSet<Empleado> Empleados {get;set;}
    public DbSet<Cargo> Cargos {get;set;}
    public DbSet<Talla> Tallas {get;set;}
    public DbSet<Inventario> Inventarios {get;set;}
    public DbSet<FormaPago> FormaPagos {get;set;}
    public DbSet<InventarioTalla> InventariosTallas {get;set;}
    public DbSet<InsumoProveedor> InsumosProveedores {get;set;}
    public DbSet<InsumoPrenda> InsumosPrendas {get;set;}
    
    //JWT    
    public DbSet<Role> Roles{get;set;}
    public DbSet<User> Users {get;set;}
    public DbSet<UserRole> UsersRoles {get;set;}


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var roles = new[]
        {
            new Role { Id = 1, Description = "Administrador" },
            new Role { Id = 2, Description = "Empleado" },
        };
        var cargos = new[]
        {
            new Cargo{ Id = 1, Descripcion = "Auxiliar de bodega", SueldoBase = 1400000 },
            new Cargo{ Id = 2, Descripcion = "Jefe de Produccion", SueldoBase = 2000000 },
            new Cargo{ Id = 3, Descripcion = "Secretaria", SueldoBase = 1500000 },
            new Cargo{ Id = 4, Descripcion = "Jefe de TI", SueldoBase = 2500000 },
        };

        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.Entity<Cargo>().HasData(cargos);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}