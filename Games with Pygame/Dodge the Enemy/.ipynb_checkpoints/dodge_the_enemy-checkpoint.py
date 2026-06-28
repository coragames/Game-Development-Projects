"""
Dodge The Bird
==============

Features
--------
✓ Arrow-key movement
✓ Random bird sizes
✓ Random bird speeds
✓ Enemy image scaled to bird size
✓ Collision detection
✓ Current score display
✓ Persistent best score
✓ Game Over screen
✓ Restart game with R
✓ Quit game with Q
✓ Background music loops during gameplay
✓ Music stops on collision
✓ Music restarts when a new game begins
✓ Pause game with P
✓ Music pauses and resumes correctly
"""

import pygame
import random
import sys

# ==========================================================
# INITIALIZE PYGAME
# ==========================================================
pygame.init()

# ==========================================================
# SCREEN SETTINGS
# ==========================================================
SCREEN_WIDTH = 1200
SCREEN_HEIGHT = 800
FPS = 60

# ==========================================================
# COLORS
# ==========================================================
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GREEN = (50, 180, 50)
RED = (255, 0, 0)

# ==========================================================
# PLAYER SETTINGS
# ==========================================================
PLAYER_SPEED = 5

# ==========================================================
# BIRD SETTINGS
# ==========================================================
BIRD_MIN_SIZE = 20
BIRD_MAX_SIZE = 60

BIRD_MIN_SPEED = 2
BIRD_MAX_SPEED = 10

BIRD_SPAWN_RATE = 25

# ==========================================================
# HIGH SCORE FILE
# ==========================================================
HIGH_SCORE_FILE = "highscore.txt"

# ==========================================================
# CREATE GAME WINDOW
# ==========================================================
screen = pygame.display.set_mode(
    (SCREEN_WIDTH, SCREEN_HEIGHT)
)

pygame.display.set_caption("Dodge The Bird")

clock = pygame.time.Clock()

# ==========================================================
# LOAD IMAGES
# ==========================================================
player_image = pygame.image.load(
    "/Users/ravirajpanchal/Learn Pygame/Dodge the Enemy/angry_bird_hero.png"
).convert_alpha()

enemy_image = pygame.image.load(
    "/Users/ravirajpanchal/Learn Pygame/Dodge the Enemy/angry_brd_blue.png"
).convert_alpha()

# ==========================================================
# LOAD AUDIO
# ==========================================================
music_loaded = False
hit_sound = None

try:
    pygame.mixer.music.load(
        "(Loop) juanjo_sound - matumbia.wav"
    )

    hit_sound = pygame.mixer.Sound(
        "Interface Push Button.mp3"
    )

    music_loaded = True

except Exception as error:
    print("Audio files could not be loaded.")
    print(error)

# ==========================================================
# FONTS
# ==========================================================
font = pygame.font.SysFont(None, 36)

large_font = pygame.font.SysFont(None, 60)

# ==========================================================
# HIGH SCORE FUNCTIONS
# ==========================================================
def load_best_score():
    """
    Load best score from disk.
    """

    try:
        with open(HIGH_SCORE_FILE, "r") as file:
            return int(file.read())

    except:
        return 0


def save_best_score(score):
    """
    Save best score to disk.
    """

    with open(HIGH_SCORE_FILE, "w") as file:
        file.write(str(score))


# ==========================================================
# TEXT DRAWING HELPER
# ==========================================================
def draw_text(text, font_obj, color, x, y):

    text_surface = font_obj.render(
        text,
        True,
        color
    )

    screen.blit(
        text_surface,
        (x, y)
    )


# ==========================================================
# COLLISION DETECTION
# ==========================================================
def player_collided(player_rect, birds):

    for bird in birds:

        if player_rect.colliderect(
            bird["rect"]
        ):
            return True

    return False


# ==========================================================
# PAUSE SCREEN
# ==========================================================
def pause_game():

    paused = True

    pygame.mixer.music.pause()

    while paused:

        screen.fill(BLACK)

        draw_text(
            "PAUSED",
            large_font,
            WHITE,
            SCREEN_WIDTH // 2 - 90,
            120
        )

        draw_text(
            "Press P to Resume",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 110,
            220
        )

        draw_text(
            "Press Q to Quit",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 90,
            260
        )

        pygame.display.update()

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:

                if event.key == pygame.K_p:

                    pygame.mixer.music.unpause()
                    paused = False

                elif event.key == pygame.K_q:

                    pygame.quit()
                    sys.exit()

        clock.tick(FPS)


# ==========================================================
# GAME OVER SCREEN
# ==========================================================
def game_over_screen(score, best_score):

    while True:

        screen.fill(BLACK)

        draw_text(
            "GAME OVER",
            large_font,
            RED,
            SCREEN_WIDTH // 2 - 140,
            80
        )

        draw_text(
            f"Score: {score}",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 70,
            180
        )

        draw_text(
            f"Best Score: {best_score}",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 90,
            220
        )

        draw_text(
            "Press R to Restart",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 110,
            290
        )

        draw_text(
            "Press Q to Quit",
            font,
            WHITE,
            SCREEN_WIDTH // 2 - 90,
            330
        )

        pygame.display.update()

        for event in pygame.event.get():

            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:

                if event.key == pygame.K_r:
                    return

                elif event.key == pygame.K_q:
                    pygame.quit()
                    sys.exit()

        clock.tick(FPS)


# ==========================================================
# MAIN GAME FUNCTION
# ==========================================================
def run_game():

    best_score = load_best_score()

    while True:

        # --------------------------------------------------
        # Start / Restart background music
        # --------------------------------------------------
        if music_loaded:

            pygame.mixer.music.stop()
            pygame.mixer.music.play(-1)

        # --------------------------------------------------
        # Create player
        # --------------------------------------------------
        player_rect = player_image.get_rect()

        player_rect.center = (
            SCREEN_WIDTH // 2,
            SCREEN_HEIGHT - 60
        )

        # --------------------------------------------------
        # Game variables
        # --------------------------------------------------
        birds = []

        score = 0

        bird_spawn_counter = 0

        move_left = False
        move_right = False
        move_up = False
        move_down = False

        game_running = True

        # ==================================================
        # GAME LOOP
        # ==================================================
        while game_running:

            score += 1

            # ----------------------------------------------
            # EVENT HANDLING
            # ----------------------------------------------
            for event in pygame.event.get():

                if event.type == pygame.QUIT:
                    pygame.quit()
                    sys.exit()

                if event.type == pygame.KEYDOWN:

                    if event.key == pygame.K_LEFT:
                        move_left = True

                    elif event.key == pygame.K_RIGHT:
                        move_right = True

                    elif event.key == pygame.K_UP:
                        move_up = True

                    elif event.key == pygame.K_DOWN:
                        move_down = True

                    elif event.key == pygame.K_p:
                        pause_game()

                    elif event.key == pygame.K_q:
                        pygame.quit()
                        sys.exit()

                elif event.type == pygame.KEYUP:

                    if event.key == pygame.K_LEFT:
                        move_left = False

                    elif event.key == pygame.K_RIGHT:
                        move_right = False

                    elif event.key == pygame.K_UP:
                        move_up = False

                    elif event.key == pygame.K_DOWN:
                        move_down = False

            # ----------------------------------------------
            # MOVE PLAYER
            # ----------------------------------------------
            if move_left and player_rect.left > 0:
                player_rect.x -= PLAYER_SPEED

            if move_right and player_rect.right < SCREEN_WIDTH:
                player_rect.x += PLAYER_SPEED

            if move_up and player_rect.top > 0:
                player_rect.y -= PLAYER_SPEED

            if move_down and player_rect.bottom < SCREEN_HEIGHT:
                player_rect.y += PLAYER_SPEED

            # ----------------------------------------------
            # SPAWN BIRDS
            # ----------------------------------------------
            bird_spawn_counter += 1

            if bird_spawn_counter >= BIRD_SPAWN_RATE:

                bird_spawn_counter = 0

                bird_size = random.randint(
                    BIRD_MIN_SIZE,
                    BIRD_MAX_SIZE
                )

                scaled_enemy_image = pygame.transform.scale(
                    enemy_image,
                    (bird_size, bird_size)
                )

                bird_rect = pygame.Rect(
                    random.randint(
                        0,
                        SCREEN_WIDTH - bird_size
                    ),
                    -bird_size,
                    bird_size,
                    bird_size
                )

                birds.append(
                    {
                        "rect": bird_rect,
                        "speed": random.randint(
                            BIRD_MIN_SPEED,
                            BIRD_MAX_SPEED
                        ),
                        "image": scaled_enemy_image
                    }
                )

            # ----------------------------------------------
            # MOVE BIRDS
            # ----------------------------------------------
            for bird in birds:

                bird["rect"].y += bird["speed"]

            # ----------------------------------------------
            # REMOVE OFF-SCREEN BIRDS
            # ----------------------------------------------
            birds = [
                bird
                for bird in birds
                if bird["rect"].top < SCREEN_HEIGHT
            ]

            # ----------------------------------------------
            # COLLISION CHECK
            # ----------------------------------------------
            if player_collided(
                player_rect,
                birds
            ):

                # Stop background music
                if music_loaded:
                    pygame.mixer.music.stop()

                # Play collision sound
                if hit_sound:
                    hit_sound.play()

                # Update best score
                if score > best_score:

                    best_score = score

                    save_best_score(
                        best_score
                    )

                game_running = False

            # ----------------------------------------------
            # DRAW EVERYTHING
            # ----------------------------------------------
            screen.fill(GREEN)

            screen.blit(
                player_image,
                player_rect
            )

            for bird in birds:

                screen.blit(
                    bird["image"],
                    bird["rect"]
                )

            draw_text(
                f"Score: {score}",
                font,
                BLACK,
                10,
                10
            )

            draw_text(
                f"Best: {best_score}",
                font,
                BLACK,
                10,
                45
            )

            pygame.display.update()

            clock.tick(FPS)

        # --------------------------------------------------
        # Show game-over screen
        # --------------------------------------------------
        game_over_screen(
            score,
            best_score
        )


# ==========================================================
# PROGRAM ENTRY POINT
# ==========================================================
def main():
    run_game()


if __name__ == "__main__":
    main()
