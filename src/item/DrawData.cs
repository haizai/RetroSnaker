// 渲染用数据
using System;
namespace RetroSnaker {
    class DrawData {
        public int x;
        public int y;
        public char cha;
        public ConsoleColor foreColor; 
        public DrawData(int _x, int _y , char _cha, ConsoleColor foreColor = ConsoleColor.Gray) {
            this.x = _x;
            this.y = _y;
            this.cha = _cha;
            this.foreColor = foreColor;
        }
    }
}