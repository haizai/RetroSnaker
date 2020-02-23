using System;
namespace RetroSnaker
{    class Operate {
        static public void Register() {
            do {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow) {
                    Global.Event.emit(EventName.TurnDir,new EventArgsDir(Dir.Top));
                } else if (key.Key == ConsoleKey.LeftArrow) {
                    Global.Event.emit(EventName.TurnDir,new EventArgsDir(Dir.Left));
                } else if (key.Key == ConsoleKey.RightArrow) {
                    Global.Event.emit(EventName.TurnDir,new EventArgsDir(Dir.Right));
                }else if (key.Key == ConsoleKey.DownArrow) {
                    Global.Event.emit(EventName.TurnDir,new EventArgsDir(Dir.Bottom));
                }
            } while(true);

        }
    }
}