enum Dir {
    Left,
    Right,
    Top,
    Bottom,
}
enum GameState {
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