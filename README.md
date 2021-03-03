## Prueba técnica Homie Backend 

### Prerequisitos

- NetCore 2.1
- Ms SQL 

### Instalación 

1. Descargar NetCore 2.1 de 
2. Instalar NetCore 2.1 
3. Ejecutar el script(CreateScript.sql) de creación de la base de datos de Homie

### Ejecución 

Para iniciar la aplicación ejecutaremos en línea de comandos 

dotnet restore 

Para restaurar todas las dependencias y a continuación 

dotnet run 


Con lo que la aplicación iniciará 

Para ver más fácilmente el funcionamiento si nos dirigimos a http://localhost/documentation podremos encontrar una aplicación para probar la API 


### Funcionamiento 
#### Obtener un token
Primero como partner debemos logearnos con nuestro usuario y contraseña para obtener un token en formato JWT


El script de la base de datos agregó 3 partners de prueba con diferentes niveles de partner

+ Partner basico 
 user: segundamano password: segundamano-12345 

+ Partner 
 user: metroscubicos 
 password: metroscubicos-12345

+ Partner full 
 user: inmuebles24
 password: inmuebles24-12345

#### Consultar las propiedades publicadas

Esto lo realizamos mediante el método GET en el localhost usando nuestro token

Según el tipo de Partner será la información que podremos consultar

#### El CRUD 
en el mismo localhost podemos encontrar nuestro crud mediante los diferentes métodos de http

- GET para obtener una propiedad
- POST para crear una propiedad 
- PUT para actualizar la propiedad
- DELETE para eliminar la propiedad 

