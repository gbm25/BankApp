# ¿ Que es BankApp ?

Es un pequeño programa que me fue presentado como prueba en una entrevista de trabajo.

# ¿ Que tecnologías/herramientas usa este proyecto ?

- .Net 6.0
- C#
- WPF (Windows Presentation Foundation)
- SQL server 2019
- T-SQL

#  Requisitos previos para poder probarla 

Este proyecto fue desarrollado usando la versión "Desarrollador" de SQL Server 2019, por lo que no es necesario una versión completa para poder probarlo.

Es posible usar otro tipo de servidor de base de datos SQL, pero se recomienda usar SQL Server 2019 que se puede encontrar [aquí](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) puesto que el script de creación de estructura esta escrito en T-SQL .

Para generar la estructura (base de datos y tablas) del proyecto y datos de relleno, se recomienda usar los scripts T-SQL localizados en la carpeta [Scripts](https://github.com/gbm25/BankApp/tree/master/Scripts).

Es necesario modificar la cadena de conexión en el archivo App.Config, para permitir el acceso a la base de datos.

# ¿ Cual es la estructura de la base de datos ?

## Tablas

La base de datos esta formada por 2 tablas:
* __Customer__ : Contiene la información del cliente.
    * **id**: int NOT NULL Autoincremental Primary Key
    * **first_name**: nvarchar(50) 
    * **last_name**: nvarchar(50) 
    * **username**: nvarchar(50) 
    * **password**: nvarchar(100) 
    * **country**: nvarchar(100) 
    * **region**: nvarchar(100) 
    * **city**: nvarchar(250) 
    * **last_update**: datetime
    
* __Account__ : Contiene la información de las cuentas bancarias. 
    * **id**: int NOT NULL Autoincremental Primary Key
    * **customer_id**: int NOT NULL Foreign key de la columna `id` de la tabla `Account`
    * **account_number**: nvarchar(50) 
    * **description**: nvarchar(max) 

## Diagrama de la base de datos.

![BD Diagram](https://github.com/gbm25/BankApp/tree/master/Docs/images/db_diagram.png "Diagrama de la base de datos")

# Estructura del código

El código se compone de 2 partes diferenciadas.

Las **vistas**, compuestas por los archivos xaml y su correspondientes clases, que componen la GUI y la lógica de como se muestra la información.

Los **modelos**: consta de las clases Customer y BankAccount, representan los datos de una linea de los datos de las tablas `Customer` y `Account` respectivamente. También almacenan la logica necesaria para obtener la información de base de datos.