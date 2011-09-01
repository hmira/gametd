using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using TexLib;

namespace TowerDef
{
    
    abstract class Cannon
    {
        private DateTime lastShoot = DateTime.MinValue;
        public double Range = Config.CannonRange;
        public Vector2d position;
        public Vector2d Aim;
        protected Vector2d R_Down, R_Up, L_Down, L_Up;
        protected int size = Config.CannonSize;
        protected int texture;

        public virtual void CreateAim(Enemy Ball)
        {
            Vector2d a = BulletPrediction(Ball);
            Aim = a - position;
        }

        public virtual void SetPos()
        {
            Vector2d AimN = Vector2d.NormalizeFast(Aim);
            Vector2d x = this.position;
            Vector2d a = new Vector2d(-AimN.Y, AimN.X);
            Vector2d Helpv1 = AimN + a;
            Vector2d Helpv2 = AimN - a;

            Helpv1 *= size;
            Helpv2 *= size;

            R_Down = x + Helpv2;
            L_Up = x - Helpv2;
            R_Up = x - Helpv1;
            L_Down = x + Helpv1;
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

        public virtual Vector2d BulletPrediction(Enemy b)
        {
            Vector2d dir = b.dir;

            //predicted bullet target
            Vector2d X = b.Speed * b.position - position * Bullet.Speed;
            X /= +b.Speed - Bullet.Speed;
            double d = (position - X).Length;
            Vector2d res = b.position + b.dir * d * Bullet.Speed;

            return res;
        }

        private Vector2d GetIntersect(
            Vector2d A, Vector2d B,
            double CannSpeed, double BallSpeed,
            Vector2d BallPos, Vector2d CanPos, out bool hit)
        {
            double Cann_dist1, Ball_dist1, Cann_dist2, Ball_dist2;

            while (true)
            {
                Vector2d C = (A + B) * 0.5;

                Cann_dist1 = (A - CanPos).Length * BallSpeed;
                Ball_dist1 = (A - BallPos).Length * CannSpeed;
                Cann_dist2 = (C - CanPos).Length * BallSpeed;
                Ball_dist2 = (C - BallPos).Length * CannSpeed;

                if (Ball_dist1 <= Cann_dist1 && Ball_dist2 >= Cann_dist2)
                {
                    B = C;
                    if (Math.Abs(Ball_dist1 - Ball_dist2) < 5)
                    {
                        hit = true;
                        return C;
                    }
                    continue;
                }

                Cann_dist1 = Cann_dist2;
                Ball_dist1 = Ball_dist2;
                Cann_dist2 = (B - CanPos).Length * BallSpeed;
                Ball_dist2 = (B - BallPos).Length * CannSpeed;

                if (Ball_dist1 <= Cann_dist1 && Ball_dist2 >= Cann_dist2)
                {
                    A = C;
                    if (Math.Abs(Ball_dist1 - Ball_dist2) < 5)
                    {
                        hit = true;
                        return C;
                    }
                    continue;
                }
                break;
            }
            hit = false;
            return Vector2d.Zero;

        }

        /// <summary>
        /// v pripade, ze medzi poziciou nepriatela a miestor
        /// </summary>
        /// <param name="Elapsed"></param>
        /// <param name="AA"></param>
        /// <param name="BB"></param>
        /// <param name="CannSpeed"></param>
        /// <param name="BallSpeed"></param>
        /// <param name="BallPos"></param>
        /// <param name="CanPos"></param>
        /// <param name="hit"></param>
        /// <returns></returns>
        private Vector2d GetIntersect2(
            double Elapsed,
            Vector2d AA, Vector2d BB,
            double CannSpeed, double BallSpeed,
            Vector2d BallPos, Vector2d CanPos, out bool hit)
        {
            double Cann_dist1, Ball_dist1, Cann_dist2, Ball_dist2;
            Vector2d A = AA;
            Vector2d B = BB;

            while (true)
            {
                Vector2d C = (A + B) * 0.5;

                Cann_dist1 = (A - CanPos).Length * BallSpeed;
                Ball_dist1 = ((A - AA).Length + Elapsed) * CannSpeed;
                Cann_dist2 = (C - CanPos).Length * BallSpeed;
                Ball_dist2 = ((C - AA).Length + Elapsed) * CannSpeed;

                if (Ball_dist1 <= Cann_dist1 && Ball_dist2 >= Cann_dist2)
                {
                    B = C;
                    if (Math.Abs(Ball_dist1 - Ball_dist2) < 5)
                    {
                        hit = true;
                        return C;
                    }
                    continue;
                }

                Cann_dist1 = Cann_dist2;
                Ball_dist1 = Ball_dist2;
                Cann_dist2 = (B - CanPos).Length * BallSpeed;
                Ball_dist2 = ((B - AA).Length + Elapsed) * CannSpeed;

                if (Ball_dist1 <= Cann_dist1 && Ball_dist2 >= Cann_dist2)
                {
                    A = C;
                    if (Math.Abs(Ball_dist1 - Ball_dist2) < 5)
                    {
                        hit = true;
                        return C;
                    }
                    continue;
                }
                break;
            }
            hit = false;
            return Vector2d.Zero;
        }

        public virtual Bullet Shoot(Enemy b)
        {
            if ((DateTime.Now - this.lastShoot).TotalMilliseconds < Config.ShootInterval)
                return null;
            else
                lastShoot = DateTime.Now;

            Vector2d Target = Vector2d.Zero;
            bool hit;
            Target = GetIntersect(b.position, b.P.Points[1].Coords,
                                    Bullet.Speed, b.Speed,
                                    b.position, this.position, out hit);

            if (!hit && b.P.Points.Count < 3)
                return null;
            if (!hit)
                Target = GetIntersect2(b.P.Points[1].Course - b.Elapsed, b.P.Points[1].Coords, b.P.Points[2].Coords,
                                    Bullet.Speed, b.Speed,
                                    b.position, this.position, out hit);
            if (Target.X > Config.MapSizeX || Target.Y > Config.MapSizeY || Target.X < 0 || Target.Y < 0)
                return null;
            Bullet ret = new Bullet(this.position, Target, DateTime.Now);
            ret.enemy = b;

            if (this is Cannon1)
                ret.Strength = Config.Strenght1;
            else if (this is Cannon2)
                ret.Strength = Config.Strenght2;
            else if (this is Cannon3)
                ret.Strength = Config.Strenght3;
            return ret;
        }


    }

    class Cannon1 : Cannon
    {
        public Cannon1()
        {
            texture = TexSupport.canTexture1;
        }
    }
    class Cannon2 : Cannon
    {
        public Cannon2()
        {
            texture = TexSupport.canTexture2;
        }
    }
    class Cannon3 : Cannon
    {
        public Cannon3()
        {
            texture = TexSupport.canTexture3;
        }
    }
}
