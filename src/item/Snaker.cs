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
            headCell = new HeadCell(10,1,Dir.Right);
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
            this.TestKnockSelf();
        }
        private List<Dir> turnList = new List<Dir>();
        private void OnTurnDir(Object sender, EventArgs e) {
            if (Global.State == GameState.InGame) {
                var e1 = (EventArgsDir)e;
                if (this.turnList.Count == 0) {
                    if (this.IsAllowDir(e1.dir,this.dir)) {
                        this.turnList.Add(e1.dir);
                        return;
                    }
                } else {
                    if (this.IsAllowDir(e1.dir,this.turnList[this.turnList.Count-1])) {
                        this.turnList.Add(e1.dir);
                        return;
                    }
                }
            }
        }
        private bool IsAllowDir(Dir? nextDir, Dir? nowDir) {
            return  nextDir == Dir.Bottom && this.dir != Dir.Top ||
                    nextDir == Dir.Top && this.dir != Dir.Bottom ||
                    nextDir == Dir.Left && this.dir != Dir.Right ||
                    nextDir == Dir.Right && this.dir != Dir.Left;
        }
        private void UpdateTurn() {
            if (this.turnList.Count == 0) {
                return;
            }
            var newTurn = this.turnList[0];
            this.turnList.RemoveAt(0);
            if (IsAllowDir(newTurn,this.dir)) {
                for (int i = 0; i < this.cellList.Count; i++) {
                    this.cellList[i].Turn(newTurn, i+1);
                }
                this.dir = newTurn;
            }
        }
        public Pos GetHeadPos(){
            return this.headCell.GetPos();
        }
        public void KnockWall(){
            this.headCell.Knock();
        }
        private void TestKnockSelf() {
            foreach(var cell in this.cellList) {
                if (!(cell is HeadCell)) {
                    if (cell.x == this.headCell.x && cell.y == this.headCell.y) {
                        Global.State = GameState.KnockSelf;
                        this.cellList.Remove(cell);
                        this.headCell.Knock();
                        break;
                    }
                }
            }
        }
    }
}
