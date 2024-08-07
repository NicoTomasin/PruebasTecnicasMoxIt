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

Luego de las primeras pruebas, me parecía que la dificultad no era la adecuada y recordé el concepto de [Triada](https://piktochart.com/es/blog/como-elegir-la-paleta-de-colores-parte-ii-herramientas-para-combinar-colores/), [Complementarios](https://es.wikipedia.org/wiki/Colores_complementarios) y [Armonía de tonos](https://educacionplasticayvisual.com/color/armonias-de-color/).
Tomando esto como referencia, Decidí usar el espacio de color HSL (H = Tono, S = Saturación y L = Luminosidad).
Esto me ayudo por ejemplo a crear un color random en HSL `randomHslColorGenerator` y en base a el, para el primer nivel agregarle 120° al tono, 120° nace de los ángulos internos del triangulo, esto se puede ver en `generateTriadColors`.
Similar es el caso para `generateComplementaryColors` en el que genero 3 colores complementarios al primero (random), agregándole 90° a cada tono es decir que cada color va a estar a 0°, 90°, 180° y 270°.
Finalmente para el nivel difícil decidí de manera arbitraria en la función `generateToneHarmonies`, que por cada color que quería generar `i < 5`, se genere diferente luminosidad, `const newL = Math.max(0, Math.min(100, l + (i - 2) * 10));` me aseguro que se generen valores mayores a 0 y menores a 100, ya que en hsl, luminosidad acepta como máximo 100% y ademas al tener `l + (i - 2) * 10)` para asegurarse de generar valores para L inferiores y mayores al original.

Todo este flujo cierra cuando `generateDivs` crear la paleta que se mostrara en el HTML de manera aleatoria para que la respuesta correcta no sea siempre el primer item. Ademas a partir de este flujo y debido al requerimiento de mostrar el código HEX para el usuario. Le solicite a [Claude](https://claude.ai/) que cree la función `hslToHex` ya que no logre comprender como hacer la equivalencia entre un espacio y el otro.

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
