// 主角蛇
using System;
using System.Collections.Generic;
namespace RetroSnaker
{
    class Snaker:IItem
    {
        public Dir dir = Dir.Right;
        private List<Cell> cellList = new List<Cell>();
        public Snaker() {
            var headCell = new Cell(10,1,Dir.Right);
            headCell.isHead = true;
            this.cellList.Add(headCell);
            this.cellList.Add(new Cell(9,1,Dir.Right));
            this.cellList.Add(new Cell(8,1,Dir.Right));
            this.cellList.Add(new Cell(7,1,Dir.Right));
            this.cellList.Add(new Cell(6,1,Dir.Right));
            this.cellList.Add(new Cell(5,1,Dir.Right));
            this.cellList.Add(new Cell(4,1,Dir.Right));
            this.cellList.Add(new Cell(3,1,Dir.Right));
            this.cellList.Add(new Cell(2,1,Dir.Right));
            this.cellList.Add(new Cell(1,1,Dir.Right));
        }
        public DrawData[] Draw() {
            var data = new DrawData[this.cellList.Count];
            for (int i = 0; i < this.cellList.Count; i++) {
                if (this.cellList[i].isHead) {
                    data[i] = new DrawData(this.cellList[i].x, this.cellList[i].y, '▇', ConsoleColor.Yellow);
                } else {
                    data[i] = new DrawData(this.cellList[i].x, this.cellList[i].y, '▇');
                }
            }
            return data;
        }
        public void Update() {
            this.UpdateTurn();
            for (int i = 0; i < this.cellList.Count; i++) {
                this.cellList[i].Update();
            }
        }
        private Dir? willTurn = null;
        public void Turn(Dir _dir) {
            this.willTurn = _dir;
        }
        private void UpdateTurn() {
            if (this.willTurn.HasValue) {
                if (
                    this.willTurn == Dir.Bottom && this.dir != Dir.Top ||
                    this.willTurn == Dir.Top && this.dir != Dir.Bottom ||
                    this.willTurn == Dir.Left && this.dir != Dir.Right ||
                    this.willTurn == Dir.Right && this.dir != Dir.Left
                ) {
                    for (int i = 0; i < this.cellList.Count; i++) {
                        this.cellList[i].Turn(this.willTurn.Value, i+1);
                    }
                    this.dir = this.willTurn.Value;
                }
            }
            this.willTurn = null;
        }
    }
}
