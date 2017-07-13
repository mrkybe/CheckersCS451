using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    // Gamemaster - Maintains that the game rules are followed, turns taken in correct order,
    //              initializses game state, resolves the outcome of commands from players
    //            - Knows about PLAYER1 - PLAYERX
    //
    // Server     - Maintains connections to players's client, matches the player's client to a Gamemaster, 
    //              pipes messages from player's client to correct Gamemaster, sends player's client the updated game state.
    //
    // Client     - Connects to the server, displays the current game state, turns user input into simple command that can be sent to the server for the Gamemaster.
    public class Game
    {
    }
}