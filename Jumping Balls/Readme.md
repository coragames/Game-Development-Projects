# Jumping Balls

A fast-paced endless arcade game where your reflexes are put to the test! Destroy bouncing balls, earn points, and extend your playtime before the timer runs out.

---

## Screenshots


---

## Gameplay

- The game starts with **1 bouncing ball** inside the screen boundaries.
- Tap on a ball to destroy it.
- Each destroyed ball:
  - Increases your score by **1**
  - Adds **5 seconds** to the remaining timer
  - Starts the next wave with **one additional ball**
- The game begins with a **60-second countdown timer**.
- Keep clearing waves to survive as long as possible.

---

## Difficulty Progression

The game becomes harder after every wave.

| Wave | Balls |
|------:|------:|
| 1 | 1 |
| 2 | 2 |
| 3 | 3 |
| 4 | 4 |
| ... | ... |

Every completed wave spawns **one more ball** than the previous wave, making the game progressively more challenging.

---

## Scoring

- +1 point for every ball destroyed.
- +5 seconds added to the timer for every successful hit.
- Try to beat your **Highest Score**!

---

## Game Over

The game ends when the timer reaches **0**.

The Game Over screen displays:

- Current Score
- Highest Score
- Restart Button

Restart the game and see how long you can survive!

---

## Features

- Endless arcade gameplay
- Progressive difficulty
- Physics-based bouncing balls
- 60-second countdown timer
- +5 seconds bonus for every successful hit
- High score tracking
- Simple one-touch controls
- Quick restart

---

## Objective

Destroy every bouncing ball before the timer expires.

The faster you clear each wave, the more time you earn, allowing you to continue playing and achieve even higher scores.

---

## Controls

| Action | Control |
|---------|---------|
| Destroy Ball | Tap on a ball |
| Restart Game | Tap **Restart** |

---

## Game Flow

```text
Start Game
    │
    ▼
Spawn 1 Ball
    │
    ▼
Destroy Ball
    │
    ├── +1 Score
    ├── +5 Seconds
    └── Spawn Next Wave (+1 Ball)
             │
             ▼
      Repeat Until Timer = 0
             │
             ▼
          Game Over
             │
      ├── Current Score
      ├── Highest Score
      └── Restart
