using System.Drawing;

namespace PingPong.Game.Models
{
    public class Platform : GameObject
    {
        public Platform(uint height, uint width, int speed = 1, int posX = 0, int posY = 0, Color? backColor = null) : base(width, height, posX, posY, speed, speedY: 0, backColor)
        {
        }

        public override void Move(int minX, int maxX, int minY, int maxY)
        {
            if (PosX + SpeedX < minX)
            {
                PosX = minX;
                return;
            }
            if (PosX + SizeX + SpeedX > maxX)
            {
                PosX = maxX - (int)SizeX;
                return;
            }
            PosX += SpeedX;
        }
    }
}
