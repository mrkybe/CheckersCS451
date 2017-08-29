# CheckersCS451
A client/server system for playing checkers online written in C#.

## Executing the Project As-Is
Included with the source is a Release.zip folder. In this, there are two sub-folders: Client and Server

### Client
The Client folder contains the code required for a Checkers client to be run. The Client window is the game board and is used to connect to the server and play Checkers.

### Server
The Server folder contains the code required to run a local Checkers server. The Server will provide the IPv4 as part of the listening endpoint when it starts. Clients can use the specified endpoint to connect to the Checkers Server and play the game. The Server does support multiple parallel games to be run at once, and a game will start once two players are connected.

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
