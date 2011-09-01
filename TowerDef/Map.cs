using OpenTK.Graphics.OpenGL;
using TexLib;

namespace TowerDef
{
    struct Elem
    {
        public int x,y;
        public Elem(int x, int y)
        {
            this.x = x; this.y = y;
        }
    }
    class Map
    {
        public int width, height;
        private const int Xelem = Config.MapX, Yelem = Config.MapY; // z tohto pohľadu sieť 11 × 6
        private int mapTexture = TexSupport.mapTexture;
        public bool[,] T;
        private Elem[] track = Config.Track;
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height; 
            T = new bool[width, height];
            this.SetBack();
        }
        public void SetBack()
        {
            for (int i = 0; i < track.Length - 1; i++)
            {
                int x1 = track[i].x;
                int y1 = track[i].y;
                int x2 = track[i + 1].x;
                int y2 = track[i + 1].y;
                bool IsX = x1 - x2 != 0;
                int dif = IsX ? x2 - x1 : y2 - y1;
                for (int j = 0; j < dif; j++)
                {
                    if (IsX)
                        T[x1 + j, y1] = true;
                    else
                        T[x1, y1 + j] = true;
                } 
                for (int j = 0; j > dif; j--)
                {
                    if (IsX)
                        T[x1 + j, y1] = true;
                    else
                        T[x1, y1 + j] = true;
                }
            }
        }
        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, mapTexture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(Config.MapSizeX, 0               );
            GL.TexCoord2(1, 0); GL.Vertex2(0,               0               );
            GL.TexCoord2(1, 1); GL.Vertex2(0,               Config.MapSizeY );
            GL.TexCoord2(0, 1); GL.Vertex2(Config.MapSizeX, Config.MapSizeY );
            GL.End();
        }
        public bool IsOffTrack(int x, int y)
        {
            int xx = x / Xelem;
            int yy = y / Yelem;
            return !T[xx, yy];
        }
    }
}
