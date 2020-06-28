# GyroGame - Software Architecture Documentation

## Table of Contents

- [GyroGame - Software Architecture Documentation](#gyrogame---software-architecture-documentation)
  - [Table of Contents](#table-of-contents)
  - [1. Introduction](#1-introduction)
    - [1.1 Purpose](#11-purpose)
    - [1.2 Scope](#12-scope)
    - [1.3 Definitions, Acronyms, and Abbreviations](#13-definitions-acronyms-and-abbreviations)
    - [1.4 References](#14-references)
    - [1.5 Overview](#15-overview)
  - [2. Architectural Representation](#2-architectural-representation)
  - [3. Architectural Goals and Constraints](#3-architectural-goals-and-constraints)
  - [4. Use-Case View](#4-use-case-view)
  - [5. Logical View](#5-logical-view)
    - [5.1 Overview](#51-overview)
    - [5.2 Architecturally Significant Design Packages](#52-architecturally-significant-design-packages)
  - [6. Process View](#6-process-view)
  - [7. Deployment View](#7-deployment-view)
  - [8. Implementation View](#8-implementation-view)
    - [8.1 Overview](#81-overview)
    - [8.2 Layers](#82-layers)
  - [9. Data View](#9-data-view)
  - [10. Size and Performance](#10-size-and-performance)
  - [11. Quality](#11-quality)

## 1. Introduction

### 1.1 Purpose

This document provides a comprehensive architectural overview of the system, by using several architectural views to depict different aspects of the system. It is intended to capture and convey the significant architectural decisions which have been made on the system.

### 1.2 Scope

This document describes the architecture of the GyroGame Project.

### 1.3 Definitions, Acronyms, and Abbreviations

| **Abbreviation** |                |
| ---------------- | -------------- |
| N/A              | Not applicable |

### 1.4 References

| **Title**                                                                                                                 |
| -----------------------------------------------------------------------------                                             |
| [**GyroGame Blog**](https://gyrogame.de/)                                                                                 |
| [**GitHub - Unity Project**](https://github.com/GyroInc/gyrogame-unity)                                                   |
| [**GitHub - Controller Firmware**](https://github.com/GyroInc/gyrogame-hardware)                                          |
| [**YouTrack Project Management**](https://youtrack.gyrogame.de)                                                           |
| [**Software Requirements Specification**]((https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/SRS.md))   |

### 1.5 Overview

The following sections describe the architectural details of GyroGame.
This document does not describe functionality made available by Unity but rather describes the original code of this project.

## 2. Architectural Representation

Unity utilizes what's called component architecture. Every game object can be deployed any and as many components as necessary. Each component now represents one functionality that it adds to the game object. Ideally, each component should be it's own system and not depend on another one.

## 3. Architectural Goals and Constraints

This component-based architecture, if properly executed, allows for a very clear and easy handling with functionality, that every object can be deployed with. It also allows for good scalability of the games scope with components being reusable for many different objects and game events.

## 4. Use-Case View

Below, you can find the overall use case diagram that shows all use cases the application should provide.

![Overall Use Case Diagram](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/OUCD_rev2.svg)

Here you can find the various use case specification documents:

- [Use Case: Player Movement](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/UseCases/PlayerMovement/UC_PlayerMovement.md)
- [Use Case: Pause Menu](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/UseCases/PauseMenu/UC_PauseMenu.md)
- [Use Case: Connect Control Cube](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/UseCases/ConnectCube/UC_ConnectCube.md)
- [Use Case: Rotate Obstacle](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/UseCases/RotateObstacle/UC_RotateObstacle.md)

## 5. Logical View

### 5.1 Overview

![Component Architecture Logical View](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/images/ComponentLogicalView.svg)

### 5.2 Architecturally Significant Design Packages

![Class Diagram](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/images/ClassDiagram_labeled.png)

## 6. Process View

N/A

## 7. Deployment View

![Deployment View](https://github.com/GyroInc/gyrogame-unity/blob/master/Documentation/images/DeploymentView.svg)

## 8. Implementation View

### 8.1 Overview

N/A

### 8.2 Layers

N/A

## 9. Data View

The only persistent data stored for the game are:

- Save files to store player progress
- Game settings (e.g. mouse speed)

These are being handled by Unity's internal PlayerPrefs.

## 10. Size and Performance

N/A

## 11. Quality

Unity recommends the component architecture to be used in the development of an application. Therefore the quality is at its best choosing this architecture.
