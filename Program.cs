using System;

namespace RetroSnaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            
            game.Run().Wait();
        }
    }
}
