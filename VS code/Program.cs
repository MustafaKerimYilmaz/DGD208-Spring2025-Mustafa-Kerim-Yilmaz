using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {
        var game = new Game();
        await game.GameLoop();
    }
}
