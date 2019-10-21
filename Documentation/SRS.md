# GyroGame - Software Requirements Specification

## Revision History

| Date       |  Version | Description       |
| --------   | -------- | ----------------- |
| 21/10/2019 |   1.0    | initial version   | 

## Table of Contents
- [GyroGame - Software Requirements Specification](#gyrogame---software-requirements-specification)
  - [Revision History](#revision-history)
  - [Table of Contents](#table-of-contents)
  - [1. Introduction](#1-introduction)
    - [1.1 Purpose](#11-purpose)
    - [1.2 Scope](#12-scope)
    - [1.3 Definitions, Acronyms and Abbreviations](#13-definitions-acronyms-and-abbreviations)
    - [1.4 References](#14-references)
    - [1.5 Overview](#15-overview)
  - [2. Overall Description](#2-overall-description)
    - [2.1 Vision](#21-vision)
    - [2.2 Overall Use-Case-Diagram](#22-overall-use-case-diagram)
    - [2.3 User Characteristics](#23-user-characteristics)
  - [3. Specific Requirements](#3-specific-requirements)
    - [3.1 Functionality](#31-functionality)
      - [3.1.1 Functionality - The Controller](#311-functionality---the-controller)
      - [3.1.2 Functionality - The Game](#312-functionality---the-game)
    - [3.2 Usability](#32-usability)
    - [3.3 Reliability](#33-reliability)
      - [3.3.1 Availability](#331-availability)
      - [3.3.2 Bugs](#332-bugs)
    - [3.4 Performance](#34-performance)
    - [3.5 Supportability](#35-supportability)
    - [3.6 Design Constraints](#36-design-constraints)
      - [3.6.1 Development tools](#361-development-tools)
      - [3.6.2 Unity](#362-unity)
      - [3.6.3 Arduino IDE](#363-arduino-ide)
    - [3.7 Purchased Components](#37-purchased-components)
    - [3.9 Interfaces](#39-interfaces)
      - [3.9.1 User Interfaces](#391-user-interfaces)
      - [3.9.2 Hardware Interfaces](#392-hardware-interfaces)
      - [3.9.3 Software Interfaces](#393-software-interfaces)
      - [3.9.4 Communications Interfaces](#394-communications-interfaces)
    - [3.10 Licensing Requirements](#310-licensing-requirements)
    - [3.12 Legal, Copyright and other Notices](#312-legal-copyright-and-other-notices)
    - [3.13 Applicable Standards](#313-applicable-standards)
  - [4. Supporting Information](#4-supporting-information)


## 1. Introduction

### 1.1 Purpose

The purpose of this document gives a general description of the GyroGame Project. It explains our vision and all features of the product. Also it offers insights into the hardware and software elements of the project.

### 1.2 Scope

This document is designed for internal use only and will outline the development process of the project.

### 1.3 Definitions, Acronyms and Abbreviations

| Term     |                                     |
| -------- | ----------------------------------- |
| **SRS**  | Software Requirements Specification |
| **Unity**| The used game engine                |
| **AAA**  | A very high budget game              |

### 1.4 References

| Title                                                                 | Date       |
| --------------------                                                  | ---------- |
| [**Blog**](https://gyrogame.de/)                                          | 19/10/2019 |
| [**GitHub - Unity Project**](https://github.com/Manut38/gyrogame-unity)              | 19/10/2019 |
| [**GitHub - Controller Firmware**](https://github.com/Manut38/gyrogame-hardware)     | 19/10/2019 |
| [**Use Case Diagram**](https://github.com/Manut38/gyrogame-unity/blob/master/Documentation/OUCD.PNG)                                                | 21/10/2018 |

### 1.5 Overview

The next chapters provide information about our vision based on the use case diagram as well as more detailed software requirements.

## 2. Overall Description

### 2.1 Vision

The goal of GyroGame is to create a new kind of game for gamers that are also makers. Users will have the possibility to follow a guide and build their own game controller. When they did so correctly they will be able to play the game.

### 2.2 Overall Use-Case-Diagram

![UseCaseDiagram](https://github.com/Manut38/gyrogame-unity/blob/master/Documentation/OUCD.PNG)

### 2.3 User Characteristics

Our main target group consists of people who like to play video games but also like to build electronics projects.

## 3. Specific Requirements

### 3.1 Functionality

#### 3.1.1 Functionality - The Controller

The game uses the custom controller as a key component. Without it the game can not be played. The controller uses a gyroscope to sense its orientation, this data will be used to solve puzzles in game.
The controller will have a telemetry to show the remaining battery.

#### 3.1.2 Functionality - The Game

The game will be a first person puzzle game. The user has to solve puzzles via interaction with the controller. The puzzles will consist of blocked paths which will have to be bypassed by rotating objects, rooms or gravity with the cube. These will be color coded. In addition clues about solving approaches can be given via the lighting output of the controller.

### 3.2 Usability

With very few user inputs e.g. rotating the cube, walking around and looking around, the game will be intuitive to play. Also there will be at most a minimal user interface, to keep the game as simple as possible.

### 3.3 Reliability

#### 3.3.1 Availability

The game will be available to everyone for free, which includes the building instructions for the controller. It has to be built by everyone who wants to play the game.

#### 3.3.2 Bugs

We classify bugs like the following:

-   **Critical bug**: An error that crashes the game or hinders the player from progressing any further. 
-   **Non critical bug**: An issue that will not create a game breaking experience like shading issues or communication issues with the controller.

### 3.4 Performance

Unity has a baseline hardware requirement that must be met for it to work. However the game will not be an AAA title thus most computers will be able to run the game.

### 3.5 Supportability

Once the game is finished it wont be supported by us anymore. It is an open source project, so anyone who has improvements will be able to implement them und update the game.

### 3.6 Design Constraints

Due to limited time and resources we will keep the graphics simple and minimal, to reduce the time of 3d modelling and focus more on level design.

#### 3.6.1 Development tools

-   **git**: Version control system
-   **YouTrack**: Project management application and sprint management
-   **Unity**: Game engine
-   **Arduino IDE**: IDE for programming microcontrollers 
-   **Visual Studio**: IDE used for scripting within Unity

#### 3.6.2 Unity

Unity is a free and easy to use game engine. It is widely used by many people and has an active development and user community.
It is built on the .NET Framework and can be used like any other application.

#### 3.6.3 Arduino IDE

The Arduino IDE is a free IDE for programming arduino and arduino compatible microcontrollers. It has a builtin compiler and use mostly C code to program the devices.

### 3.7 Purchased Components

  We won't list any components as purchased, so we are can keep the self-built controllers.

### 3.9 Interfaces

#### 3.9.1 User Interfaces

The user will be able to navigate the menus via mouse input and a GUI, input movement commands via the W, A, S, D keys and look around in the 3d environment with his mouse.

#### 3.9.2 Hardware Interfaces

The application will use a bi-directional bluetooth connection to communicate to the controller.

#### 3.9.3 Software Interfaces

The bluetooth connection will transfer serial data back and fourth between the game and the controller hardware.

#### 3.9.4 Communications Interfaces

- N\A

### 3.10 Licensing Requirements

Our project runs under the MIT License. This way everyone is allowed to fork the project and create his own version.

### 3.12 Legal, Copyright and other Notices

-   N\A

### 3.13 Applicable Standards

-   N\A

## 4. Supporting Information

For a better overview, look at the table of contents and/or references.