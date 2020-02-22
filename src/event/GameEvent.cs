using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class GameEvent {
        public void emit(EventName name, EventArgs e) {
            if (this.eventDic.ContainsKey(name)) {
                this.eventDic[name].emit(e);
            }
        }
        public void addEventListener(EventName name, EventHandler f) {
            if (!this.eventDic.ContainsKey(name)) {
                this.eventDic[name] = new OneEvent(name);
            }
            this.eventDic[name].addEventListener(f);
        }
        public void removeEventListener(EventName name, EventHandler f) {
            if (this.eventDic.ContainsKey(name)) {
                this.eventDic[name].removeEventListener(f);
            }
        }
        public void removeEventListener(EventName name) {
            if (this.eventDic.ContainsKey(name)) {
                this.eventDic[name].removeEventListener();
            }
        }
        public void removeEventListener() {
            foreach (var name in this.eventDic.Keys) {
                this.removeEventListener(name);
            }
        }
        public Dictionary<EventName,OneEvent> eventDic = new Dictionary<EventName, OneEvent>();
    }
}