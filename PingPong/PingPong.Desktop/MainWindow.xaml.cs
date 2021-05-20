using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using PingPong.Game.Models;
using Color = System.Drawing.Color;

namespace PingPong.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Playfield _playfield;
        private DispatcherTimer _timer;
        private int _countOfTicks = 0;
        private readonly uint SizeX = 200;
        private readonly uint SizeY = 400;
        private bool isGameOver = false;

        public DateTime GameTime { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Init()
        {
            List<GameObject> gmObjList = new List<GameObject>();
            for (int i = 0; i < 100; ++i)
            {
                gmObjList.Add(getRandomBall(0,(int)SizeX,0,(int)SizeY));
            }
            GameObject ball1 = getRandomBall(0, (int)SizeX, (int)SizeX, (int)SizeY);
            GameObject platform1 = new Platform(50, 70, 0, posX: 50, posY: 300);
            GameObject platform2 = new Platform(15, 50, 0, posX: 0, posY: 20);
            GameObject[] gameObjects = new GameObject[] { platform1, platform2, ball1 };
            gmObjList.AddRange(gameObjects);
            _playfield = new Playfield(SizeX, SizeY, gmObjList.ToArray());
            _playfield.OnOut += OnBallOut;

            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 1) };
            _timer.Tick += timer_Tick;
            //_timer.Start();

            Thread thread = new Thread(UpdateTextWrong);
            thread.Start();
        }

        private Ball getRandomBall(int minPosX, int maxPosX, int minPosY, int maxPosY)
        {
            Random rand = new Random();
            uint radius = (uint)rand.Next(5, 20);
            int speedX = rand.Next(-5, 5);
            int speedY = rand.Next(-5, -1);
            int posX = rand.Next(minPosX, maxPosX);
            int posY = maxPosY / 2;
            return new Ball(radius, speedX, speedY, posX, posY, backColor: Color.Aquamarine);
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            ++_countOfTicks;
            if (_countOfTicks % 60 == 0)
            {
                GameTime = GameTime.AddSeconds(1);
                LabelTime.Content = GameTime.ToLongTimeString();
            }
            if (_countOfTicks >= 500)
            {
                GameObject ball1 = getRandomBall(0, (int)SizeX, (int)SizeX, (int)SizeY);
                _playfield.AddGameObject(ball1);
                _countOfTicks = 0;
            }

            UpdatePlayfield();
        }

        private void PutObject(GameObject gameObject)
        {
            UIElement element = new UIElement();
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString(gameObject.BackColor.Name);

            if (gameObject is Ball ellipsing)
            {
                element = new Ellipse { Height = ellipsing.SizeY, Width = ellipsing.SizeX, Fill = brush, Margin = new Thickness(ellipsing.PosX, ellipsing.PosY, 0, 0), VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
            }

            if (gameObject is Platform recta)
            {
                element = new Rectangle { Height = recta.SizeY, Width = recta.SizeX, RadiusX = 15, RadiusY = 10, Fill = Brushes.Black, Margin = new Thickness(recta.PosX, recta.PosY, 0, 0), VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
            }

            Playfield_Grid.Children.Add(element);
        }

        private void UpdatePlayfield()
        {
            _playfield.MoveGameObjects();
            Playfield_Grid.Children.Clear();
            foreach (var gameObject in _playfield.GameObjects)
                PutObject(gameObject);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                GameObject platform = _playfield.GameObjects.FirstOrDefault(x => x.GetType() == typeof(Platform));
                if (platform != null)
                {
                    platform.SpeedX = 0;
                }
            }
        }

        private void OnBallOut(bool isOnTop)
        {
            isGameOver = true;
            _timer.Stop();
            if (isOnTop)
            {
                MessageBox.Show("Победа");
            }
            else
            {
                MessageBox.Show("Арман Аракелян");
            }

            StartButton.IsEnabled = true;
            _playfield.OnOut -= OnBallOut;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GameObject platform = _playfield.GameObjects.FirstOrDefault(x => x.GetType() == typeof(Platform));
            if (platform != null)
            {
                if (e.Key == Key.Left)
                {
                    platform.SpeedX = -2;
                }

                if (e.Key == Key.Right)
                {
                    platform.SpeedX = 2;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            if (_playfield != null)
                _playfield.OnOut -= OnBallOut;
            Init();
        }

        private void UpdateTextWrong()
        {
            while (isGameOver != true)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate () { UpdatePlayfield(); }
                );
                Thread.Sleep(15);
            }
        }
    }
}
