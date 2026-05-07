# CLI-Number-Baseball

A command-line Number Baseball game originally known as Bulls and Cows built with **F# / .NET 10**.

The enemy generates a 3-digit number with unique digits, and you try to guess it using strike and ball hints.

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)  
  Verify with: `dotnet --version` (should show `10.x.x`)

### Run

```bash
# Windows
run.bat

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

### Publish Self-Contained Binary

```bash
# Windows x64
dotnet publish -c Release -r win-x64 --self-contained

# Linux x64
dotnet publish -c Release -r linux-x64 --self-contained
```

---

## How to Play

### Generating Secret Number

The enemy generates a secret 3-digit number containing unique digits from `0` to `9`.

### Taking a Turn

1. You are prompted: `[Turn X] Your guess :`
2. Type a 3-digit number and press **Enter**.
    -  If the input is invalid 
    (e.g., not 3 digits, containing non-numeric characters, or having repeating digits),
    you are asked to try again without consuming a turn.
3. The enemy evaluates your guess and outputs the result with strike and ball hints.

### Strike and Ball

- **Strike**: A correct digit in the correct position.
- **Ball**: A correct digit in the wrong position.
- **Out**: None of the digits match the secret number.

### Winning & Ending

| Result | Condition |
|--------|-----------|
| **You win** | You get **3 strikes** within the 6 turns |
| **Enemy wins** | You consume all **6 turns** without getting 3 strikes |

After the game ends, you are asked whether to play again.

---

## Example Session

``` 
=== CLI-Number-Baseball ===
Enemy generated secret 3-digit number. Guess the number.

[Turn 1] Your guess : 012
[Turn 1] 2 strikes 0 ball

[Turn 2] Your guess : 345
[Turn 2] 0 strike 1 ball

[Turn 3] Your guess : 678
[Turn 3] Out
...
```

---

### Project Structure

```
CLI-Number-Baseball/
├── NumberBaseball.fsproj  # .NET 10 F# project file
├── run.bat                # Windows run script
├── run.sh                 # Unix run script
├── README.md
├── requirements.md
└── NumberBaseball/
    ├── Game.fs            # Game loop, secret number generation, player input validation, player input evaluation
    └── Program.fs         # Entry point, play-again loop
```

### Key Types

```fsharp
// Possible outcomes of a completed game
type GameResult = PlayerWins | EnemyWins
```

### Module Overview

| Module | Responsibility |
|--------|---------------|
| `Game`  | Main game loop, secret number generation, input validation, player input evaluation, end-state display |
| `Program` | Entry point; prints generating secret number message, drives play-again loop |

---

## Rules Summary

- The secret number contains unique digits from `0` to `9`.
- You have a maximum of **6 turns** to guess the number.
- Selecting an invalid input re-prompts — the turn does **not** advance.
- A **strike** means right digit, *right place*. A **ball** means right digit, *wrong place*.
- 3 strikes win the game. Consuming 6 turns loses the game.

## LLM Usage

**What I used the LLM for:**
I used the LLM to help generate the initial F# project structure and the logic for generating a random 3-digit number.

**What I had to manually change or reprompt:**
When I asked for the logic to generate a random 3-digit number, the LLM didn't consider that the secret number should contain unique digits. Once I realized this, I had to manually change it.

**The main point that the LLM was not able to do correctly:**
While it is true that I did not explicitly specify "no duplicate digits" in my prompt, I had clearly mentioned that the function was for a "Number Baseball" game. I expected the LLM to infer the standard rules of this game and automatically generate a 3-digit number with unique digits. However, the LLM failed to apply this contextual knowledge to the code generation, simply returning a generic random 3-digit number generator instead.