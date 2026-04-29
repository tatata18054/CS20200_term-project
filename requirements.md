# Project Requirements: CLI 2048 Game

## Project Overview
A console-based puzzle game where players move tiles on a 4x4 grid to merge identical numbers, with the ultimate goal of creating a tile with the value **2048**.

## Functional Requirements
1. **Display**: The user must be able to view the 4x4 grid, current Score, and Best Score in the terminal.
2. **Input Controls**: The user can press **W** (Up), **A** (Left), **S** (Down), and **D** (Right) to slide all tiles in the respective direction.
3. **Merging Logic**: When two tiles with the same number collide while sliding, they merge into a single tile with double the value.
4. **Scoring**: Every time tiles merge, the user's score increases by the value of the newly created tile.
5. **Tile Spawning**: After a valid move (one that changes the board state or results in a merge), a new tile (2 or 4) must randomly spawn in an empty cell.
6. **Victory Condition**: The user wins the game when a tile with the value **2048** appears on the grid.
7. **Defeat Condition**: The game ends (Game Over) when the grid is full and no more moves or merges are possible.
8. **Persistence**: The Best Score should be saved and loaded from a local file (`highscore.txt`).

## Example Interaction
The game starts by displaying the initial grid, "Score: 0", and "Best: [High Score]". If the user presses 'D', all tiles slide to the right, identical numbers merge, and the score is updated. A new tile is then spawned, and the game waits for the next input.
