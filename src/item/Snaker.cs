// 主角蛇
using System;
using System.Collections.Generic;
namespace RetroSnaker
{
    class Snaker:IItem,IDebug
    {
        private int nowFps = 0;
        public Dir dir = Dir.Right;
        private List<Cell> cellList = new List<Cell>();
        private HeadCell headCell;
        public bool hideHead = false;
        public Snaker() {
            headCell = new HeadCell(Global.Config.InitLeng,1,Dir.Right);
            this.cellList.Add(headCell);
            for (int i = Global.Config.InitLeng - 1; i > 0; i--) {
                this.cellList.Add(new Cell(i,1,Dir.Right));
            }
            Log();
            Global.Event.addEventListener(EventName.TurnDir,this.OnTurnDir);
        }
        public DrawData[] Draw() {
            var data = new DrawData[this.cellList.Count];
            for (int i = 0; i < this.cellList.Count; i++) {
                data[i] = this.cellList[i].Draw();
            }
            return data;
        }
        private Pos lastPos;
        private Dir lastDir;
        private List<TurnData> lastTurnList;
        private int MoveCount = 0;
        public void Update() {
            if (this.nowFps<Global.Config.MoveFps) {
                this.nowFps++;
                return;
            }
            MoveCount += 1;
            Log();
            this.UpdateTurn();
            var lastCell = this.cellList[this.cellList.Count - 1];
            this.lastPos = new Pos(lastCell.x, lastCell.y);
            this.lastDir = lastCell.dir;
            this.lastTurnList = lastCell.CloneTurnList();
            this.nowFps = 0;
            for (int i = 0; i < this.cellList.Count; i++) {
                this.cellList[i].Update();
            }
            this.TestKnockSelf();
            
            // DebugMode.Debug(this);
        }
        private Dir? nextDir = null;
        private List<Dir> turnList = new List<Dir>();
        private void OnTurnDir(Object sender, EventArgs e) {
            if (Global.State == GameState.InGame) {
                var e1 = (EventArgsDir)e;
                // if (this.turnList.Count == 0) {
                //     if (this.IsAllowDir(e1.dir,this.dir)) {
                //         this.turnList.Add(e1.dir);
                //         this.nowFps = this.moveFps;
                //         return;
                //     }
                // } else {
                //     if (this.IsAllowDir(e1.dir,this.turnList[this.turnList.Count-1])) {
                //         this.turnList.Add(e1.dir);
                //         return;
                //     }
                // }
                if (!this.nextDir.HasValue) {
                    if (this.IsAllowDir(e1.dir,this.dir)) {
                        this.nextDir = e1.dir;
                        this.nowFps = Global.Config.MoveFps;
                        return;
                    }
                }
            }
        }
        private bool IsAllowDir(Dir nextDir, Dir nowDir) {
            if (nextDir == Dir.Bottom || nextDir == Dir.Top) {
                if (nowDir == Dir.Top || nowDir == Dir.Bottom) {
                    return false;
                }
            }
            if (nextDir == Dir.Left || nextDir == Dir.Right) {
                if (nowDir == Dir.Left || nowDir == Dir.Right) {
                    return false;
                }
            }
            return true;
        }
        private void UpdateTurn() {
            // if (this.turnList.Count == 0) {
            //     return;
            // }
            // var newTurn = this.turnList[0];
            // this.turnList.RemoveAt(0);
            // if (IsAllowDir(newTurn,this.dir)) {
            //     for (int i = 0; i < this.cellList.Count; i++) {
            //         this.cellList[i].Turn(newTurn, i+1);
            //     }
            //     this.dir = newTurn;
            // }
            if (!this.nextDir.HasValue) {
                return;
            }
            if (IsAllowDir(this.nextDir.Value,this.dir)) {
                for (int i = 0; i < this.cellList.Count; i++) {
                    this.cellList[i].Turn(this.nextDir.Value, i+1);
                }
                this.dir = this.nextDir.Value;
            }
            this.nextDir = null;
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
        public List<Pos> GetAllPosList(){
            var posList = new List<Pos>();
            for (int i = 0; i < this.cellList.Count; i++) {
                posList.Add(new Pos(this.cellList[i].x,this.cellList[i].y));
            }
            return posList;
        }
        public void Clear() {
            this.cellList.Clear();
            this.headCell = null;
            Global.Event.removeEventListener(EventName.TurnDir,this.OnTurnDir);
        }
        public void HitStar() {
            this.cellList.Add(new Cell(lastPos.x,lastPos.y,lastDir,lastTurnList));
            Log();
        }
        public Object Debug(){
            var list = new List<string>();
            foreach(var cell in cellList) {
                list.Add(cell.Debug());
            }
            var lastStr = "[Last]";
            foreach(var t in lastTurnList) {
                lastStr += $"turn={t.turn},frame={t.frame};";
            }
            list.Add($"x={lastPos.x},y={lastPos.y},dir={lastDir},list={lastStr}");
            return list;
        }
        private void Log() {
            Global.Log($"Length {this.cellList.Count} Move {MoveCount}");
        }
    }
}
