# Gyro Game - Software Requirements Specification

## Table of Contents

-   [Gyro Game - Software Requirements Specification](#flashcard-community---software-requirements-specification)

    -   [Table of Contents](#table-of-contents)

    -   [1. Introduction](#1-introduction)

        -   [1.1 Purpose](#11-purpose)
        -   [1.2 Scope](#12-scope)
        -   [1.3 Definitions, Acronyms and Abbreviations](#13-definitions-acronyms-and-abbreviations)
        -   [1.4 References](#14-references)
        -   [1.5 Overview](#15-overview)

    -   [2. Overall Description](#2-overall-description)

        -   [2.1 Vision](#21-vision)

    -   [2.2 Product perspective](#22-product-perspective)

        -   [2.3 User characteristics](#23-user-characteristics)
        -   [2.4 Dependencies](#24-dependencies)

    -   [3. Specific Requirements](#3-specific-requirements)

        -   [3.1 Functionality – Data Backend](#31-functionality--data-backend)

            -   [3.1.1 Read data given over API endpoints](#311-read-data-given-over-api-endpoints)
            -   [3.1.2 Parse data](#312-parse-data)
            -   [3.1.3 Provide data](#313-provide-data)

        -   [3.2 Functionality – User Interface](#32-functionality--user-interface)

            -   [3.2.1 User system](#321-user-system)
            -   [3.2.3 Flashcard boxes](#323-flashcard-boxes)
            -   [3.2.4 Flashcards](#324-flashcards)
            -   [3.2.5 Statistics](#325-statistics)

        -   [3.3 Usability](#33-usability)

        -   [3.4 Reliability](#34-reliability)

            -   [3.4.1 Availability](#341-availability)
            -   [3.4.2 MTBF, MTTR](#342-mtbf-mttr)
            -   [3.4.3 Accuracy](#343-accuracy)
            -   [3.4.4 Bug classes](#344-bug-classes)

        -   [3.5 Performance](#35-performance)

            -   [3.5.1 Response time](#351-response-time)
            -   [3.5.2 Throughput](#352-throughput)
            -   [3.5.3 Capacity](#353-capacity)
            -   [3.5.4 Resource utilization](#354-resource-utilization)

        -   [3.6 Supportability](#36-supportability)

        -   [3.7 Design Constraints](#37-design-constraints)

            -   [3.7.1 Development tools](#371-development-tools)
            -   [3.7.2 Spring Boot](#372-spring-boot)
            -   [3.7.3 ReactJS](#373-reactjs)
            -   [3.7.4 Supported Platforms](#374-supported-platforms)

        -   [3.8 Online User Documentation and Help System Requirements](#38-online-user-documentation-and-help-system-requirements)

        -   [3.9 Purchased Components](#39-purchased-components)

        -   [3.10 Interfaces](#310-interfaces)

            -   [3.10.1 User Interfaces](#3101-user-interfaces)
            -   [3.10.2 Hardware Interfaces](#3102-hardware-interfaces)
            -   [3.10.3 Software Interfaces](#3103-software-interfaces)
            -   [3.10.4 Communications Interfaces](#3104-communications-interfaces)

        -   [3.11 Licensing Requirements](#311-licensing-requirements)

        -   [3.12 Legal, Copyright and other Notices](#312-legal-copyright-and-other-notices)

        -   [3.13 Applicable Standards](#313-applicable-standards)

    -   [4. Supporting Information](#4-supporting-information)

## 1. Introduction

### 1.1 Purpose

The purpose of this document gives a general description of the Gyro Game Project. It explains our vision and all features of the product. Also it offers insights into the hardware and software elements of the project.

### 1.2 Scope

This document is designed for internal use only and will outline the development process of the project.

### 1.3 Definitions, Acronyms and Abbreviations

| Term     |                                     |
| -------- | ----------------------------------- |
| **SRS**  | Software Requirements Specification |
| **Unity**| The used game engine                |
| **AAA**  | A very high buget game              |



### 1.4 References

| Title                                                                                                 | Date       |
| ----------------------------------------------------------------------------------------------------- | ---------- |
| [Blog](https://gyrogame.de/)                                                     | 19/10/2019 |
| [GitHub Controller](https://github.com/Manut38/gyrogame-hardware)                                                     | 19/10/2019 |
| [GitHub Game](https://github.com/Manut38/gyrogame-unity)                                                 | 19/10/2019 |
| [(Use Case Diagram)]() | 21/10/2018 |

### 1.5 Overview

The next chapters provide information about our vision based on the use case diagram as well as more detailed software requirements.

## 2. Overall Description

### 2.1 Vision

The goal of Gyro Game is to create a new kind of game for gamers that are also makers. Users will have the possibility to follow a guide and build their own game controller. When they did so correctly they will be able to play the game.

## 2.2 Product perspective

Our Use-Case-Diagram

![UseCaseDiagram](https://github.com/Manut38/gyrogame-unity/blob/master/Documentation/UCD.png)

### 2.3 User characteristics

Our main target group consists of people who like to play video games but also like to build electronics projects.

### 2.4 Dependencies

!!FlashCardCommunity depends on a database where all flashcards data is stored.

## 3. Specific Requirements

### 3.1 Functionality - The Controller

The game uses the controller as a key component. Without it the game can not be played. The controller uses a gyroscope to sense its orientation, this data will be used to solve puzzles in game.
The controller will have a telemetry to show the remaining battery.

### 3.2 Functionality – The Game

The game will be a first person puzzle game. The user has to solve puzzles via interaction with the controller. The puzzles will consist of blocked paths which will have to be bypassed by rotating objects, rooms or gravity with the cube. These will be color coded. In addition clues about solving approaches can be given via the lighting output of the controller.

### 3.3 Usability

With very few user inputs eg. rotating the cube, walking around and looking around, the game will be intuitive to play. Also there will be at most a minimalistic user interface, to keep the game as simple as possible.

#### 3.4.1 Availability

The game will be available to everyone for free, which includes the building instuctions. The controller has to be built by everyone who wants to play the game.


#### 3.4.2 Bugs

We classify bugs like the following:

-   **Critical bug**: An error that crashes the game or hinders the player from progressing any further. 
-   **Non critical bug**: An issue that will not create a gamebraking experience like shading issues or misscommunication with the controller.

### 3.5 Performance

Unity has a baseline hardware requirement that must be met for it to work. However the game will not be an AAA title thus most computers will work.

### 3.6 Supportability

Once the game is finished it wont be supported by us anymore. It is an open source project, so anyone who has imporvements will be able to implement them und update the game.

### 3.7 Design Constraints

Due to limited time and resources we will keep the graphics simple and minimalistic, to reduce the time of 3d modelling and focus more on level design

#### 3.7.1 Development tools

-   Git: version control system
-   Youtrack: time management application and sprint management
-   Unity: game engine
-   Arduino IDE: Microcontroller IDE
-   Visualstudio: Unity IDE

#### 3.7.2 GIT



#### 3.7.3 ReactJS

ReactJS helps building interactive UIs that can be updated dynamically and therefore eliminate the need to refresh the web application. One can also develop single components and can reuse them all over the application. Such a component could be a login form, a profile card or anything else one wants to reuse. We are going to import a React framework called Material-UI that provides a lot of pre-defined components. Its design based on, oh wonder, the Material-Design. The development will
take place with the newest version of JavaScript. Fortunately, our development environment is able to compile it to the lower version of JavaScript. Thus, we can
use the newest features without having to worry about browser compatibility.
Furthermore we will extend ReactJS with Redux. Redux allows us to keep track of state changes in the frontend and is able to notify other components about it. For further reading see [here](https://github.com/phoenixfeder/fc-com/blob/master/SoftwareArchitectureDocument.md#2-architectural-representation).

#### 3.7.4 Supported Platforms

Since FlashCardCommunity will be a web application the user only needs a modern web browser and a stable internet connection. With modern web browser we mean the
current versions of Mozilla Firefox, Google Chrome, Opera, Edge and even IE down to version 9 will be supported!

### 3.8 Online User Documentation and Help System Requirements

We want a provide a F.A.Q. for possible questions that can come up when using our application. Since it can be frustrating when a F.A.Q. doesn't really help with your answering problems, we want each F.A.Q. to be easy to understand and follow. For example a F.A.Q. that answers how to create a flashcard will include step-by-step
instructions and enough pictures to show the user exactly what to click at.

### 3.9 Purchased Components

-   We won't list any components as purchased to be able to keep self built things.

### 3.10 Interfaces

#### 3.10.1 User Interfaces

Our User Interface will provide one page for each implemented functionality.
To navigate between these sites the user will find a menu bar at the sides.

Since we are using the Material UI framework the application will be accessible from either desktop or mobile browsers.

#### 3.10.2 Hardware Interfaces

-   N\\A

#### 3.10.3 Software Interfaces

Our backend implements a REST-API, whose URLs our frontend can invoke with Http-Requests.
We will prepare base URLs for data concerning the user as well as the flashcards. For each base URL one can choose to either receive the data to show them on the website or push new data to our backend.

These data will be processed by our backend and then passed to our database.
The connection between our backend and the database will be managed by Hibernate.

#### 3.10.4 Communications Interfaces

Each HTTP-Request and Response contains a JSON.
By  interpreting its content our system will be able to transfer all needed data between front- and backend.

### 3.11 Licensing Requirements

Our project runs under the MIT License. This way everyone is allowed to create his own version.

### 3.12 Legal, Copyright and other Notices

-   N\\A

### 3.13 Applicable Standards

-   N\\A

## 4. Supporting Information

For a better overview, watch the table of contents and/or references.
