// 全局变量
using System;
namespace RetroSnaker
{
    
    delegate void Function();
    class Global
    {
        // 蛇移动速度即为1帧1格
        static public int Fps {private set; get;} = 1;
    }
}
