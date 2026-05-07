open Game
open System

[<EntryPoint>]
let main _ =
    printfn "=== CLI-Number-Baseball ==="
    printfn "Enemy generated secret 3-digit number. Make a guess the number."
    let rec playLoop () =
        let _ = run ()
        printf "\nPlay again? (y/n): "
        match Console.ReadLine().Trim().ToLower() with
        | "y" -> playLoop ()
        | _ -> ()

    playLoop ()
    0