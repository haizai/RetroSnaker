using System;
namespace RetroSnaker {
    class EventArgsFrame:EventArgs {
        public int frame;
        public EventArgsFrame(int frame){this.frame = frame;}
    }
    class EventArgsDir:EventArgs {
        public Dir dir;
        public EventArgsDir(Dir dir){this.dir = dir;}
    }

}