# ModelProject (Versão em português [aqui](README.md))

He creado este proyecto como una pequeña demostración de mi codificación.
Es una forma de demostrar tanto mi estilo como parte de mis conocimientos de forma práctica.

Creé una versión inicial utilizando sólo un proyecto con una arquitectura sencilla. Si lo deseas, puedes verlo en el branch "ProjetoSimples".
La versión actual poseé una mejor división en capas, que aunque sea un overengineering, representa mejor un sistema corporativo real.
Como mejoras futuras, tengo la pretensión de implementar aún pruebas de unidad e interfaz, más funcionalidades y uso de otras tecnologías.

## Desarrollo actual

La versión que se encuentra actualmente en Master fue una versión que he creado de forma simplista: todo en un único proyecto, pocas clases, haciendo honor al nombre. Actualmente, estoy desarrollando una nueva versión, con separación en capas, siguiendo el DDD, como si fuera para un proyecto más grande. La versión todavía no está lista, pero si deseas consultar el estado, vea el Branch SeparandoEmProjetos.

## SPA (Angular) & MPA (Asp.net MVC)

El ModelProject actualmente poseé dos diferentes formas de implementación front-end similares: Asp.net MVC y Angular.
Puede ser que un proyeto tenga funcionalidade que el otro todavía no posee. Estoy actualizando y evolucionando ambos a los pocos.

No olvides de hacer un npm install en el proyecto Angular para instalar todas las dependencias, ni de hacer un Update-Database en el Package Manager Console para crear el Banco de Datos.

## Tecnologías implementadas

El sistema fue construido en ASP.NET CORE 2.1, utilizando C#, y en Angular.
Sigue abajo un resumen de las principales tecnologías utilizadas:

* ASP.NET CORE MVC 2
* Angular 5
* C#
* Identity
* Entity Framework
* Bootstrap
* Sass

## Funcionalidades

En esta primera versión, me enfocé en demostrar algunas funcionalidades. Los siguientes son:

* Diseño responsivo
* Registro de usuarios
* Autenticación
* Validación de campos
* Internacionalización

## Instalação

Para ejecutar la aplicación, es necesário antes generar la Base de Datos. Para eso, abra la "Consola de administrador de paquetes" y ejecute los siguientes pasos:
1. Seleccione el proyecto Vini.ModelProject.Infra.Data como proyecto predeterminado
2. Ejecute el comando: Update-Database -Context ModelProjectContext
3. Seleccione el proyecto Vini.ModelProject.Infra.CrossCutting.Identity como proyecto predeterminado
4. Ejecute el comando: Update-Database -Context IdentityDbContext

Para ejecutar el proyecto Angular, es necesário también instalar todas las dependencias. Para eso, no olvides de ejecutar un "npm install" en la carpeta client. 

### Contacto y mejoras

Para más información, puede enviarme un mensaje o contactarme con [Linkedin](https://www.linkedin.com/in/vinicius-bastos/)

Si tiene alguna sugerencia de mejoras o cosas que sería interesante implementar, no deje de enviarme, me quedan agradecidos.
