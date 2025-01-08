# Prueba 3 BackEnd

Este proyecto es la 3 prueba del curso de introdución al desarrollo web/móvil 
Universidad Católica del Norte. Desarrollado con **ASP.NET Core 8**

# Requisitos Previos

Antes de ejecutar este proyecto, asegúrate de contar con los siguientes elementos:

- **.NET SDK 8.0**: [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** o cualquier base de datos compatible con Entity Framework Core.
- **Postman** (opcional, para probar los endpoints).
- Una cuenta en **Cloudinary** para la gestión de imágenes.

## Instalación

1. Clona este repositorio:
   ```bash
   https://github.com/bemoremore/Prueba3IDWM.git

2. Restaura las dependencias del proyecto:
    ```bash
    dotnet restore
    
3. Aplica las migraciones para configurar la base de datos:

    dotnet ef database update
    
## Build

- **Inicia el servidor de desarrollo:**
    ```bash
    dotnet run

