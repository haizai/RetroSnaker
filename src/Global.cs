// 全局变量
using System;
namespace RetroSnaker
{
    
    delegate void Function();
    class Global
    {
        static public int Fps {private set; get;} = 60;
        static public GameEvent Event = new GameEvent(); 
        static public int Width = 20;
        static public int Height = 20;
        static public GameState State = GameState.InGame;

        static public void Log(Object o) {
            Console.SetCursorPosition(0,0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(o);
        }
    }
}
