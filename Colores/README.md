# Prueba técnica : Colores

## Tecnologías usadas:

- CSS
- HTML
- JS

Decidí mantener el proyecto lo mas simple posible, sin usar ningún servidor como Nodejs ni empaquetar el codigo. Esto me perjudica en la legibilidad del js pero se ve una clara ventaja en el peso y velocidad del proyecto.

## Notas de desarrollo:

El enunciado requiere crear una paleta de colores random y mostrarle el HEX al jugador para que intente adivinar cual es. En una primera instancia había generado un color random para cada nivel pero tomando arbitrariamente un valor fijo para R G B según el nivel. Por ejemplo:

En el nivel 1 `#AAAA[random]`

En el nivel 2 `#AA[random][random]`

En el nivel 3 `#[random][random][random]`

Luego de las primeras pruebas, me parecía que la dificultad no era la adecuada y recordé el concepto de [Triada](https://piktochart.com/es/blog/como-elegir-la-paleta-de-colores-parte-ii-herramientas-para-combinar-colores/), [Complementarios](https://es.wikipedia.org/wiki/Colores_complementarios) y [Armonía de tonos](https://educacionplasticayvisual.com/color/armonias-de-color/), en base a eso comence a trabajar con el espacio de color HSL (H = Tono, S = Saturación y L = Luminosidad).

A nivel codigo, uso la funcion `randomHslColorGenerator` que genera un nuevo color en formato HSL de manera aleatoria que luego sera la base para construir cada nivel.

Para el nivel uno y usando `generateTriadColors`, se le agrega 120° a la H (Tono) del color base obteniendo asi una triada.

Para el nivel dos y usando `generateComplementaryColors`, agrego 90° lo que nos da un cuadrado complementario (4 colores).

Finalmente en el nivel difícil con `generateToneHarmonies`, modifico L (Luminosidad) de a 10% para generar colores muy similares al original.

Todo este flujo cierra cuando `generateDivs` genera la paleta de colores ordenada al azar y uso `hslToHex` para poder mostrar el hex al usuario final.

Luego para lograr un ejercicio mas completo, agregue un contador haciendo que la finalidad del juego sea mantener la racha, un botón para volver al inicio y uno para reiniciar en caso de perder.

## ToDo

- [ ] Usaría Node con Webpack o Babel para poder modularizar el código y que sea mas fácil de mantener.
- [ ] Trataría de minimizar el uso de 'Magic Numbers' en la creación de colores.
- [ ] Optimizaría los algoritmos para dejar de ser arbitrarios y permitan configurar mas niveles de dificultad solo agregándolo al array de `levels`
- [ ] Recortaría el rango de luminosidad disponible para random color, ya que existe la posibilidad de que toque un color base muy oscuro o muy claro y la variación de tonalidad no sea perceptible debido a la baja luminosidad.
- [ ] Agregaría un Score Board Global usando SQLite y colocando tres letras como los viejos arcades.
- [ ] Renderizaria los colores en un canvas o trataría de generarlos en una imagen para evitar que puedan obtener el color al inspeccionar el código.
- [ ] Crearía un eventListener al click derecho para que anule o se pierda automáticamente para evitar trampas.
- [ ] Ofuscaría el código para evitar que puedan deducir que color es el ganador según el eventListener `win`.
- [ ] Crearía una paleta de colores usando variables en css para reutilizar estilos

## Algunas aclaraciones extras

Decidí hacer la mayor cantidad de código en ingles para seguir un estándar y posibilitar mayor mantenimiento a futuro.
Ademas para las clases o IDs del HTML use [Kebab-Case](https://developer.mozilla.org/en-US/docs/Glossary/Kebab_case) ya que suele ser el standard y en el JS use [camelCase](https://developer.mozilla.org/en-US/docs/Glossary/Camel_case) por que es de la manera que estoy acostumbrado a trabajar.
Por ultimo decidí crear funciones por fuera del eventListener y no dentro como arrowFunctions para estar "Mas cercano" a la modularidad y legibilidad que provee tener el código separado en diferentes módulos. Ademas permito que las funciones se inicialicen una sola vez y estén disponibles para cualquier evento que se quiera agregar a futuro
