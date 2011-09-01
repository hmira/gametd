using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexLib;

namespace TowerDef
{
    static class TexSupport
    {
        public static int enemy1Texture = TexUtil.CreateTextureFromFile("enemy1.png");
        public static int enemy2Texture = TexUtil.CreateTextureFromFile("enemy2.png");
        public static int enemy3Texture = TexUtil.CreateTextureFromFile("enemy3.png");
        public static int mapTexture = TexUtil.CreateTextureFromFile("map.png");
        public static int bulletTexture = TexUtil.CreateTextureFromFile("bullet.png");
        public static int canTexture1 = TexUtil.CreateTextureFromFile("delo.png");
        public static int canTexture2 = TexUtil.CreateTextureFromFile("delo2.png");
        public static int canTexture3 = TexUtil.CreateTextureFromFile("delo3.png");
    }
}
