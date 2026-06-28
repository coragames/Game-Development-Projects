# ==========================================
# Player Information Window
# ==========================================

import tkinter as tk


def player_info():
    """
    Creates a window where two players
    can enter their names.
    """

    # ------------------------------------------
    # Create the main window
    # ------------------------------------------
    player_window = tk.Tk()

    player_window.title("Player Information")
    player_window.geometry("500x250+100+100")
    player_window.configure(bg="lightblue")

    # ------------------------------------------
    # Variables to store player names
    # ------------------------------------------
    player1_name_var = tk.StringVar()
    player2_name_var = tk.StringVar()

    # ------------------------------------------
    # Heading
    # ------------------------------------------
    heading_label = tk.Label(
        player_window,
        text="Enter Player Names",
        font=("Times", 20, "bold"),
        bg="lightblue",
        fg="black"
    )
    heading_label.grid(row=0, column=0, columnspan=2, pady=20)

    # ------------------------------------------
    # Player 1 Label
    # ------------------------------------------
    player1_label = tk.Label(
        player_window,
        text="Player 1",
        font=("Times", 14, "bold"),
        bg="black",
        fg="white",
        width=10
    )
    player1_label.grid(row=1, column=0, padx=20, pady=10)

    # ------------------------------------------
    # Player 1 Name Entry Box
    # ------------------------------------------
    player1_entry = tk.Entry(
        player_window,
        textvariable=player1_name_var,
        width=20
    )
    player1_entry.grid(row=2, column=0, padx=20)

    # ------------------------------------------
    # Player 2 Label
    # ------------------------------------------
    player2_label = tk.Label(
        player_window,
        text="Player 2",
        font=("Times", 14, "bold"),
        bg="black",
        fg="white",
        width=10
    )
    player2_label.grid(row=1, column=1, padx=20, pady=10)

    # ------------------------------------------
    # Player 2 Name Entry Box
    # ------------------------------------------
    player2_entry = tk.Entry(
        player_window,
        textvariable=player2_name_var,
        width=20
    )
    player2_entry.grid(row=2, column=1, padx=20)

    # ------------------------------------------
    # Function to display entered names
    # ------------------------------------------
    def submit_names():
        print("Player 1:", player1_name_var.get())
        print("Player 2:", player2_name_var.get())

    # ------------------------------------------
    # Submit Button
    # ------------------------------------------
    submit_button = tk.Button(
        player_window,
        text="Submit",
        command=submit_names,
        bg="green",
        fg="white",
        font=("Arial", 12, "bold")
    )
    submit_button.grid(row=3, column=0, columnspan=2, pady=30)

    # ------------------------------------------
    # Start the Tkinter event loop
    # ------------------------------------------
    player_window.mainloop()
