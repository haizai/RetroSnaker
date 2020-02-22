// 全局变量
using System;
namespace RetroSnaker
{
    
    delegate void Function();
    class Global
    {
        // 蛇移动速度即为1帧1格
        static public int Fps {private set; get;} = 1;
        // 总帧数
        static public int Frame = 0;
        static public GameEvent Event = new GameEvent(); 
    }
}
