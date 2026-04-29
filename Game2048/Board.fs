module Board

open System

type Grid = int[,]

// 4x4 크기의 빈 격자를 생성합니다.
let create () : Grid = Array2D.create 4 4 0

// 빈 칸의 좌표 리스트를 반환합니다.
let getEmptyPositions (grid: Grid) =
    [ for r in 0..3 do
        for c in 0..3 do
            if grid.[r, c] = 0 then yield (r, c) ]

// 빈 칸 중 무작위 위치에 2(90% 확률) 또는 4(10% 확률)를 생성합니다.
let spawnTile (grid: Grid) =
    let emptyPos = getEmptyPositions grid
    if List.isEmpty emptyPos then grid
    else
        let r, c = emptyPos.[Random().Next(emptyPos.Length)]
        let nextValue = if Random().NextDouble() < 0.9 then 2 else 4
        let newGrid = Array2D.copy grid
        newGrid.[r, c] <- nextValue
        newGrid

// 한 줄(4개 요소)에 대한 병합 로직입니다. 2048의 핵심 규칙인 '같은 숫자 합치기'와 '점수 계산'을 수행합니다.
let private mergeLine (line: int[]) =
    // 0이 아닌 숫자만 추출합니다.
    let nonZeros = line |> Array.filter (fun x -> x <> 0)
    
    let rec combine acc src score =
        match src with
        | x :: y :: tail when x = y -> 
            // 두 숫자가 같으면 합치고 점수에 더합니다.
            combine (x + y :: acc) tail (score + x + y)
        | x :: tail -> 
            combine (x :: acc) tail score
        | [] -> (List.rev acc, score)
    
    let combinedList, addedScore = combine [] (List.ofArray nonZeros) 0
    
    // 결과를 4자리 배열로 만들고 나머지는 0으로 채웁니다.
    let resultArr = Array.ofList combinedList
    let padded = Array.zeroCreate 4
    if resultArr.Length > 0 then
        Array.blit resultArr 0 padded 0 resultArr.Length
    padded, addedScore

// 특정 행(Row)을 배열로 가져옵니다.
let private getRow (grid: Grid) r = [| for c in 0..3 -> grid.[r, c] |]
// 특정 열(Column)을 배열로 가져옵니다.
let private getCol (grid: Grid) c = [| for r in 0..3 -> grid.[r, c] |]

// 왼쪽 이동: 각 행을 정방향으로 병합합니다.
let moveLeft (grid: Grid) =
    let newGrid = Array2D.create 4 4 0
    let mutable totalScore = 0
    for r in 0..3 do
        let merged, score = mergeLine (getRow grid r)
        totalScore <- totalScore + score
        for c in 0..3 do newGrid.[r, c] <- merged.[c]
    newGrid, totalScore

// 오른쪽 이동: 각 행을 뒤집어서 병합한 후 다시 뒤집습니다.
let moveRight (grid: Grid) =
    let newGrid = Array2D.create 4 4 0
    let mutable totalScore = 0
    for r in 0..3 do
        let merged, score = mergeLine (getRow grid r |> Array.rev)
        totalScore <- totalScore + score
        let finalLine = Array.rev merged
        for c in 0..3 do newGrid.[r, c] <- finalLine.[c]
    newGrid, totalScore

// 위쪽 이동: 각 열을 정방향으로 병합합니다.
let moveUp (grid: Grid) =
    let newGrid = Array2D.create 4 4 0
    let mutable totalScore = 0
    for c in 0..3 do
        let merged, score = mergeLine (getCol grid c)
        totalScore <- totalScore + score
        for r in 0..3 do newGrid.[r, c] <- merged.[r]
    newGrid, totalScore

// 아래쪽 이동: 각 열을 뒤집어서 병합한 후 다시 뒤집습니다.
let moveDown (grid: Grid) =
    let newGrid = Array2D.create 4 4 0
    let mutable totalScore = 0
    for c in 0..3 do
        let merged, score = mergeLine (getCol grid c |> Array.rev)
        totalScore <- totalScore + score
        let finalCol = Array.rev merged
        for r in 0..3 do newGrid.[r, c] <- finalCol.[r]
    newGrid, totalScore

// 게임 종료 조건 확인 (빈 칸이 없고 합칠 수 있는 인접 타일이 없는 경우)
let isGameOver (grid: Grid) =
    if not (List.isEmpty (getEmptyPositions grid)) then false
    else
        let canMerge = 
            seq {
                for r in 0..3 do
                    for c in 0..3 do
                        if r < 3 && grid.[r, c] = grid.[r+1, c] then yield true
                        if c < 3 && grid.[r, c] = grid.[r, c+1] then yield true
            } |> Seq.contains true
        not canMerge

// 승리 조건 확인 (2048 타일 존재 여부)
let isVictory (grid: Grid) =
    let mutable found = false
    for r in 0..3 do
        for c in 0..3 do
            if grid.[r, c] >= 2048 then found <- true
    found