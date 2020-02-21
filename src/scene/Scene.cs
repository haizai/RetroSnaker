// 场景层
using System;
using System.Collections.Generic;
using System.Timers;
namespace RetroSnaker
{
    class Scene
    {
        private List<IItem> itemList = new List<IItem>();
        public Scene() {
            var snaker = new Snaker();
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
            Console.Clear();
            foreach (var item in this.itemList)
            {
                item.Update();
                var drawDatas = item.Draw();
                foreach (var drawData in drawDatas) {
                    Console.SetCursorPosition(drawData.x*2,drawData.y);
                    Console.ForegroundColor = drawData.foreColor;
                    Console.Write(drawData.cha);
                }
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