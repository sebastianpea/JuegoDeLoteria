# 🎴 Lotería en Red

Juego de Lotería mexicana multijugador en tiempo real, construido con **C# / Windows Forms** en el cliente y **ASP.NET Core + SignalR** en el servidor.

---

## 📋 Descripción

Lotería en Red es una implementación digital del clásico juego de mesa mexicano. Los jugadores se conectan a una sala mediante un código, arman su propio tablero eligiendo sus cartas, y compiten en tiempo real mientras el servidor va "cantando" las cartas del mazo. El primero en completar el patrón de victoria gana.

---

## ✨ Características

- **Multijugador en red local** — hasta N jugadores en la misma sala mediante SignalR
- **Tablero personalizable** — arrastra y suelta las 54 cartas clásicas para armar tu tablero, o usa el botón aleatorio
- **Guardar/cargar tableros** — exporta e importa tu tablero favorito en formato JSON
- **Múltiples tableros por jugador** — juega con 1 a 4 tableros simultáneamente
- **9 formas de ganar** configurables por el host:
  - Tablero completo, cualquier fila, cualquier columna, cualquier diagonal
  - Cuatro esquinas, cuatro en el centro, forma de X, forma de L
  - Cualquier fila/columna/diagonal
- **Modo manual** — el host controla cuándo se canta cada carta
- **Modo automático** — las cartas se cantan solas con intervalo configurable (1–30 segundos)
- **Pausa y control de velocidad** en tiempo real (solo host)
- **Sistema de desempate automático** cuando varios jugadores cantan Lotería al mismo tiempo
- **Puntajes acumulados** entre rondas
- **Chat integrado** en el lobby y durante la partida
- **Audio** — narración de cada carta al estilo Undertale
- **Tutorial interactivo** con diálogo animado al iniciar
- **Cartas dobles** — opción para permitir repetir cartas en el tablero

---

## 🗂️ Estructura del Proyecto

```
Solución
├── Servidor/
│   └── Hubs/
│       ├── HubDeJuego.cs       # Hub principal de SignalR (lógica del servidor)
│       └── Sala.cs             # Estado de cada sala de juego
│
├── Compartido/
│   ├── CartaInfo.cs            # Modelo de carta para comunicación cliente-servidor
│   ├── EventosHub.cs           # Constantes de eventos SignalR
│   └── MazoCompartido.cs       # Mazo de 54 cartas para el servidor
│
└── Cliente (JuegoDeLoteria)/
    ├── Forms/
    │   └── MainForm.cs         # Formulario principal, maneja navegación entre pantallas
    ├── Controles/
    │   ├── MenuControl         # Pantalla de inicio
    │   ├── DialogoControl      # Tutorial con diálogo animado
    │   ├── UnirseControl       # Pantalla para conectarse a una sala
    │   ├── LobbyControl        # Sala de espera y configuración
    │   ├── SeleccionTableroControl  # Armar el tablero antes de jugar
    │   ├── JuegoControl        # Pantalla principal durante la partida
    │   ├── PostJuegoControl    # Resultados y cartas no llamadas
    │   ├── TableroControl      # Componente visual de un tablero individual
    │   ├── ChatControl         # Chat en tiempo real
    │   └── ConfiguracionControl # Ajuste de volumen
    ├── Juego/
    │   ├── Carta.cs            # Modelo de carta con imagen
    │   ├── MazoDeCartas.cs     # Mazo con las 54 cartas clásicas
    │   ├── Tablero.cs          # Lógica de marcado y verificación de victoria
    │   └── FormasdeGanar.cs    # Enum con los 9 patrones de victoria
    └── Redes/
        └── ClienteDeJuego.cs   # Cliente SignalR, gestiona conexión y eventos
```

---

## 🚀 Requisitos

| Componente | Versión mínima |
|---|---|
| .NET | 8.0 |
| Windows | 10 / 11 (cliente WinForms) |
| ASP.NET Core | 8.0 (servidor) |
| SignalR | incluido en ASP.NET Core 8 |

---

## ⚙️ Instalación y Ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/loteria-en-red.git
cd loteria-en-red
```

### 2. Ejecutar el servidor

```bash
cd Servidor
dotnet run
```

El servidor escucha en `http://0.0.0.0:5000` por defecto.

### 3. Ejecutar el cliente

```bash
cd Cliente
dotnet run
```

O abre la solución en Visual Studio y ejecuta el proyecto cliente.

### 4. Conectarse

1. En la pantalla de inicio, haz clic en **Jugar**
2. Ingresa tu nombre, el código de sala que quieras usar y la IP del servidor
3. El primero en unirse a una sala se convierte en **host**
4. El host configura las opciones y hace clic en **Iniciar Juego**

---

## 🎮 Cómo Jugar

1. **Unirse** — ingresa nombre, código de sala e IP del servidor
2. **Lobby** — espera a que todos estén listos; el host elige forma de ganar, intervalo y modo
3. **Armar tablero** — arrastra las cartas que quieras a tu tablero 4×4 (o usa Aleatorio). Puedes guardar tu tablero para cargarlo en partidas futuras
4. **Jugar** — cuando se cante una carta, haz clic sobre ella en tu tablero para marcarla
5. **¡Lotería!** — cuando creas tener el patrón ganador, presiona el botón **Lotería**. El servidor verifica tu tablero; si es válido, ganas. Si hay empate, se desempata automáticamente

---

## 🏆 Formas de Ganar

| Forma | Descripción |
|---|---|
| `TableroCompleto` | Las 16 cartas marcadas |
| `CualquierFila` | Una fila completa |
| `CualquierColumna` | Una columna completa |
| `CualquierDiagonal` | Una diagonal completa |
| `CualquierFilaColumnaDiagonal` | Cualquiera de las tres anteriores |
| `CuatroEsquinas` | Las 4 esquinas del tablero |
| `CuatroEnElCentro` | El bloque 2×2 central |
| `FormaDeX` | Ambas diagonales simultáneamente |
| `FormaDeL` | Primera columna + última fila (y sus variantes en espejo) |

---

## 🔧 Configuración del Host

Durante el lobby, el host puede ajustar:

- **Forma de ganar** — el patrón que decide quién gana
- **Intervalo** — segundos entre carta y carta (1–30)
- **Cartas dobles** — permite repetir cartas en el tablero
- **Modo manual** — el host controla manualmente cuándo se canta la siguiente carta

Durante la partida (solo host, modo automático):

- **Pausar / Reanudar** la partida
- **Aumentar / Reducir** la velocidad con los botones `+` y `−`

---

## 📡 Arquitectura de Red

El proyecto usa **ASP.NET Core SignalR** para comunicación bidireccional en tiempo real.

```
Cliente A ──┐
Cliente B ──┼──► HubDeJuego (SignalR) ──► Sala (estado en memoria)
Cliente C ──┘
```

Todos los eventos están definidos como constantes en `EventosHub.cs` (proyecto Compartido) para evitar strings sueltos tanto en cliente como servidor.

## 📝 Notas
- El estado de las salas se guarda **en memoria** del servidor; si el servidor se reinicia, las partidas activas se pierden
- Si el host se desconecta, el rol pasa automáticamente al siguiente jugador en la sala
- La verificación de Lotería siempre ocurre en el **cliente** y el resultado se reporta al servidor para validación y desempate
- Los tableros se envían al servidor antes de iniciar para permitir el desempate por carta más reciente
