// 场景层
using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class Map:IDebug {
        public int width;
        public int height;
        private Dictionary<int, Dictionary<int, DrawData>> nowMapData = new Dictionary<int, Dictionary<int, DrawData>>();
        private Dictionary<int, Dictionary<int, DrawData>> lastMapData = new Dictionary<int, Dictionary<int, DrawData>>();
        public Map(int width,int height){
            this.width = width;
            this.height = height;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++){
                    if (!this.nowMapData.ContainsKey(i)) {
                        this.nowMapData[i] = new Dictionary<int, DrawData>();
                    }
                    this.nowMapData[i][j] = new DrawData(i,j);

                    if (!this.lastMapData.ContainsKey(i)) {
                        this.lastMapData[i] = new Dictionary<int, DrawData>();
                    }
                    this.lastMapData[i][j] = new DrawData(i,j);
                
                }
            }
        }
        public void AddNowDrawData(DrawData data) {
            this.nowMapData[data.x][data.y].cha = data.cha;
            this.nowMapData[data.x][data.y].foreColor = data.foreColor;
        }
        public void DrawDiff() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++){
                    if (!this.lastMapData[i][j].Equals(this.nowMapData[i][j])) {
                        this.DrawAt(this.nowMapData[i][j]);
                        this.lastMapData[i][j].cha = this.nowMapData[i][j].cha;
                        this.lastMapData[i][j].foreColor = this.nowMapData[i][j].foreColor;
                    }
                    this.nowMapData[i][j].Clear();
                }
            }
        }
        private void DrawAt(DrawData drawData) {
            Console.SetCursorPosition(drawData.x*2,drawData.y);
            Console.ForegroundColor = drawData.foreColor;
            Console.Write(drawData.cha);
        }

        public Object Debug(){
            var ret = new Dictionary<string,List<Dictionary<string,Object>>>();
            var nowList = new List<Dictionary<string,Object>>();
            var lastList = new List<Dictionary<string,Object>>();
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++){
                    if (this.nowMapData[i][j].cha != ' ') {
                        var d = new Dictionary<string, Object>();
                        d["x"] = this.nowMapData[i][j].x;
                        d["y"] = this.nowMapData[i][j].y;
                        nowList.Add(d);
                    }
                    if (this.lastMapData[i][j].cha != ' ') {
                        var d = new Dictionary<string, Object>();
                        d["x"] = this.lastMapData[i][j].x;
                        d["y"] = this.lastMapData[i][j].y;
                        lastList.Add(d);
                    }
                }
            }
            ret["now"] = nowList;
            ret["last"] = lastList;
            return ret;
        }
    }
}
