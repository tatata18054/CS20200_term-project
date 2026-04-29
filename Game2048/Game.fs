module Game

open System
open Board

type GameState = { Grid: Grid; Score: int; BestScore: int; IsGameOver: bool }

let highScoreFile = "highscore.txt"

let loadBestScore () =
    if IO.File.Exists(highScoreFile) then
        match Int32.TryParse(IO.File.ReadAllText(highScoreFile)) with
        | true, v -> v
        | _ -> 0
    else 0

let saveBestScore score =
    IO.File.WriteAllText(highScoreFile, score.ToString())

let render (state: GameState) =
    Console.Clear()
    printfn "=== 2048 F# Edition ==="
    printfn "Score: %d      Best: %d" state.Score state.BestScore
    printfn " ___________________________ "
    for r in 0..3 do
        printf "|"
        for c in 0..3 do
            let v = state.Grid.[r, c]
            if v = 0 then 
                printf "      |" 
            else 
                printf "%6d|" v
        printfn ""
        printfn "|______|______|______|______|"
    printfn "Controls: W/A/S/D (Quit: Q)"

let run () =
    let initialBest = loadBestScore ()
    let mutable state = { 
        Grid = Board.create () |> Board.spawnTile |> Board.spawnTile
        Score = 0
        BestScore = initialBest
        IsGameOver = false 
    }

    while not state.IsGameOver do
        render state
        let input = Console.ReadKey(true).Key
        let nextGrid, addedScore = 
            match input with
            | ConsoleKey.W -> Board.moveUp state.Grid
            | ConsoleKey.A -> Board.moveLeft state.Grid
            | ConsoleKey.S -> Board.moveDown state.Grid
            | ConsoleKey.D -> Board.moveRight state.Grid
            | _ -> state.Grid, 0
        
        if nextGrid <> state.Grid then
            let updatedGrid = Board.spawnTile nextGrid
            let newScore = state.Score + addedScore
            let newBest = max state.BestScore newScore
            
            state <- { state with 
                        Grid = updatedGrid
                        Score = newScore
                        BestScore = newBest
                        IsGameOver = Board.isGameOver updatedGrid }
            
            if newBest > initialBest then
                saveBestScore newBest

            if Board.isVictory updatedGrid then
                state <- { state with IsGameOver = true }
        
        if input = ConsoleKey.Q then state <- { state with IsGameOver = true }
    
    render state
    printfn "\nFinal Score: %d" state.Score
    if Board.isVictory state.Grid then
        printfn "Congratulations! You reached 2048!"
    else
        printfn "Game Over! No more moves possible."