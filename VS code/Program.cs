using System;
using System.Threading.Tasks;

namespace PetSimulatorGame
{
    class Program
    {
        static async Task Main(string[] args) // I wrote "async" so that R can use "await"
        { 
             Game game = new Game();
             await game.GameLoop();
        }
    }
}
