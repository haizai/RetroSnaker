enum Dir {
    Left,
    Right,
    Top,
    Bottom,
}
enum GameState {
    InGame,
    KnockWall, // 撞墙
    End,
}

struct Pos {
    public int x;
    public int y;
    public Pos(int x,int y) {
        this.x = x;
        this.y = y;
    }
}