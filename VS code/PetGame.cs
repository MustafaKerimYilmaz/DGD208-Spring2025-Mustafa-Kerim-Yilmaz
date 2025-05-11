using System;
using System.Threading.Tasks;

namespace PetSimulatorGame   
{
    public class Game
    {
        public async Task GameLoop()
        {
            Console.WriteLine("Game started, Good Luck!");
            // basic loop for game.
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Turn {i + 1}...");
                await Task.Delay(2000); // 2 sec. delay
            }

            Console.WriteLine("Game ended, It seems you're a bit of a beginner!");
        }
    }
}
