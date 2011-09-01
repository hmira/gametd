using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace TowerDef
{
    static class Config
    {
        #region Map
        public const int MapSizeX = 990, MapSizeY = 480;
        public const int MapX = 90, MapY = 80;
        public static Path CreatePath()
        {
            Path P = new Path(); 
            P.AddPoint(new Vector2d(45 + MapX * 1, 30 + 0 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 1, 30 + 4 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 3, 30 + 4 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 3, 30 + 1 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 5, 30 + 1 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 5, 30 + 4 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 7, 30 + 4 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 7, 30 + 1 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 9, 30 + 1 * MapY));
            P.AddPoint(new Vector2d(45 + MapX * 9, 30 + 6 * MapY));
            return P;
        }

        public static Elem[] Track ={
                                   new Elem(1,0),
                                   new Elem(1,4),
                                   new Elem(3,4),
                                   new Elem(3,1),
                                   new Elem(5,1),
                                   new Elem(5,4),
                                   new Elem(7,4),
                                   new Elem(7,1),
                                   new Elem(9,1),
                                   new Elem(9,6)
                               };
        #endregion

        #region Cannon
        public const int CannonRange = 150;
        public const int CannonSize = 30;
        public const int ShootInterval = 800; //in milliseconds
        public const int 
            Strenght1 = 9,
            Strenght2 = 25,
            Strenght3 = 80;
        #endregion

        #region Enemy
        public static Vector2d 
            EnemyFrom = new Vector2d(40, 40),
            EnemyTo = new Vector2d(45 + 90 * 9, 30 + 6 * 80);
        public const int EnemyAmo = 200;
        public const double EnemySpeed = 0.03;

        public const int Enemy1Amo = 200;
        public const double Enemy1Speed = 0.03;

        public const int Enemy2Amo = 300;
        public const double Enemy2Speed = 0.03;

        public const int Enemy3Amo = 300;
        public const double Enemy3Speed = 0.04;
        #endregion

        #region Bullet
        public const double BulletSpeed = 0.60;
        #endregion

        #region Money

        public const int InitialMoney = 5;
        public const int EnemyPrice = 1;
        public const int Cannon1Price = 2;
        public const int Cannon2Price = 5;
        public const int Cannon3Price = 10;

        #endregion
    }
}
