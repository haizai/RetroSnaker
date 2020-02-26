using System;
namespace RetroSnaker
{
    class Star:IItem
    {
        private int x;
        private int y;
        public Star(int x,int y) {
            this.x = x;
            this.y = y;
        }
        public DrawData[] Draw() {
            var d = new DrawData[1];
            d[0] = new DrawData(x,y,'★',ConsoleColor.Yellow);
            return d;
        }
        public bool IsHit(Pos pos) {
            if (x == pos.x && y == pos.y) {
                return true;
            }
            return false;
        }
        public void SetStar(Pos p) {
            x = p.x;
            y = p.y;
        }
        public void Update(){}
        public void Clear(){}
    }
}