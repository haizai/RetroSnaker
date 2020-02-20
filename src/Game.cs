using System;
using System.Timers;
using System.Threading.Tasks;
namespace RetroSnaker
{
    class Game
    {
        private Timer timer;
        public async Task Run(){
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

        }
    }
}
