# Prueba técnica : Vuelos

## Tecnologías usadas:

- ASP NET MVC
- RAZOR + BOOTSTRAP
- ENTITY FRAMEWORK 
- SQLITE

## Notas de desarrollo:

El desarrollo se llevo a cabo utilizando el patron MVC y ASP NET CORE.
Además para la creación de la base de datos use la metodología code first de EF y SQLite para agilizar el desarrollo y facilitar la corrección del ejercicio.
Este ejercicio me sirvió mucho para profundizar en el uso de viewBag, viewModel y los asp net tags helpers ya que el pasaje de datos en este tipo de proyectos no es algo que tenga refinado.



## Mejoras o cosas que haría diferentes

- Separación en capas: Actualmente esta dividido en carpetas pero la manera mas limpia de hacerlo seria creado una capa extra para el repository/service y otra para la base de datos y EF.
- Experimentar con asp + react: Nunca trabaje con estas dos tecnologías en conjunto y siento que es un buen proyecto para hacerlo.
- Definitivamente mejoraría la UI: No me llevo tan bien con bootstrap, pero me gastaría revertir esa relación.


## Algunas aclaraciones extras

Sin bien el enunciado no especificaba nada, decidí hacer la mayor cantidad de código en ingles para seguir un estándar y posibilitar mayor mantenimiento a futuro.
Además seguí las nomenclaturas propuestas por net tanto en controladores, vistas y modelos como el uso de `PascalCase` para variables y funciones. 
