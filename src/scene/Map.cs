// 场景层
using System;
using System.Collections.Generic;
namespace RetroSnaker {
    class Map:IDebug {
        public int width;
        public int height;
        private Dictionary<Pos,DrawData> nowMapData = new Dictionary<Pos,DrawData>();
        private Dictionary<Pos,DrawData> lastMapData = new Dictionary<Pos,DrawData>();
        public Map(int width,int height){
            this.width = width;
            this.height = height;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++){
                    this.nowMapData[new Pos(i,j)] = new DrawData(i,j);
                    this.lastMapData[new Pos(i,j)] = new DrawData(i,j);
                
                }
            }
        }
        public void AddNowDrawData(DrawData data) {
            var d = this.nowMapData[new Pos(data.x,data.y)];
            d.cha = data.cha;
            d.foreColor = data.foreColor;
        }
        public void DrawDiff() {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++){
                    var pos = new Pos(i,j);
                    if (!this.lastMapData[pos].Equals(this.nowMapData[pos])) {
                        this.DrawAt(this.nowMapData[pos]);
                        this.lastMapData[pos].cha = this.nowMapData[pos].cha;
                        this.lastMapData[pos].foreColor = this.nowMapData[pos].foreColor;
                    }
                    this.nowMapData[pos].Clear();
                }
            }
        }
        private void DrawAt(DrawData drawData) {
            Console.SetCursorPosition(drawData.x*2,drawData.y);
            Console.ForegroundColor = drawData.foreColor;
            Console.Write(drawData.cha);
        }

        public Object Debug(){
            // var ret = new Dictionary<string,List<Dictionary<string,Object>>>();
            // var nowList = new List<Dictionary<string,Object>>();
            // var lastList = new List<Dictionary<string,Object>>();
            // for (int i = 0; i < width; i++) {
            //     for (int j = 0; j < height; j++){
            //         if (this.nowMapData[i][j].cha != ' ') {
            //             var d = new Dictionary<string, Object>();
            //             d["x"] = this.nowMapData[i][j].x;
            //             d["y"] = this.nowMapData[i][j].y;
            //             nowList.Add(d);
            //         }
            //         if (this.lastMapData[i][j].cha != ' ') {
            //             var d = new Dictionary<string, Object>();
            //             d["x"] = this.lastMapData[i][j].x;
            //             d["y"] = this.lastMapData[i][j].y;
            //             lastList.Add(d);
            //         }
            //     }
            // }
            // ret["now"] = nowList;
            // ret["last"] = lastList;
            return "";
        }
    }
}
