# Tanks

## Overview

**Tanks** is a 3D tank combat game developed using **Unity** and **C#**. Players control a battle tank and engage enemy tanks in an action-packed battlefield environment. The game combines player-controlled combat with AI-driven enemy behavior, creating dynamic and challenging gameplay.

The project demonstrates key game development concepts including tank movement, shooting mechanics, enemy AI, health systems, physics interactions, visual effects, and game state management.

## Gameplay Videos

https://github.com/user-attachments/assets/43c5a85c-f1af-4866-bc69-ca8c63d98c5f

## Features

* Player-controlled tank movement and rotation
* Turret aiming and projectile shooting system
* AI-controlled enemy tanks
* Health and damage system
* Physics-based projectile interactions
* Visual effects and explosion animations
* Sound effects and background music
* User Interface for health and game status
* Win and lose game conditions
* Built using Unity and C#

## AI Game Mechanics

The game incorporates several AI mechanics to create engaging enemy behavior:

### Enemy Tank AI

* Autonomous enemy tank navigation
* Target detection and tracking
* Dynamic pursuit of the player tank
* Automatic turret rotation toward the player
* Decision-making for attacking when the player is within range
* Continuous movement and repositioning during combat

### Combat AI

* Range-based attack behavior
* Projectile firing when the player enters attack range
* Cooldown system to prevent continuous firing
* Reactive combat responses based on player position

### State-Based Behavior

Enemy tanks operate using a simple state machine that includes:

* **Patrol State** – Moving through designated areas
* **Chase State** – Pursuing the player when detected
* **Attack State** – Firing projectiles when within range
* **Idle State** – Waiting when no target is detected

These AI systems provide a more dynamic and challenging gameplay experience.

## Gameplay

The objective is to destroy enemy tanks while protecting your own.

### How to Play

1. Launch the game.
2. Control your tank using the keyboard.
3. Aim and fire projectiles at enemy tanks.
4. Avoid incoming enemy attacks.
5. Eliminate all enemy tanks to win the game.

## Controls

| Action        | Control                      |
| ------------- | ---------------------------- |
| Move Forward  | W                            |
| Move Backward | S                            |
| Turn Left     | A                            |
| Turn Right    | D                            |
| Aim Turret    | Mouse Movement               |
| Fire          | Left Mouse Button / Spacebar |

## Technologies Used

* Unity Game Engine
* C#
* Unity Physics System
* Unity NavMesh (if used)
* Unity Particle System
* Unity UI Toolkit / UI System
* Unity Audio System

## Learning Objectives

This project showcases:

* Object-Oriented Programming in C#
* AI State Machine Implementation
* Enemy Targeting Systems
* Player Input Handling
* Projectile and Combat Mechanics
* Physics-Based Interactions
* UI Development
* Audio Integration
* Scene Management
