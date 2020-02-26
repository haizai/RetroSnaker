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
        private Star star;
        private int freeCount;
        public Scene() {
            Global.Event.addEventListener(EventName.AfterUpdate,this.OnAfterUpdate);
            Global.Event.addEventListener(EventName.Reset,this.OnReset);
            this.Init();
        }
        private void Init(){
            map = new Map(Global.Width,Global.Height);
            freeCount = (Global.Width-2) * (Global.Height-2);
            snaker = new Snaker();
            this.itemList.Add(snaker);
            wall = new Wall(Global.Width,Global.Height);
            this.itemList.Add(wall);
            star = new Star(8,4);
            this.itemList.Add(star);
            Global.State = GameState.InGame;
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
            this.Init();
        }
        // 处理不同item的事件， item内部的事件在update中就处理了
        private void ResolveConflict(){
            var headPos = this.snaker.GetHeadPos();
            if (wall.IsWall(headPos)) {
                Global.State = GameState.KnockWall;
                wall.Knock(headPos);
                snaker.KnockWall();
                return;
            }
            if (star.IsHit(headPos)) {
                snaker.HitStar();
                star.SetStar(GetRandomFreePos(snaker.GetAllPosList()));
            }
        }
        private Pos GetRandomFreePos(List<Pos> snakerPosList) {
            Random rnd = new Random();
            int val = rnd.Next(0,freeCount - snakerPosList.Count);
            int a = 0;
            int b = 0;
            for (int i = 1; i < Global.Width - 1;i++) {
                for (int j = 1; j < Global.Height - 1;j++) {
                    var p = new Pos(i,j);
                    if (snakerPosList.Contains(p)) {
                        b++; 
                    } else {
                        if (a == val) {
                            return p;
                        } else {
                            a++;
                        }
                    }
                }
            }
            return new Pos(0,0);
        }
    }
}