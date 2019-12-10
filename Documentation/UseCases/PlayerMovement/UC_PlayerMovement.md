# GyroGame <!-- omit in toc -->

# Use-Case Specification: Player Movement <!-- omit in toc -->

## Table of Contents <!-- omit in toc -->
- [Player Movement](#player-movement)
  - [1.1 Brief Description](#11-brief-description)
- [2. Flow of Events](#2-flow-of-events)
  - [2.1 Basic Flow](#21-basic-flow)
    - [2.1.1 Activity Diagram](#211-activity-diagram)
    - [2.1.2 Mock Up](#212-mock-up)
- [3. Special Requirements](#3-special-requirements)
- [4. Preconditions](#4-preconditions)
- [4.1 Level loaded](#41-level-loaded)
- [4.2 Level section](#42-level-section)
- [5. Postconditions](#5-postconditions)
  - [5.1 Character position updated](#51-character-position-updated)
- [6. Extension Points](#6-extension-points)


## Player Movement

### 1.1 Brief Description
As a requirement every player has to be able to connect the control cube to the game instance.

The title menu features a button for this function.

## 2. Flow of Events

### 2.1 Basic Flow

#### 2.1.1 Activity Diagram

![Activity Diagram - Player Movement](./AD_PlayerMovement.svg)

#### 2.1.2 Mock Up

![Moving and Jumping](../../images/MovingJumping.gif)

## 3. Special Requirements

(n/a)

## 4. Preconditions

## 4.1 Level loaded
A level has to be loaded and the game has to be unpaused.

## 4.2 Level section
The current section of the level has to allow the player to move around freely

## 5. Postconditions

### 5.1 Character position updated
The players position will change according to his inputs.

## 6. Extension Points

(n/a)