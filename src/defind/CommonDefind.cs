
namespace RetroSnaker {
    enum Dir {
        Left,
        Right,
        Top,
        Bottom,
    }
    enum GameState {
        InitConfig, // 初始化配置
        InGame,
        End,
        KnockWall, // 撞墙
        KnockSelf, // 撞尾
    }

    struct Pos {
        public int x;
        public int y;
        public Pos(int x,int y) {
            this.x = x;
            this.y = y;
        }
    }
    struct InitConfig {
        public int Width;
        public int Height;
        public int InitLeng;
        public int MoveFps;
    }
}
