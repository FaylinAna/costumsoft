## Contenido del Proyecto

 CRUD Paquetes:

- Creación de paquetes: Permite crear un paquete al cual se le asigna un tracking number y se establece un estado como nuevo.
- Cambio de estados: Permite cambiar el estado de un paquete a través de diferentes opciones.
- Visualización de detalles: Se pueden ver los detalles de un paquete, incluyendo su estado y otros atributos.
- Búsqueda de paquetes: Se puede buscar un paquete por su número de seguimiento (tracking number).

-simulador de archivos y directorios
    se creo un treeview personalizado que toma la informacion de un json fijo.


## Infraestructura

1. **API en .NET Core 6:** Esta parte del proyecto contiene todas las configuraciones de log y base de datos en el archivo `appsettings.json`. Para generar un token, sigue estos pasos:

    - Consulta y ejecuta la aplicación en el endpoint `/api/Auth/CreateApplication`.
    - La API retornará un secret key que se debera guardar para conectar a los endpoints.
    - generar un token con el nombre de la aplicacion y el secret key generado /api/Auth/GetToken

2. **Frontend en Vue 3:**

## Tecnologías Utilizadas

El proyecto utiliza el siguiente conjunto de tecnologías:

### Backend (.NET Core 6)

- .NET Core 6
- Patrón de diseño CQRS
- Mediator
- Dapper
- Middleware para autenticación, validaciones y logs

### Frontend (Vue 3)

- Vue 3
- Vue Router
- Axios



## Requisitos del Sistema

- .NET Core SDK 6
- Base de datos PostgreSQL
- Node.js y npm (para el frontend)


---

## Configuración

1. **Configuración de la API:**

    - Las configuraciones de log y base de datos se encuentran en el archivo `appsettings.json`.

2. **Configuración del Frontend:**

    - Configura el proyecto Vue 3 para obtener el token de autenticación.

---

## Uso

1. **Ejecutar la API:**

    ```bash
    dotnet run
    ```

2. **Ejecutar el Frontend:**

    ```bash
    npm install
    npm run serve
    ```

---

## Pendientes 


-poder reenombrar archivos y directorios
-problem details exception'
---



## Licencia

Este proyecto está licenciado bajo la [Licencia XYZ](LICENSE.md) - por favor lee el archivo LICENSE.md para más detalles.

---

## Contacto

- Autor/a: Ana Laura Hernandez Elias
- Email: ylexxna14@gmail.com

-> usuario Anatest
-> apikey fe1fe51370654a5a92767ff64ec03838





