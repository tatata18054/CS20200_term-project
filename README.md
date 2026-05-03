# F# 2048 CLI

A command-line version of the classic 2048 puzzle game built with **F# / .NET 10**.

Merge tiles with the same number to reach the **2048** tile!

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)  
  Verify with: `dotnet --version` (should show `10.x.x`)

### Run
move to directory first
```bash
# Windows (Command Prompt or PowerShell)
.\run.bat

# Unix / macOS
chmod +x run.sh
./run.sh

# Or directly
dotnet run
```

### Build

```bash
dotnet build
```

---

## How to Play

### Controls

- **W**: Move Up
- **A**: Move Left
- **S**: Move Down
- **D**: Move Right
- **Q**: Quit Game

### Game Rules

1. **Movement**: When you move in a direction, all tiles slide as far as possible.
2. **Merging**: If two tiles of the same number collide while moving, they merge into a single tile with the sum of their values.
3. **Spawning**: After every successful move, a new tile (2 -90% or 4 - 10%) spawns in a random empty spot.
4. **Winning**: Reach the **2048** tile to win!
5. **Game Over**: The game ends when the board is full and no more merges are possible.
---

## Scoring System

- **Earning Points**: When two tiles with the same value merge, your score increases by the value of the new tile. For example, merging two `8` tiles to create a `16` tile adds 16 points to your score.
- **Best Score**: The game automatically tracks your highest score across sessions. 
- **Persistence**: Your high score is saved locally in a `highscore.txt` file in the project root.

---

## Project Structure

```
CS20200_term-project/
├── 2048.fsproj         # .NET 10 F# project file
├── run.bat             # Windows run script
├── run.sh              # Unix run script
├── README.md           # This file
├── requirements.md     # Project requirements
└── Game2048/
    ├── Board.fs        # Core game logic (Grid, movement, merging, spawning)
    ├── Game.fs         # Game state management and UI rendering
    └── Program.fs      # Application entry point
```

### Module Overview

| Module | Responsibility |
|--------|---------------|
| `Board` | Core logic: `Grid` type, tile merging, movements (Up/Down/Left/Right), win/loss detection. |
| `Game`  | Console UI: Rendering the board, handling keyboard input, and maintaining score. |
| `Program` | Entry point: Starts the game loop. |

---

## Implementation Details

- **Language**: F# (Functional-first approach)
- **UI**: Console-based with structured grid rendering using `|` and `_`.
- **Logic**: Immutable-style grid updates where movement functions return a new grid state.
- **Score**: Tracks the total value of merged tiles.


### LLM Used

I have used LLM to write the all the above README.md file by providing the github link. 
I gave example project(Tic-Tac-Toe) project to look and write the same style.

Also I used LLM to make the "let render" in Game.fs which makes the gameboard, prints score and other because I couldn't guess a good way to make a board.
First LLM made the board with dots, making 

.2 .2 . .

.4 . . .

. . . .

. . . .

kind of way, but I didn't liked the way that if there is no number the space bewtween dots changes. So, I asked it to make the baord with fixed sized.
Then it made the board as now using | and __.
