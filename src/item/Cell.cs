using System.Collections.Generic;
namespace RetroSnaker {
    class Cell {
        public int x;
        public int y;
        public Dir dir;
        private List<TurnData> turnList = new List<TurnData>();
        public bool isHead = false;
        public Cell(int x, int y, Dir dir) {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
        public void Update(){
            Dir? _dir = null;
            for (int i = this.turnList.Count-1; i>=0; i--)
            {          
                var turn = this.turnList[i];   
                turn.frame--;
                if (turn.frame == 0){
                    _dir = turn.turn;
                    this.turnList.Remove(turn);
                }
            }
            if (_dir != null) {
                if (
                    _dir == Dir.Left && this.dir != Dir.Right ||
                    _dir == Dir.Right && this.dir != Dir.Left ||
                    _dir == Dir.Top && this.dir != Dir.Bottom ||
                    _dir == Dir.Bottom && this.dir != Dir.Top
                ) {
                    this.dir = (Dir)_dir;
                }
            }
            switch (this.dir) {
                case Dir.Left: this.x--; break;
                case Dir.Right: this.x++; break;
                case Dir.Top: this.y--; break;
                case Dir.Bottom: this.y++; break;
            }
        }
        public void Turn(Dir dir, int frame) {
            this.turnList.Add(new TurnData(){turn = dir, frame = frame});
        }
    }
}
