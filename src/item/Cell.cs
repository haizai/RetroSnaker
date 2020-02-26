// 蛇上方块
using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class Cell {
        public int x;
        public int y;
        public char cha = '▇';
        public ConsoleColor foreColor = ConsoleColor.Gray;
        public Dir dir;
        protected List<TurnData> turnList = new List<TurnData>();
        public Cell(int x, int y, Dir dir) {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
        public Cell(int x, int y, Dir dir,List<TurnData> turnList) {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.turnList = turnList;
        }
        public List<TurnData> CloneTurnList() {
            var list = new List<TurnData>();
            foreach (var item in this.turnList)
            {
                list.Add(new TurnData(){turn = item.turn, frame = item.frame});
            }
            return list;
        }
        public Pos GetPos(){
            return new Pos(x,y);
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
        public DrawData Draw(){
            return new DrawData(x, y, cha, foreColor);
        }
        public string Debug(){
            var turnStr = "";
            foreach(var t in turnList) {
                turnStr += $"turn={t.turn},frame={t.frame};";
            }
            return $"x={x},y={y},dir={dir},list={turnStr}";
        }
    }
}
