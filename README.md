# Prueba tecnica Juan Manuel Carreño 

Para dar solucion a la prueba tecnica se uso .NetCore 10.0.202 para el backend y para el Frontend se uso Angular 21.2.8 

en el back se usó la siguiente cadena de conexion para acceder al servicio SQL Server 2025 Dev Edition 

``` code
DefaultConnection": "Server=localhost;Database=biblioteca;User Id=sa;Password=*a123456;TrustServerCertificate=True;
```


seteada en el appsetting.json en el backend.

Se realizaron diversas migraciones para modificar campos y el de la tarea de creacion de la tabla loans que siguiere el ejercicio, los cuales aun se encuentran dentro de la estructura de directorios compartidos en este git. Uno de los ejemplos de esto es lo siguiente 

``` bash
λ dotnet ef migrations add addLoanTable -p src\Infrastructure\ -s src\Api\Biblioteca.Api\
```

Se ejecuta el comando anterior una vez se realizaran los ajustes en el codigo de las etities y/o los mapeos de las mismas.

Al ejecutar el backend por defecto correra todas las migraciones pendientes sobre el SQL Server, y su pagina por defecto será el Swagger ubicado en la url [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

El proyecto se estructuro aplicando conceptos de clean code dentro de lo posible del tiempo, teniendo en cuenta que ha sido un ejercicio en el que he podido retomar las tecnologias .Net y Angular que hacia varios años no las usaba y el tiepo estuvo bastante ajustado para realizar el codigo y documentarme de los cambios y mejoras en estas tecnologias.

Para NetCore, se crearon 4 Capas destrotas a continuacion
* capa de Aplicacion
* capa de Infraestructura
* capa de dominio
* capa de presentacion

En donde se implementan los distintos objetos para ser debidamente encapsulados y presentados.
Para correr el backend se utilizo el IDE de .Net Community Edition y se expone en el puerto 5000

Para Angular, se utiliza la plantilla CoreUI para agilizar la implementacion del entorno grafico , se crean los Componentes y servicios necesarios dentro de su estructura de directorios por defecto ("app"), tiene muchas oportunidades de mejora al igual que el backend, sin embargo, y como lo mencione anteriormente, fue una excelente oportunidad para actualizar mis conocimientos.

Correr el frontend tampoco tiene gran percance, se expone en el puerto 4200 y se ejecuta con el siguiente comando desde su carpeta principal 

``` bash
λ ng serve
```




