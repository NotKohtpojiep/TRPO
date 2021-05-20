using System;
using System.Drawing;

namespace PingPong.Game.Models
{
    public class GameObject : IMovable
    {
        private uint _sizeX;
        private uint _sizeY;
        private double _posX;
        private double _posY;
        private double _speedX;
        private double _speedY;
        private Color? _backColor;

        public GameObject(uint sizeX, uint sizeY, int posX, int posY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _posX = posX;
            _posY = posY;
        }

        public GameObject(uint sizeX, uint sizeY, int posX, int posY, int speedX = 0, int speedY = 0, Color? backColor = null) : this(sizeX, sizeY, posX, posY)
        {
            _backColor = backColor;
            _speedX = speedX;
            _speedY = speedY;
        }

        public uint SizeX
        {
            get => _sizeX;
            set => _sizeX = value;
        }

        public uint SizeY
        {
            get => _sizeY;
            set => _sizeY = value;
        }

        public double PosX
        {
            get => _posX;
            set => _posX = value;
        }
        public double PosY
        {
            get => _posY;
            set => _posY = value;
        }

        public double SpeedX
        {
            get => _speedX;
            set => _speedX = value;
        }
        public double SpeedY
        {
            get => _speedY;
            set => _speedY = value;
        }
        public Color BackColor
        {
            get
            {
                if (_backColor != null)
                {
                    return (Color)_backColor;
                }
                return Color.Transparent;
            }
            set => _backColor = value;
        }

        /// <summary>
        /// Move object by speed to the end of the play field
        /// </summary>
        /// <returns>(PosX, PosY) after of moving</returns>
        public virtual void Move(int minX, int maxX, int minY, int maxY)
        {
            MovePos(ref _posX, ref _speedX, minX, maxX, SizeX);
            MovePos(ref _posY, ref _speedY, minY, maxY, SizeY);
        }

        private void MovePos(ref double pos, ref double speed, int minPos, int maxPos, uint size)
        {
            if (pos + speed < minPos)
            {
                pos = minPos;
                speed = Math.Abs(speed);
            }

            if (pos + size + speed > maxPos)
            {
                pos = maxPos - (int)size;
                speed = Math.Abs(speed) * -1;
            }
            pos += speed;
        }
    }
}
