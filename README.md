# Tuya

El proyecto fue realizado en la versión .Net 8

Para iniciar el proyecto
1. En el archivo appsettings.json está la conexión a la Base de datos
2. Usar el comando "Update-Database" en la Consola de Administrador de paquetes para que se ejecute la migration
3. En la DbContext (AppDbContext) se cargan algunos datos iniciales en las entidades (Customer, Product)
4. El proyecto Tuya.Api es el proyecto de inicio.
5. Se uso swagger para realizar las pruebas
6. Dejo un JSON de ejemplo para crear una Orden
{
  "idCustomer": 2,
  "address": "Carrera 50",
  "total": 62000,
  "orderDetails": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 25000,
      "totalPrice": 50000
    },
    {
      "productId": 2,
      "quantity": 2,
      "unitPrice": 6000,
      "totalPrice": 12000
    }
  ]
}