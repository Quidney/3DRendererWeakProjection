using System.Numerics;
using _3DRendererWeakProjection.Rendering;

namespace _3DRendererWeakProjection
{
    public partial class Form1 : Form
    {
        internal static Form1? Instance;

        Object3D Cube;
        float deltaTime;

        public Form1()
        {
            Text = "3D Rendering";
            ClientSize = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            Cube = new Object3D(Vector3.Zero, Vector3.Zero, new Vector3(200, 100, 100), PrimalTypes.Thingy);

            _ = new Camera(Vector3.Zero, Vector3.Zero);

            Instance = this;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            TimeSpan previous = new(DateTime.Now.Ticks);
            while (true)
            {
                Camera? cam = Camera.Instance;
                if (cam == null) { await Task.Delay(1000 / 60); continue; }

                TimeSpan current = new (DateTime.Now.Ticks);
                deltaTime = (float)(current - previous).TotalSeconds;
                previous = current;

                MoveCamera();

                Invalidate();
                await Task.Delay(1000 / 60);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            Renderer3D.Render(Cube, g);

            base.OnPaint(e);
        }


        void MoveCamera()
        {
            Camera? cam = Camera.Instance;
            if (cam == null) return;

            cam.Position += new Vector3(GetHorizontalInput() * deltaTime, 0, GetVerticalInput() * deltaTime);

            Cube.Rotation += new Vector3(GetHorizontalArrowKeys() * deltaTime, 0, GetVerticalArrowKeys() * deltaTime);
        }

        HashSet<Keys> KeysDown = [];
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!KeysDown.Contains(e.KeyCode))
                KeysDown.Add(e.KeyCode);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (KeysDown.Contains(e.KeyCode))
                KeysDown.Remove(e.KeyCode);
        }

        int GetVerticalInput() => GetInputFromKeys(Keys.W, Keys.S);
        int GetHorizontalInput() => GetInputFromKeys(Keys.D, Keys.A);

        int GetVerticalArrowKeys() => GetInputFromKeys(Keys.Right, Keys.Left);
        int GetHorizontalArrowKeys() => GetInputFromKeys(Keys.Up, Keys.Down);

        int GetInputFromKeys(Keys key1, Keys key2)
        {
            return KeysDown.Contains(key1) && KeysDown.Contains(key2) ? 0 : KeysDown.Contains(key1) ? 1 : KeysDown.Contains(key2) ? -1 : 0;
        }
    }
}
