// 物体基本接口
namespace RetroSnaker {
    interface IItem:IClear {
        DrawData[] Draw();
        void Update();
    }
    interface IClear {
        void Clear();
    }
}