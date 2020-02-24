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
            Global.Event.addEventListener(EventName.Reset,this.OnReset);
            this.Init();
        }
        private void Init(){
            map = new Map(Global.Width,Global.Height);
            snaker = new Snaker();
            this.itemList.Add(snaker);
            wall = new Wall(Global.Width,Global.Height);
            this.itemList.Add(wall);
        }

        public void runLoop(){
            if (Global.State != GameState.InGame) {
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
        private Dictionary<long, Function> testDic = new Dictionary<long,Function>();
        private void TestFun(long frame, Dir dir) {
            this.testDic[frame] = () => {
                Global.Event.emit(EventName.TurnDir,new EventArgsDir(dir));
            };
        }
        private void OnAfterUpdate(Object sender, EventArgs e) {
            var e1 = (EventArgsFrame)e;
            if (this.testDic.ContainsKey(e1.frame)) {
                this.testDic[e1.frame]();
            }
        }
        private void OnReset(Object sender,EventArgs e) {
            foreach (var item in this.itemList){
                item.Clear();
            }
            this.itemList.Clear();
            this.map.Clear();
            this.map = null;
            Global.State = GameState.InGame;
            this.Init();
        }
        // 处理不同item的事件， item内部的事件在update中就处理了
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