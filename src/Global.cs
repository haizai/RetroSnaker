// 全局变量
using System;
namespace RetroSnaker
{
    
    delegate void Function();
    class Global
    {
        static public int Fps {private set; get;} = 60;
        static public int MoveFps = 12;// 表示蛇移动多少帧1格，越小越快
        static public int InitLeng = 3;// 表示蛇起始长度
        static public GameEvent Event = new GameEvent(); 
        static public int Width = 10;
        static public int Height = 10;
        static public GameState State = GameState.InGame;


        static public void Log(Object o) {
            Console.SetCursorPosition(0,0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(o);
        }
    }
}
