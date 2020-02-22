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
        public Scene() {
            Global.Event.addEventListener(EventName.AfterUpdate,this.OnAfterUpdate);
            map = new Map(40,80);
            snaker = new Snaker();
            this.itemList.Add(snaker);
            
            TestFun(2,()=>{
                snaker.Turn(Dir.Bottom);
            });
            TestFun(4,()=>{
                snaker.Turn(Dir.Left);
            });
            TestFun(6,()=>{
                snaker.Turn(Dir.Top);
            });
            TestFun(7,()=>{
                snaker.Turn(Dir.Right);
            });
            TestFun(10,()=>{
                snaker.Turn(Dir.Bottom);
            });
        }
        public void runLoop(){
            foreach (var item in this.itemList)
            {
                item.Update();
                var drawDatas = item.Draw();
                DebugMode.DebugArg(drawDatas);
                foreach (var drawData in drawDatas) {
                    this.map.AddNowDrawData(drawData);
                }
                DebugMode.Debug(this.map);
                this.map.DrawDiff();
            }
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
    }
}