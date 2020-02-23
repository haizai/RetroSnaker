using System;
namespace RetroSnaker {
    class EventArgsFrame:EventArgs {
        public int frame;
    }
    class EventArgsDir:EventArgs {
        public Dir dir;
    }

}