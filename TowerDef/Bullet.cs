using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using TexLib;

namespace TowerDef
{
    class Bullet
    {
        private Vector2d From;
        private Vector2d To;
        private Vector2d dir;
        private DateTime ShootTime;

        public Enemy enemy;
        public Vector2d position;
        public Vector2d target;
        public bool HitEnd = false;
        public bool Alive = true;
        public int Strength;

        public const double Speed = Config.BulletSpeed;
        private static int texture = TexSupport.bulletTexture;
        private Vector2d R_Down, R_Up, L_Down, L_Up;

        public void SetPos()
        {
            Vector2d x = this.position;
            Vector2d Helpful1 = new Vector2d(5, 5);
            Vector2d Helpful2 = new Vector2d(-5, 5);
            R_Down = x - Helpful2;
            L_Up = x + Helpful2;
            R_Up = x + Helpful1;
            L_Down = x - Helpful1;
        }
        public Bullet(Vector2d From, Vector2d To, DateTime st)
        {
            ShootTime = DateTime.Now;
            this.From = From;
            this.To = To;
            this.dir = Vector2d.Normalize(To - From);
        }
        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(R_Down);
            GL.TexCoord2(1, 0); GL.Vertex2(L_Down);
            GL.TexCoord2(1, 1); GL.Vertex2(L_Up);
            GL.TexCoord2(0, 1); GL.Vertex2(R_Up);
            GL.End();
        }
        public void Update(DateTime st)
        {
            double t = (DateTime.Now - ShootTime).TotalMilliseconds;
            position = From + Vector2d.Multiply(dir, Bullet.Speed * t);
            if ((position - From).LengthSquared > (From - To).LengthSquared)
                HitEnd = true;
        }

    }
}
