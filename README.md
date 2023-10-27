# Filtro J3 - Caso Safe Clothing

La empresa safe clothing desea realizar un backend que le permita llevar el control, registro y seguimiento de la producción de prendas de seguridad industrial, la empresa cuenta con diferentes tipos de prendas entre las cuales están las prendas resistentes al fuego (Ignifugas), resistentes a altos voltajes (Arco eléctrico). La empresa lo contrata a usted como experto backend para que cumpla con los siguientes requerimientos de desarrollo.

- Implementar restricción de peticiones haciendo uso de limitaciones de peticiones por IP.
- Implementar protección a los endPoints haciendo uso de JWT y roles de usuario.
- Implementar esquema de versionado de Api que facilite el proceso de implementación de nuevos endpoints sin afectar el funcionamiento de las aplicaciones externas que consumen los servicios del Api.
- Implementar endpoints que permitan realizar el proceso de CRUD en cada uno de los controladores del backend.
- Debido al gran volumen de información que la empresa procesa diariamente se requiere que los endpoints encargados de consultar el contenido de las tablas implementen sistema de paginación.

# Consultas

## Grupo A

Listar los insumos que pertenecen a una prenda especifica. El usuario debe ingresar el código de la prenda.
```
/API/Insumo/GetAllInsumosByPrenda/{codigoPrenda}
```

Listar los Insumos que son vendidos por un determinado proveedor cuyo tipo de persona sea Persona Jurídica. El usuario debe ingresar el Nit de proveedor.
```
/API/Insumo/GetAllByProvedorTipoPersona/{tipoPersona}/{Nit}
```

Listar todas las ordenes de producción cuyo estado se en proceso.
```
/API/Orden/GetAllOrdenesbyIdCliente/{idCliente}
```

Listar los empleados por un cargo especifico. Los cargos que se encuentran en la empresa son: Auxiliar de Bodega, Jefe de Producción, Corte, Jefe de bodega, Secretaria, Jefe de IT.
```
/API/Empleado/GetAllEmpleadosByCargo/{cargo}
```

Listar las ordenes de producción que pertenecen a un cliente especifico. El usuario debe ingresar el IdCliente y debe obtener la siguiente información:
```
/API/Orden/GetAllOrdenesbyIdCliente/{identificacionCliente}
```
1. IdCliente, Nombre, Municipio donde se encuentra ubicado.
2. Nro de orden de producción, fecha y el estado de la orden de producción Se debe mostrar la descripción del estado, código del estado, valor total de la orden de producción.
3. Detalle de orden: Nombre de la prenda, Código de la prenda, Cantidad, Valor total en pesos y en dólares.


Listar las ventas realizadas por un empleado especifico. El usuario debe ingresar el Id del empleado y mostrar la siguiente información.

1. Id Empleado
2. Nombre del empleado
3. Facturas : Nro Factura, fecha y total de la factura.
```
/API/Venta/GetVentasByIdEmpleado/{idEmpleado}
```

Listar los productos y tallas del inventario. La consulta debe mostrar el id del inventario, nombre del producto, tallas y cantidad de cada talla.
```
/API/Inventario/GetProductoByInv
```

### Uso de Json Web Token
Ya que no se cargan usuarios en la base de datos, es necesesario crearlo. Por defecto el rol de usuario va ser Empleado, el cual puede hacer peticiones a todo el CRUD menos a los enpoints especiales. Cuando se prueben los endpoints es necesario que el usuario tenga el rol de Administrador el cual se le asigna por medio del addrole.

Los datos necesarios para poder hacer post a los endpoints de JWT y en general se encuetran más facilmente en el Swagger, que se incializa por medio de dotnet watch run.

Nota: He tenido inconvenientes con la autorización, ya que, en vez de lanzarme la respuesta 401 o 403, me lanza 404 Not found, pero si le pasamos en token igualmente funcionará.

Si el token caduca, el programa esta diseñado para que por medio de su refresh token pueda generar otro.

Tomamos el refresh token

creamos la cookie y apartir de ese refresh token se generan nuevos tokens
Si el token expira podemos generar cuantos queramos con el mismo refresh token. Si el refresh token expira se debe generar otro token desde el endpoint token.

- Duración del refresh token: 1 hora
- Duración del token de acceso: 1 minuto
Si se presentan inconvenientes a la hora de generar un nuevo token desde el refresh token hay que borrar las cookies.
