// ---- generacion de colores ----
function randomHslColorGenerator() {
  const h = Math.floor(Math.random() * 360);
  const s = Math.floor(Math.random() * 100);
  const l = Math.floor(Math.random() * 100);
  return [h, s, l];
}
function generateTriadColors(hslColor) {
  // destructuro el array hslColor
  const [h, s, l] = hslColor;
  const triadColors = [];
  for (let i = 0; i < 3; i++) {
    //genero un nuevo matiz cada 120 grados (triada)
    const newH = (h + i * 120) % 360;
    triadColors.push([newH, s, l]);
  }
  return triadColors;
}
function generateComplementaryColors(hslColor) {
  const [h, s, l] = hslColor;
  const complementaryColors = [
    hslColor,
    [(h + 180) % 360, s, l],
    [(h + 90) % 360, s, l],
    [(h + 270) % 360, s, l],
  ];
  return complementaryColors;
}
function generateToneHarmonies(hslColor) {
  const [h, s, l] = hslColor;
  const toneHarmonies = [];
  for (let i = 0; i < 5; i++) {
    const newL = Math.max(0, Math.min(100, l + (i - 2) * 10));
    toneHarmonies.push([h, s, newL]);
  }
  return toneHarmonies;
}
function triad() {
  const randomHslColor = randomHslColorGenerator();
  const triadColors = generateTriadColors(randomHslColor);
  generateDivs(triadColors, randomHslColor);
}
function complementary() {
  const randomHslColor = randomHslColorGenerator();
  const complementaryColors = generateComplementaryColors(randomHslColor);
  generateDivs(complementaryColors, randomHslColor);
}
function tones() {
  const randomHslColor = randomHslColorGenerator();
  const toneHarmonies = generateToneHarmonies(randomHslColor);
  generateDivs(toneHarmonies, randomHslColor);
}
function hslToHex(hsl) {
  let [h, s, l] = hsl;
  function componentToHex(c) {
    const hex = c.toString(16);
    return hex.length === 1 ? "0" + hex : hex;
  }
  s /= 100;
  l /= 100;

  let c = (1 - Math.abs(2 * l - 1)) * s;
  let x = c * (1 - Math.abs(((h / 60) % 2) - 1));
  let m = l - c / 2;
  let r = 0,
    g = 0,
    b = 0;

  if (0 <= h && h < 60) {
    r = c;
    g = x;
    b = 0;
  } else if (60 <= h && h < 120) {
    r = x;
    g = c;
    b = 0;
  } else if (120 <= h && h < 180) {
    r = 0;
    g = c;
    b = x;
  } else if (180 <= h && h < 240) {
    r = 0;
    g = x;
    b = c;
  } else if (240 <= h && h < 300) {
    r = x;
    g = 0;
    b = c;
  } else if (300 <= h && h < 360) {
    r = c;
    g = 0;
    b = x;
  }

  // Convertir los valores RGB a valores de 0-255
  r = Math.round((r + m) * 255);
  g = Math.round((g + m) * 255);
  b = Math.round((b + m) * 255);

  // Convertir RGB a HEX
  const hexR = componentToHex(r);
  const hexG = componentToHex(g);
  const hexB = componentToHex(b);

  return `#${hexR}${hexG}${hexB}`;
}
function generateDivs(colors, originalColor) {
  // Mezclar los colores de manera aleatoria
  colors = colors.sort(() => Math.random() - 0.5);

  // Limpiar el contenedor antes de agregar nuevos divs
  const palette = document.getElementById("palette");
  const colorTitle = document.getElementById("game-color");
  colorTitle.textContent = hslToHex(originalColor);
  colors.forEach((color) => {
    const [h, s, l] = color;
    const colorBox = document.createElement("div");
    colorBox.style.backgroundColor = `hsl(${h}, ${s}%, ${l}%)`;
    colorBox.classList.add("color-box");

    if (JSON.stringify(color) === JSON.stringify(originalColor)) {
      colorBox.addEventListener("click", () => win());
    } else {
      colorBox.addEventListener("click", () => loss());
    }

    palette.appendChild(colorBox);
  });
}
// ---- Variables Globales ----
let streak = 0;
let difficultyFlag = 0;

// ---- Referencias a Elementos del DOM ----
const menu = document.getElementById("menu");
const lossBackButton = document.getElementById("loss-back");
const levels = ["easy", "medium", "hard"];
const backButton = document.getElementById("back");
const game = document.getElementById("game");
const palette = document.getElementById("palette");
const counter = document.getElementById("counter-streak");
const lossMessage = document.getElementById("loss-message");
const gameHeader = document.getElementById("game-header");
const lossRetryButton = document.getElementById("loss-retry");

// ---- Funciones de UI ----
function updateCounter() {
  counter.textContent = streak;
}

function showGameUI() {
  menu.style.display = "none";
  game.style.display = "block";
  lossMessage.style.display = "none";
  gameHeader.style.display = "flex";
  palette.style.display = "flex";
}

function showLossUI() {
  gameHeader.style.display = "none";
  palette.style.display = "none";
  lossMessage.style.display = "flex";
}

function showMenuUI() {
  gameHeader.style.display = "flex";
  palette.style.display = "flex";
  lossMessage.style.display = "none";
  menu.style.display = "flex";
  game.style.display = "none";
  palette.innerHTML = "";
}

// ---- Funciones del Juego ----
function win() {
  streak++;
  updateCounter();
  startGame(difficultyFlag);
}

function loss() {
  showLossUI();
  streak = 0;
  updateCounter();
}

function startGame(difficulty) {
  difficultyFlag = difficulty;
  lossRetryButton.removeEventListener("click", retryGame); // Evita múltiples adiciones
  lossRetryButton.addEventListener("click", retryGame);
  showGameUI();
  palette.innerHTML = "";
  const styleOfPalette = [triad, complementary, tones];
  styleOfPalette[difficulty]();
}

function retryGame() {
  startGame(difficultyFlag);
}

function setupLevelButtons() {
  levels.forEach((level, index) => {
    const button = document.createElement("button");
    button.textContent = level.toUpperCase();
    button.id = level.toLowerCase();
    button.addEventListener("click", () => startGame(index));
    menu.appendChild(button);
  });
}

// ---- Inicialización ----
function initialize() {
  setupLevelButtons();
  backButton.addEventListener("click", showMenuUI);
  lossBackButton.addEventListener("click", showMenuUI);
}
document.addEventListener("DOMContentLoaded", initialize);
