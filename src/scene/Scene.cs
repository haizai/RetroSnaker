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
        private void TestFun(int frame, Function cb) {
            var timer2 = new Timer(frame * 1000 / Global.Fps);
            timer2.Elapsed += (Object source, ElapsedEventArgs e) => {
                cb();
            };
            timer2.AutoReset = false;
            timer2.Enabled = true;
            timer2.Start();
        }
    }
}