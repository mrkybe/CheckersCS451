# CheckersCS451
A client/server system for playing checkers online written in C#.

## Getting Started
Checkout the project, open it in Visual Studio.

## Solution Projects Explained
#### CheckersCS451
Client application for communicating with the server.  Displays the current game state.  Transforms user's interactions with the UI to a simple command that can be sent to the Gamemaster on the server.

#### CustomControls
Custom WinForms control for displaying an interactive game board for checkers.

#### CheckersServer
Server application that hosts the Gamemaster.  Matches a newly connected player's client to a Gamemaster.   Maintains connection to the player's clients.  Passes messages from the client to the appropriate Gamemaster.  Sends clients the updated game state.

#### Data
Gamemaster class is here.  Maintains that the game rules are followed, that turns are taken in the correct order, resolves the outcome of commands from players, initializses the game state.  Pretty much just the most bare-bones form of whatever game, in this case checkers.  Doesn't care its online.

## Authors
* Ruslan Kaybyshev
* Camden Marchetti
* Aurora Lane
* Yong Choi