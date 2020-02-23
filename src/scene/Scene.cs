// 场景层
using System;
using System.Collections.Generic;
using System.Timers;
namespace RetroSnaker
{
    class Scene
    {
        private List<IItem> itemList = new List<IItem>();
        private Snaker snaker;
        private Map map;
        private Wall wall;
        public Scene() {
            Global.Event.addEventListener(EventName.AfterUpdate,this.OnAfterUpdate);
            map = new Map(Global.Width,Global.Height);
            snaker = new Snaker();
            this.itemList.Add(snaker);
            wall = new Wall(Global.Width,Global.Height);
            this.itemList.Add(wall);
            
            // TestFun(2,()=>{
            //     snaker.Turn(Dir.Bottom);
            // });
            // TestFun(4,()=>{
            //     snaker.Turn(Dir.Left);
            // });
            // TestFun(6,()=>{
            //     snaker.Turn(Dir.Top);
            // });
            // TestFun(7,()=>{
            //     snaker.Turn(Dir.Right);
            // });
            // TestFun(10,()=>{
            //     snaker.Turn(Dir.Bottom);
            // });
        }
        public void runLoop(){
            if (Global.State == GameState.KnockWall) {
                return;
            }
            foreach (var item in this.itemList)
            {
                item.Update();
            }
            this.ResolveConflict();
            foreach (var item in this.itemList)
            {
                var drawDatas = item.Draw();
                foreach (var drawData in drawDatas) {
                    this.map.AddNowDrawData(drawData);
                }
            }
            this.map.DrawDiff();
        }
        private Dictionary<int, Function> testDic = new Dictionary<int,Function>();
        private void TestFun(int frame, Function cb) {
            this.testDic[frame] = cb;
        }
        private void OnAfterUpdate(Object sender, EventArgs e) {
            var e1 = (EventArgsFrame)e;
            if (this.testDic.ContainsKey(e1.frame)) {
                this.testDic[e1.frame]();
            }
        }
        private void ResolveConflict(){
            var headPos = this.snaker.GetHeadPos();
            if (this.wall.IsWall(headPos)) {
                Global.State = GameState.KnockWall;
                this.wall.Knock(headPos);
                this.snaker.KnockWall();
                return;
            }
        }
    }
}