using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using TexLib;

namespace TowerDef
{
    class TestBullet
    {
        public LinkedList<Vector2d> Path;
        public Vector2d position;
        public Vector2d target;

        public const double Speed = 0.3;

        private string FileName = "bullet.png";
        private int texture;

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

        public TestBullet()
        {
            texture = TexUtil.CreateTextureFromFile(FileName);
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

    }
}
