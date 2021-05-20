namespace PingPong.Game
{
    public interface IMovable
    {
        /// <summary>
        /// Move object by speed to the end of play field
        /// </summary>
        /// <returns>(PosX, PosY) after of move</returns>
        void Move(int minX, int maxX, int minY, int maxY);
    }
}
