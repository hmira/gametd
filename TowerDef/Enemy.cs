using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using TexLib;

namespace TowerDef
{
    class Enemy
    {
        private DateTime st;
        public Vector2d dir;

        private Vector2d From = Config.EnemyFrom;
        private Vector2d To = Config.EnemyTo;
        public Path P;

        public Vector2d position;
        public bool HitEnd = false;
        public bool Alive = true;
        //public int Amo = Config.EnemyAmo;

        //public const double Speed = Config.EnemySpeed;
        public double Elapsed;

        protected int texture;
        public double Speed;
        public int Amo;

        private Vector2d R_Down, R_Up, L_Down, L_Up;

        public void SetPos()
        {
            Elapsed = (this.position - P.Points[0].Coords).Length + P.Points[0].Course;
            if (Elapsed > P.Points[1].Course)
                P.Points.RemoveAt(0);

            Vector2d x = this.position;
            Vector2d Helpful1 = new Vector2d(15, 15);
            Vector2d Helpful2 = new Vector2d(-15, 15);
            R_Down = x - Helpful2;
            L_Up = x + Helpful2;
            R_Up = x + Helpful1;
            L_Down = x - Helpful1;
        }

        public Enemy()
        {
            st = DateTime.Now;
            Elapsed = 0.0;
            P = Config.CreatePath();
        }

        public virtual void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(R_Down);
            GL.TexCoord2(1, 0); GL.Vertex2(L_Down);
            GL.TexCoord2(1, 1); GL.Vertex2(L_Up);
            GL.TexCoord2(0, 1); GL.Vertex2(R_Up);
            GL.End();
        }

        public void Update()
        {
            double elapsed = (DateTime.Now - st).TotalMilliseconds;
            //dir = Vector2d.Normalize(To - From);
            //position = From + ((elapsed * Speed) * dir);
            position = P.GetPosition(elapsed, Speed);
        }

        public bool IsOffTrack()
        {
            return ((this.position - this.To).Length < 2);
        }
    }

    class Enemy1 : Enemy
    {
        public Enemy1()
        {
            this.texture = TexSupport.enemy1Texture;
            this.Speed = Config.Enemy1Speed;
            this.Amo = Config.Enemy1Amo;
        }
    }

    class Enemy2 : Enemy
    {
        public Enemy2()
        {
            this.texture = TexSupport.enemy2Texture;
            this.Speed = Config.Enemy2Speed;
            this.Amo = Config.Enemy2Amo;
        }
    }

    class Enemy3 : Enemy
    {
        public Enemy3()
        {
            this.texture = TexSupport.enemy3Texture;
            this.Speed = Config.Enemy3Speed;
            this.Amo = Config.Enemy3Amo;
        }
    }
}
