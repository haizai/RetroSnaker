using System;
namespace RetroSnaker {
    class EventArgsFrame:EventArgs {
        public long frame;
        public EventArgsFrame(long frame){this.frame = frame;}
    }
    class EventArgsDir:EventArgs {
        public Dir dir;
        public EventArgsDir(Dir dir){this.dir = dir;}
    }

}