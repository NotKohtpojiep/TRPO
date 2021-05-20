using System.Drawing;

namespace PingPong.Game.Models
{
    public class Ball : GameObject
    {
        private uint _radius;

        public Ball(uint radius, int speedX = 1, int speedY = 1, int posX = 0, int posY = 0, Color? backColor = null) : base(radius * 2, radius * 2, posX, posY, speedX, speedY, backColor)
        {
            _radius = radius;
        }

        public uint Radius
        {
            get => _radius;
        }
    }
}
