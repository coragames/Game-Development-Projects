"""
Main Menu for Dodge The Bird
"""

import tkinter as tk
from tkinter import messagebox
import dodge_the_enemy

# ==========================================
# Load High Score
# ==========================================
def load_best_score():

    try:
        with open("highscore.txt", "r") as file:
            return int(file.read())

    except:
        return 0


# ==========================================
# Start Game
# ==========================================
def start_game():

    root.withdraw()

    try:
        dodge_the_enemy.main()

    except Exception as error:

        messagebox.showerror(
            "Game Error",
            str(error)
        )

    root.deiconify()
    update_high_score()


# ==========================================
# Update High Score Label
# ==========================================
def update_high_score():

    score_label.config(
        text=f"Best Score: {load_best_score()}"
    )


# ==========================================
# Exit Application
# ==========================================
def exit_game():

    root.destroy()


# ==========================================
# Create Window
# ==========================================
root = tk.Tk()

root.title("Dodge The Bird")

root.geometry("500x350")
root.resizable(False, False)

root.configure(bg="#2C3E50")


# ==========================================
# Title
# ==========================================
title_label = tk.Label(
    root,
    text="Dodge The Bird",
    font=("Arial", 24, "bold"),
    bg="#2C3E50",
    fg="white"
)

title_label.pack(pady=30)


# ==========================================
# High Score
# ==========================================
score_label = tk.Label(
    root,
    text=f"Best Score: {load_best_score()}",
    font=("Arial", 16),
    bg="#2C3E50",
    fg="#F1C40F"
)

score_label.pack(pady=20)


# ==========================================
# Start Button
# ==========================================
start_button = tk.Button(
    root,
    text="Start Game",
    command=start_game
)

# ==========================================
# Exit Button
# ==========================================
exit_button = tk.Button(
    root,
    text="Exit",
    command=exit_game
)

exit_button.pack(pady=10)


# ==========================================
# Run GUI
# ==========================================
root.mainloop()
