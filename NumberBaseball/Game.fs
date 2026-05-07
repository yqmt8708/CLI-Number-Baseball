module Game

open System

type GameResult = PlayerWins | EnemyWins

let private rng = Random()

let private generateSecret () : string = // generate a secret 3-digit number containing unique digits from 0 to 9.
    let rec generateDigits acc = 
        if String.length acc = 3 then acc
        else 
            let digit = rng.Next(0,10).ToString()
            if acc.Contains(digit) then generateDigits acc
            else generateDigits (digit + acc)
    generateDigits ""

let private isValidInput (input : string) : bool = 
    String.length input = 3 && // check whether it is 3 digits or not
    input |> String.forall Char.IsDigit && // check whether it contains non-numeric characters or not
    (input |> Seq.distinct |> Seq.length) = 3 // check whether it has repeating digits or not

let private getUserInput turn : string =
    let rec loop () =
        printf "[Turn %d] Your guess : " turn

        let input = Console.ReadLine()
        match isValidInput input with
        | true -> input
        | false ->
            printfn "Invalid input. Please enter a three-digit number with unique digits (eg. 123)"
            loop ()
    loop ()

let private getStrikeBall (secret : string) (guess: string) : int * int = 
    let strikes = 
        Seq.zip secret guess
        |> Seq.filter (fun (x,y) -> x = y)
        |> Seq.length
    let balls = 
        let intersection = Set.intersect (Set.ofSeq secret) (Set.ofSeq guess)
        let count = Set.count intersection
        count - strikes

    (strikes, balls)

let run () : GameResult = 
    let secret = generateSecret ()
    let rec loop turn = 
        printfn ""

        // Player's guess
        let guess = getUserInput turn

        // Evaluate player's guess
        let (strikes, balls) = getStrikeBall secret guess

        if strikes = 0 && balls = 0 then // If none of the digits match the secret number, "Out"
            printfn "[Turn %d] Out" turn
        elif strikes <= 1 && balls <= 1 then 
            printfn "[Turn %d] %d strike %d ball" turn strikes balls
        elif strikes <= 1 && balls > 1 then 
            printfn "[Turn %d] %d strike %d balls" turn strikes balls
        elif strikes > 1 && balls <= 1 then 
            printfn "[Turn %d] %d strikes %d ball" turn strikes balls
        else 
            printfn "[Turn %d] %d strikes %d balls" turn strikes balls


        if strikes = 3 then // When the user gets 3 strikes within the 6 turns, the user wins.
            printfn ""
            printfn "3 strikes! You win!"
            PlayerWins
        elif turn >= 6 then // When the user consumes all 6 turns without getting 3 strikes, the enemy wins
            printfn "Out of turns! You lose... The secret number was %s." secret 
            EnemyWins
        else 
            loop (turn + 1)
    loop 1