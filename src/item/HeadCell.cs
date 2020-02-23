// 头方块
using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class HeadCell: Cell {
        public HeadCell(int x, int y, Dir dir): base(x,y,dir){
            this.foreColor = ConsoleColor.Yellow;
        }
        public void KnockWall(){
            this.foreColor = ConsoleColor.Red;
        }
    }
}