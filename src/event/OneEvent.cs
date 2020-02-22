using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class OneEvent {
        private event EventHandler E;
        private EventName name;
        private List<EventHandler> handlerList = new List<EventHandler>();
        public OneEvent(EventName name) {
            this.name = name;
        }
        public void addEventListener(EventHandler f) {
            this.E += f;
            this.handlerList.Add(f);
        }
        public void removeEventListener(EventHandler f) {
            this.E -= f;
            this.handlerList.Remove(f);
        }
        public void removeEventListener() {
            foreach(var handler in handlerList) {
                this.E -= handler;
            }
        }
        public void emit(EventArgs e) {
            this.E(this, e);
        }
    }
}