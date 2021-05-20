using System;
using System.Collections.Generic;
using System.Linq;

namespace PingPong.Game.Models
{
    public class Playfield
    {
        private uint _sizeX;
        private uint _sizeY;
        private List<GameObject> _gameObjects;

        public delegate void BallIsOut(bool isOnTop);
        public event BallIsOut OnOut;

        public Playfield(uint sizeX, uint sizeY, GameObject[] gameObjects = null)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _gameObjects =
                gameObjects == null ? new List<GameObject>() : gameObjects.ToList();
        }

        public GameObject[] GameObjects
        {
            get => _gameObjects.ToArray();
        }

        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void MoveGameObjects()
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i].GetType() == typeof(Ball))
                {
                    if (IsGameObjectOn(_gameObjects[i], typeof(Platform)))
                    {
                        _gameObjects[i].SpeedX *= -1.03;
                        _gameObjects[i].SpeedY *= -1;
                    }

                    bool? isBallOut = IsGameObjectOut(_gameObjects[i]);

                    if (isBallOut != null)
                    {
                        //OnOut?.Invoke((bool)isBallOut);
                        //return;
                    }
                }
                _gameObjects[i].Move(0, (int)_sizeX, 0, (int)_sizeY);
            }
        }

        private bool? IsGameObjectOut(GameObject gameObject)
        {
            if (gameObject.PosY + gameObject.SpeedY <= 0)
            {
                return true;
            }
            if (gameObject.PosY + gameObject.SpeedY + gameObject.SizeY >= _sizeY)
            {
                return false;
            }

            return null;
        }

        private bool IsGameObjectOn(GameObject gameObject, Type expectedGameObject)
        {
            int sizeX = (int)gameObject.SizeX;
            int sizeY = (int)gameObject.SizeY;

            GameObject[] platforms = _gameObjects.Where(x => x.GetType() == expectedGameObject)
                .Where(x => gameObject.PosX + sizeX / 2 >= x.PosX && gameObject.PosX <= x.PosX + x.SizeX).ToArray();

            Func<GameObject, bool> wherePosY;
            if (gameObject.SpeedY < 0)
                wherePosY = x =>
                    gameObject.PosY >= x.PosY && gameObject.PosY <= x.PosY + x.SizeY;
            else
                wherePosY = x =>
                    gameObject.PosY + sizeY >= x.PosY && gameObject.PosY + sizeY <= x.PosY + x.SizeY;
            platforms = platforms.Where(wherePosY).ToArray();

            if (platforms.Length > 0)
            {
                return true;
            }

            return false;
        }
    }
}
