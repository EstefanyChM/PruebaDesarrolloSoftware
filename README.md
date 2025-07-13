\# Prueba Desarrollo Software .NET Core



Este proyecto es una \*\*prueba técnica\*\* para el desarrollo de un mantenimiento web de trabajadores.



---



\## 📋 Requerimientos cumplidos



✅ Proyecto creado en MVC .NET Core 8

✅ Conexión a base de datos usando Entity Framework Core 

✅ Listado de trabajadores registrados (usando Stored Procedure).  

✅ Crear trabajador (en modal).  

✅ Actualizar trabajador (en modal).  

✅ Eliminar trabajador (con mensaje de validación).  

✅ Bandeja con filtro por sexo.  

✅ Colorear filas del listado: Azul (Masculino), Naranja (Femenino).  

✅ Estilo con \*\*Bootstrap\*\*.



---



\## 🛠️ Tecnologías usadas



\- ASP.NET Core MVC

\- Entity Framework Core

\- SQL Server

\- Bootstrap



\## 🗃️ Base de Datos



Nombre de la base de datos: `TrabajadoresPrueba`



\### Tablas y datos



Se incluye un script SQL en la carpeta \[`/sql`](./sql) para crear el procedimiento almacenado necesario para el listado de trabajadores.



Archivo incluido:

\- `sql/ListarTrabajadores.sql`: crea el procedimiento almacenado `\[dbo].\[ListarTrabajadores]`.



Asegúrate de ejecutar previamente los scripts de creación de las tablas (`Trabajadores`, `Departamento`, `Provincia`, `Distrito`) con las relaciones necesarias antes de ejecutar el procedimiento almacenado.

