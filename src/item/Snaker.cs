// 主角蛇
using System;
using System.Collections.Generic;
namespace RetroSnaker
{
    class Snaker:IItem
    {
        public Dir dir = Dir.Right;
        private List<Cell> cellList = new List<Cell>();
        private HeadCell headCell;
        public bool hideHead = false;
        public Snaker() {
            headCell = new HeadCell(7,1,Dir.Right);
            this.cellList.Add(headCell);
            this.cellList.Add(new Cell(6,1,Dir.Right));
            this.cellList.Add(new Cell(5,1,Dir.Right));
            this.cellList.Add(new Cell(4,1,Dir.Right));
            this.cellList.Add(new Cell(3,1,Dir.Right));
            this.cellList.Add(new Cell(2,1,Dir.Right));
            this.cellList.Add(new Cell(1,1,Dir.Right));

            Global.Event.addEventListener(EventName.TurnDir,this.OnTurnDir);
        }
        public DrawData[] Draw() {
            var data = new DrawData[this.cellList.Count];
            for (int i = 0; i < this.cellList.Count; i++) {
                data[i] = this.cellList[i].Draw();
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
        private void OnTurnDir(Object sender, EventArgs e) {
            var e1 = (EventArgsDir)e;
            this.willTurn = e1.dir;
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
        public Pos GetHeadPos(){
            return this.headCell.GetPos();
        }
        public void KnockWall(){
            this.headCell.KnockWall();
        }
    }
}
