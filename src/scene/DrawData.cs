// 渲染用数据
using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class DrawData:IDebug {
        public int x;
        public int y;
        public char cha;
        public ConsoleColor foreColor; 
        public DrawData(int _x, int _y , char _cha = ' ', ConsoleColor foreColor = ConsoleColor.Gray) {
            this.x = _x;
            this.y = _y;
            this.cha = _cha;
            this.foreColor = foreColor;
        }
        public bool Equals(DrawData other) {
            if (
                other.x == x &&
                other.y == y &&
                other.cha == cha &&
                other.foreColor == foreColor
            ) {
                return true;
            } 
            return false;
        }
        public void Clear() {
            cha = ' ';
            foreColor = ConsoleColor.Gray;
        }
        public Object Debug(){
            var d = new Dictionary<string,Object>();
            d["x"] = x;
            d["y"] = y;
            d["cha"] = cha;
            return d;
        }
    }
}