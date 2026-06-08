# 🎴 Juego de Lotería Multijugador

Juego de Lotería mexicana multijugador en red local, desarrollado en C# con WinForms y SignalR. Proyecto escolar desarrollado con principios de Clean Code y Programación Orientada a Objetos.

---

## 📋 Requisitos

- Windows 10 o superior
- .NET 8.0 SDK
- Visual Studio 2022
- Paquetes NuGet:
  - `Microsoft.AspNetCore.SignalR` (Servidor)
  - `Microsoft.AspNetCore.SignalR.Client` (Cliente)
  - `NAudio` (Cliente)

---

## 🏗️ Estructura del Proyecto

La solución está compuesta por tres proyectos:

```
JuegoDeLoteria.sln
├── Compartido/              → Biblioteca de clases compartida
│   ├── CartaInfo.cs         → Información básica de carta (Id, Nombre)
│   ├── MazoCompartido.cs    → Mazo del servidor con las 54 cartas
│   └── EventosHub.cs        → Constantes de eventos SignalR
│
├── Servidor/                → Aplicación ASP.NET Core
│   ├── Hubs/
│   │   ├── HubDeJuego.cs    → Hub SignalR con toda la lógica del servidor
│   │   └── Sala.cs          → Estado de cada sala de juego
│   └── Program.cs           → Configuración del servidor en puerto 5000
│
└── JuegoDeLoteria/          → Aplicación WinForms (cliente)
    ├── Controles/           → UserControls (pantallas del juego)
    │   ├── MenuControl
    │   ├── ConfiguracionControl
    │   ├── DialogControl
    │   ├── UnirseControl
    │   ├── LobbyControl
    │   ├── SeleccionTableroControl
    │   ├── JuegoControl
    │   ├── PostJuegoControl
    │   ├── TableroControl
    │   └── ChatControl
    ├── Forms/
    │   └── MainForm.cs      → Ventana principal (contenedor de controles)
    ├── Juego/
    │   ├── Carta.cs         → Carta con imagen embebida
    │   ├── MazoDeCartas.cs  → Mazo completo del cliente
    │   ├── Tablero.cs       → Tablero 4x4 con lógica de victoria
    │   └── FormasdeGanar.cs → Enum con 9 condiciones de victoria
    ├── Managers/
    │   └── AudioManager.cs  → Manejo de música con playlist
    └── Redes/
        └── ClienteDeJuego.cs → Cliente SignalR con eventos y acciones
```

---

## 🎮 Flujo del Juego

```
Menú Principal
    ↓
Diálogo Tutorial (¿Conoces la Lotería?)
    ↓
Pantalla de Unirse (Nombre, Código de sala, IP del servidor)
    ↓
Lobby (Lista de jugadores, configuración del host)
    ↓
Selección de Tablero (Aleatorio o manual con drag & drop)
    ↓
Juego (Cartas llamadas, fichas, chat, ¡Lotería!)
    ↓
Post Juego (Ganador + cartas que no se llamaron)
```

---

## 🌐 Configuración de Red

El juego funciona en red local (hotspot o WiFi compartido):

1. Una PC ejecuta el proyecto **Servidor**
2. Esa PC abre `cmd` y ejecuta `ipconfig` para obtener su IP (ej. `192.168.1.5`)
3. Todos los jugadores se conectan a la misma red
4. Cada jugador ejecuta **JuegoDeLoteria** e ingresa la IP del servidor
5. La PC servidor puede jugar usando `localhost` como IP

---

## 🚀 Cómo Ejecutar

### Opción 1 — Visual Studio
1. Abrir `JuegoDeLoteria.sln`
2. Click derecho en la Solución → Propiedades → Proyecto de inicio múltiple
3. Establecer `Servidor` y `JuegoDeLoteria` en **Iniciar**
4. Presionar **F5**

### Opción 2 — Dos instancias para pruebas
1. Ejecutar `Servidor` desde Visual Studio
2. Navegar a `JuegoDeLoteria/bin/Debug/net8.0-windows/`
3. Abrir `JuegoDeLoteria.exe` dos veces
4. Usar `localhost` como IP en ambas instancias

---

## 🏆 Condiciones de Victoria

| Condición | Descripción |
|---|---|
| Tablero Completo | Las 16 casillas marcadas |
| Cualquier Fila | Una fila completa |
| Cualquier Columna | Una columna completa |
| Cualquier Diagonal | Una diagonal completa |
| Fila, Columna o Diagonal | Cualquiera de las tres |
| Cuatro Esquinas | Las 4 esquinas del tablero |
| Cuatro en el Centro | El cuadro 2x2 del centro |
| Forma de X | Las dos diagonales completas |
| Forma de L | Cualquier rotación de L |

---

## 🃏 Mecánicas del Juego

### Tablero
- Cada jugador puede tener de 1 a 4 tableros simultáneos
- Los tableros se seleccionan antes de cada partida
- Se pueden elegir aleatoriamente o de forma manual con drag & drop
- Las cartas llamadas se resaltan automáticamente en el tablero

### Fichas
- Se arrastran desde el panel de fichas hacia las casillas del tablero
- Al colocar una ficha, la imagen cambia a la versión con ficha
- Se puede quitar una ficha haciendo click sobre ella

### Lotería
- El jugador presiona **¡Lotería!** cuando cree tener la condición de victoria
- El servidor envía las cartas mencionadas al cliente
- El cliente verifica localmente si la condición es válida
- Si es válida, se notifica a todos los jugadores
- Si no es válida, el jugador puede seguir jugando

### Chat
- Disponible durante el juego y en el lobby
- Los mensajes se muestran con el nombre del jugador
- Se puede enviar con el botón **Enviar** o presionando **Enter**

### Post Juego
- Muestra el nombre del ganador
- Muestra todas las cartas que no fueron llamadas durante la partida
- El host puede iniciar una nueva partida o salir

---

## 🔌 Arquitectura de Red (SignalR)

### Servidor → Cliente
| Evento | Descripción |
|---|---|
| `JugadorUnido` | Nuevo jugador en la sala |
| `JugadorSalio` | Jugador desconectado |
| `NuevoHost` | El host cambió |
| `JuegoIniciado` | El host inició la partida |
| `ActualizarListos` | Cuántos jugadores están listos |
| `ConteoIniciado` | Todos listos, empiezan las cartas |
| `CartaMencionada` | Nueva carta llamada |
| `VerificarLoteria` | Lista de cartas para verificar |
| `JuegoTerminado` | Partida terminada con ganador |
| `CartasRestantes` | Cartas no llamadas al final |
| `MensajeRecibido` | Mensaje de chat |

### Cliente → Servidor
| Método | Descripción |
|---|---|
| `UnirseASala` | Unirse o crear sala |
| `IniciarJuego` | Host inicia la partida |
| `JugadorListo` | Jugador terminó de elegir tablero |
| `ReclamarLoteria` | Jugador reclama victoria |
| `ResultadoLoteria` | Resultado de la verificación |
| `EnviarMensaje` | Mensaje de chat |
| `JugarDeNuevo` | Host reinicia la partida |
| `ObtenerCartasRestantes` | Solicitar cartas no llamadas |

---

## 🛠️ Principios Aplicados

### Clean Code
- **Single Responsibility** — cada clase tiene una sola responsabilidad
- **Open/Closed** — nuevas condiciones de victoria se agregan sin modificar existentes
- **DRY** — código compartido en el proyecto `Compartido`
- **Nombres significativos** — clases, métodos y variables en español descriptivo
- **Manejo de excepciones específicas** — se capturan `HttpRequestException` y `TaskCanceledException` en lugar de `Exception` general

### Programación Orientada a Objetos
- **Encapsulación** — propiedades con `private set`, campos privados
- **Abstracción** — `ClienteDeJuego` oculta la complejidad de SignalR
- **Separación de capas** — servidor, cliente y compartido completamente separados
- **Eventos** — comunicación entre capas sin acoplamiento directo
- **Constantes centralizadas** — `EventosHub` evita magic strings

---

## 🎵 Audio

- La música se reproduce en loop automáticamente
- Soporta playlist con múltiples canciones MP3
- El volumen se controla desde **Configuración**
- Los archivos de música van en la carpeta `Musica/` junto al ejecutable
