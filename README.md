
# Netby TODO - Evaluación Técnica .NET (API + Razor Pages)

Este repositorio contiene una **aplicación fullstack .NET** para la gestión de tareas pendientes, desarrollada como parte de una evaluación técnica.  
El proyecto está compuesto por:
- **Backend:** API RESTful en ASP.NET Core con autenticación JWT.
- **Frontend:** Razor Pages que consume la API.

---

## 🏗️ **Requisitos previos**

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, SQL Express, o una instancia en la nube/local)
- Visual Studio 2022+ o VS Code
- Git

---

## 📦 **Estructura del repositorio**
![image](https://github.com/user-attachments/assets/17f67543-9b75-4f9b-91d2-2d9aeff62760)
---

## ⚡ **Primeros pasos**

### 1. Clona el repositorio

```bash
git clone https://github.com/ajunquit/Netby.Todo.git
cd tu_repositorio
```

### 2. Restaura los paquetes NuGet

```bash
dotnet restore
```

---

## 🗄️ **Configuración del Backend (API)**

1. Abre el archivo `appsettings.json` en `Netby.Todo.API`.
2. Configura la cadena de conexión SQL Server:
   ```json
   "ConnectionStrings": {
     "Local": "Server=localhost;Database=NetbyTodoDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
3. Configura el secreto JWT (puedes cambiarlo):
   ```json
   "Jwt": {
     "Key": "UnaClaveSuperSecretaYSeguraDe32Caracteres1234",
     "Issuer": "NetbyIssuer",
     "Audience": "NetbyAudience"
   }
   ```

### 3. Ejecuta migraciones y crea la base de datos

```bash
dotnet ef database update -p src/web-api/infrastructure/Netby.Todo.Persistence -s src/web-api/presentation/Netby.Todo.API
```

---

## 🚀 **Ejecutar el Backend**

Desde la carpeta del proyecto API:

```bash
cd src/web-api/presentation/Netby.Todo.API
dotnet run
```
- La API por defecto corre en: **https://localhost:7077/**
- Accede a Swagger UI en: **https://localhost:7077/swagger**

---

## 🖥️ **Configuración y ejecución del Frontend (Razor Pages)**

1. Ve a la carpeta del proyecto:
   ```bash
   cd src/web-api/site/Netby.Todo.Site
   ```

2. En `appsettings.json` o en el registro del `HttpClient`, verifica que la **base address** apunte a tu API:
   ```csharp
   builder.Services.AddHttpClient("TodoApi", client =>
   {
       client.BaseAddress = new Uri("https://localhost:7077/api/");
   });
   ```

3. Ejecuta el frontend:

   ```bash
   dotnet run
   ```

4. Abre tu navegador en: **https://localhost:7078/**  
   *(puerto puede variar, revisa la consola al levantar la app)*

---

## 🔐 **Usuarios y autenticación**

- **Para usar la app debes registrarte como usuario** usando la ruta `/api/Auth/register` (desde Swagger UI).
- Luego haz login en el frontend con ese usuario.
- El token JWT se maneja automáticamente para consumir la API desde el frontend.

---

## 🧪 **Pruebas Unitarias**

Para correr los tests (NUnit + Moq):

```bash
cd Netby.Todo.Application.Tests
dotnet test
```

---

## 📸 **Capturas**
API
![image](https://github.com/user-attachments/assets/df927f94-471c-4664-aa3c-014f26548f66)

Web
![image](https://github.com/user-attachments/assets/6a7de2c7-5f68-4869-bb3d-374b2b62f5fe)

Task
![image](https://github.com/user-attachments/assets/5986adeb-8e5c-4e81-b0a7-561157262d8e)

Create/Edit Task
![image](https://github.com/user-attachments/assets/cf1ea3b9-cfe4-410a-aaad-87e8b8df3bde)
![image](https://github.com/user-attachments/assets/a4ea9fab-6497-4d49-b71c-ffb35b737e73)


Delete Task
![image](https://github.com/user-attachments/assets/529edb5e-eede-4925-9e67-d51d84a87b87)

Database
![image](https://github.com/user-attachments/assets/7678a11e-3305-4309-a623-b89fad115e30)
![image](https://github.com/user-attachments/assets/25f73c4e-a300-4207-9fc2-b1a39813772f)

---

## 📝 **Configuración de Logging (Serilog)**

- Serilog está configurado en ambos proyectos (`Netby.Todo.API` y `Netby.Todo.Site`) a través de `appsettings.json`.
- Los logs se escriben en consola y en archivos diarios.
- Puedes ajustar el nivel de logueo y sinks editando la sección `"Serilog"` en el archivo de configuración.

**Ejemplo de configuración en `appsettings.json`:**

```json
"Serilog": {
  "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
  "MinimumLevel": {
    "Default": "Information",
    "Override": {
      "Microsoft": "Warning",
      "System": "Warning"
    }
  },
  "WriteTo": [
    { "Name": "Console" },
    {
      "Name": "File",
      "Args": {
        "path": "logs/log-.txt",
        "rollingInterval": "Day"
      }
    }
  ],
  "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
  "Properties": {
    "Application": "NetbyTodo"
  }
}
```

---

## 🛠️ **Notas adicionales**

- Puedes cambiar el puerto de la API o del frontend desde `launchSettings.json` de cada proyecto.
- Si usas SQL Server Express o una instancia diferente, ajusta la cadena de conexión.
- Asegúrate de tener configurado el certificado de desarrollo para HTTPS en .NET.

---

## 📞 **Soporte**

Para cualquier duda, contacta a: [ajunquit@gmail.com]

---

¡Listo! Ahora puedes levantar y probar la solución 🚀
