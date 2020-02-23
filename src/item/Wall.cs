using System;
using System.Collections.Generic;
using System.Linq;
namespace RetroSnaker
{
    class Wall:IItem
    {
        private int width;
        private int height;
        private Dictionary<Pos,DrawData> wallDic = new Dictionary<Pos,DrawData>();
        public Wall(int w, int h) {
            width = w;
            height = h;
            wallDic[new Pos(0,0)] = new DrawData(0,0,'╔');
            wallDic[new Pos(0,h-1)] = new DrawData(0,h-1,'╚');
            wallDic[new Pos(w-1,0)] = new DrawData(w-1,0,'╗');
            wallDic[new Pos(w-1,h-1)] = new DrawData(w-1,h-1,'╝');
            for (int i = 1; i < w - 1; i++) {
                wallDic[new Pos(i,0)] = new DrawData(i,0,'═');
                wallDic[new Pos(i,h-1)] = new DrawData(i,h-1,'═');
            }
            for (int j = 1; j < h - 1; j++) {
                wallDic[new Pos(0,j)] = new DrawData(0,j,'║');
                wallDic[new Pos(w-1,j)] = new DrawData(w-1,j,'║');
            }
        }
        public DrawData[] Draw() {
            return wallDic.Values.ToArray();
        }
        public void Update(){}
        public bool IsWall(Pos pos) {
            if (pos.x == 0 || pos.x == width - 1 || pos.y == 0 || pos.y == height - 1) {
                return true;
            }
            return false;
        }

        public void Knock(Pos pos) {
            wallDic.Remove(pos);
        }
    }
}