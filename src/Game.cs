// 主循环
using System;
using System.Timers;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace RetroSnaker
{
    class Game
    {
        private Scene scene;
        private Timer timer;
        private Timer sTimer;
        private DateTime startTime;
        private long frame = 0;
        private long sFrame = 0;
        public async Task Run(){
            
            this.scene = new Scene();
            Action work = () => {
                Operate.Register();
            };
            this.startTime = DateTime.Now;
            this.timer = new Timer(1000d / Global.Fps);
            this.timer.Elapsed += LoopHandler;
            this.timer.AutoReset = false;
            this.timer.Enabled = true;
            this.timer.Start();

            this.sTimer = new Timer(1000d);
            this.sTimer.Elapsed += sHandler;
            this.sTimer.AutoReset = true;
            this.sTimer.Enabled = true;
            this.sTimer.Start();

            await Task.Run(work);
        }
        private void LoopHandler(Object source, ElapsedEventArgs e){
            this.timer.AutoReset = false;
            Global.Event.emit(EventName.BeforeUpdate,new EventArgsFrame(this.frame));
            this.scene.runLoop();
            Global.Event.emit(EventName.AfterUpdate,new EventArgsFrame(this.frame));
            var now = DateTime.Now;
            double s = 1000d / Global.Fps;
            double frameSpan = ((now - this.startTime).TotalMilliseconds - this.frame * s);
            if (frameSpan >= s) {
                this.timer.Interval = 0.01d;
            } else {
                this.timer.Interval = frameSpan = s;
            }
            this.frame += 1L;
            this.timer.AutoReset = true;
        }
        private void sHandler(Object source, ElapsedEventArgs e){
            Global.Log($"FPS {this.frame - this.sFrame}");
            this.sFrame = this.frame;
        }
    }
}
