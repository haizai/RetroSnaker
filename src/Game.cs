using System;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace RetroSnaker
{
    delegate void Function();
    class Game
    {
        private Timer timer;
        private List<IItem> itemList = new List<IItem>();
        public async Task Run(){
            Init();
            this.timer = new Timer(1000.0 / Global.Fps);
            this.timer.Elapsed += LoopHandler;
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            this.timer.Start();
            Action work = () => {
                do {

                } while(true);
            };
            await Task.Run(work);
        }
        private void LoopHandler(Object source, ElapsedEventArgs e){
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
        private void Init(){
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
