open System
open System.IO

let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "Input.txt")
let inputLines = File.ReadAllLines(inputPath)

///let inputLines = [|"3   4"; "4   3";"2   5";"1   3";"3   9";"3   3"|]

let leftLocationIDs = Array.zeroCreate<int>inputLines.Length
let rightLocationIDs = Array.zeroCreate<int>inputLines.Length

type locationCombi = {Left:int; Right:int }
let lineParse (line:string) = 
    let stringIDs = line.Split("   ")
    {Left = (int)stringIDs[0]; Right = (int)stringIDs[1]}

inputLines
|> Array.map lineParse
|> Array.iteri (
    fun  index locationCombi -> 
        leftLocationIDs[index] <- locationCombi.Left
        rightLocationIDs[index] <- locationCombi.Right
    )

let leftLocationIDsSorted = Array.sort leftLocationIDs
let rightLocationIDsSorted =  Array.sort rightLocationIDs

// Antwoord Part 1
[|0..inputLines.Length - 1|]
|> Array.map (fun index -> Math.Abs(leftLocationIDsSorted[index] - rightLocationIDsSorted[index]))
|> Array.sum

let getSimilarityScore numbers number  =
    let nrOfOccurances =
        numbers
        |> Array.where (fun n -> n = number)
        |> Array.length
    nrOfOccurances * number

// Antwoord Part 2
leftLocationIDsSorted
|> Array.map (fun number -> getSimilarityScore rightLocationIDs number)
|> Array.sum