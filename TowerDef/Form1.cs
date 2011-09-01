using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using TexLib;

namespace TowerDef
{

    public partial class Form1 : Form
    {
        private int Money;
        private int xx, yy;
        private bool preview = false;
        private Button SetSwitcher;
        private int CanSwitcher = 1;
        private List<Cannon> Cannons;
        private Map m;
        private List<Bullet> Bullets;// = new Bullet[Cannons.Count];
        private LinkedList<Enemy> Enemies;

        public DateTime gameStart = DateTime.Now;
        public DateTime lastSpawn, prew = DateTime.Now, st;// = DateTime.Now;
        private int spawn = 1000;
        private long lastFpsTime = 0;
        private long frameCounter = 0;
        Random rd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }


        void Render()
        {
            #region Counting_Fps
            long now = DateTime.Now.Ticks;
            if (now - lastFpsTime > 10000000)      // more than 1 sec
            {
                double fps = frameCounter * 1.0e7 / (now - lastFpsTime);
                lastFpsTime = now;
                frameCounter = 0;
                label1.Text = String.Format("Fps: {0:0.0}", fps);
                label2.Text = "Money: " + Money.ToString();

            }
            frameCounter++;
            #endregion

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadIdentity();
            System.Threading.Thread.Sleep(10);

            #region Drawing
            
            m.Draw();
            foreach (Bullet bul in Bullets)
            {
                bul.Draw();
            }
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw();
            }
            foreach (Cannon can in Cannons)
            {
                can.Draw();
            } 
            if (preview)
            {
                Preview.Draw(xx, yy);
                if ((DateTime.Now - prew).TotalSeconds > 0.7)
                    preview = false;
            }
            #endregion

            glControl1.SwapBuffers();

        }

        private void Init()
        {
            Money = Config.InitialMoney;
            lastSpawn = DateTime.Now;
            spawn = 100;
            m = new Map(11, 6);
            st = DateTime.Now;
            Cannons = new List<Cannon>();

            Cannon1 c = new Cannon1(); c.position = new Vector2d(560, 80); Cannons.Add(c);

            c = new Cannon1(); c.position = new Vector2d(400, 200); Cannons.Add(c);
            c = new Cannon1(); c.position = new Vector2d(240, 100); Cannons.Add(c);
            c = new Cannon1(); c.position = new Vector2d(240, 180); Cannons.Add(c);
            c = new Cannon1(); c.position = new Vector2d(240, 240); Cannons.Add(c);

            Bullets = new List<Bullet>();

            Enemies = new LinkedList<Enemy>();
            Enemy a = new Enemy(); a.position = new Vector2d(0, 0);
            Enemies.AddFirst(a);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
            TexUtil.InitTexturing();
            OnResize(null);
            GL.ClearColor(Color.Coral);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, glControl1.Width, 0, glControl1.Height, -1, 1);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            Init();
            glControl1.Paint += new PaintEventHandler(glControl1_Paint);
            glControl1.MouseClick += new MouseEventHandler(glControl1_MouseClick);

            SetSwitcher = cann1;
            SetSwitcher.BackColor = Color.LightBlue;
            cann1.Image = new Bitmap("delo.png");
            cann2.Image = new Bitmap("delo2.png");
            cann3.Image = new Bitmap("delo3.png");
        }

        void glControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cannon c = null;
                if (CanSwitcher == 1 && cann1.Enabled)
                {
                    c = new Cannon1();
                    Money -= Config.Cannon1Price;
                }
                else if (CanSwitcher == 2 && cann2.Enabled)
                {
                    c = new Cannon2();
                    Money -= Config.Cannon2Price;
                }
                else if (CanSwitcher == 3 && cann3.Enabled)
                {
                    c = new Cannon3();
                    Money -= Config.Cannon3Price;
                }
                if (c == null)
                    return;

                c.position = new Vector2d(e.X, glControl1.Height - e.Y);
                if (m.IsOffTrack(e.X, glControl1.Height - e.Y))
                    Cannons.Add(c);

            }
            else if (e.Button == MouseButtons.Right)
            {
                prew = DateTime.Now;
                preview = true;
                xx = e.X;
                yy = glControl1.Height - e.Y;
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            cann1.Enabled = (Money >= Config.Cannon1Price);
            cann2.Enabled = (Money >= Config.Cannon2Price);
            cann3.Enabled = (Money >= Config.Cannon3Price);

            if ((DateTime.Now - gameStart).TotalSeconds > 60 && Enemies.Count == 0)
            {
                MessageBox.Show("You won!");
                this.Close();
            }

            #region Spawn_Enemies
            if ((DateTime.Now - lastSpawn).TotalMilliseconds > spawn && (DateTime.Now - gameStart).TotalSeconds < 30)
            {
                Enemy1 a = new Enemy1();
                Enemies.AddLast(a);
                spawn = rd.Next(400, 1500);
                lastSpawn = DateTime.Now;
            }
            else if ((DateTime.Now - lastSpawn).TotalMilliseconds > spawn &&
                (DateTime.Now - gameStart).TotalSeconds > 40 &&
                (DateTime.Now - gameStart).TotalSeconds < 60)
            {
                Enemy2 a = new Enemy2();
                Enemies.AddLast(a);
                spawn = rd.Next(400, 1500);
                lastSpawn = DateTime.Now;
            }
            else if ((DateTime.Now - lastSpawn).TotalMilliseconds > spawn &&
                (DateTime.Now - gameStart).TotalSeconds > 80 &&
                (DateTime.Now - gameStart).TotalSeconds < 100)
            {
                Enemy3 a = new Enemy3();
                Enemies.AddLast(a);
                spawn = rd.Next(400, 1500);
                lastSpawn = DateTime.Now;
            }
            #endregion
            #region Updating_Coordinates

            foreach (Enemy a in Enemies)
            {
                a.Update();
                a.SetPos();
            }

            foreach (Bullet bul in Bullets)
            {
                bul.Update(DateTime.Now);
                bul.SetPos();
            }

            foreach (Cannon can in Cannons)
            {
                if (Enemies.Count == 0)
                    break;

                Enemy en = null;
                foreach (Enemy a in Enemies)
                {
                    if (can.Range > (a.position - can.position).Length)
                    {
                        en = a;
                        Bullet bull = can.Shoot(a);
                        if (bull != null)
                            Bullets.Add(bull);
                        break;
                    }
                }
                en = (en == null) ? Enemies.First.Value : en;
                can.CreateAim(en);
                can.SetPos();
            }

            #endregion
            glControl1.Invalidate();
            #region Clearing_Bullets

            Stack<Enemy> Clearence_Ball = new Stack<Enemy>();
            Stack<Bullet> Clearence_Bull = new Stack<Bullet>();
            foreach (Bullet bul in Bullets)
            {
                if (bul.HitEnd)
                {
                    Clearence_Bull.Push(bul);
                    bul.enemy.Amo -= bul.Strength;
                }
            }
            while (Clearence_Bull.Count > 0)
            {
                Bullet bul = Clearence_Bull.Pop();
                Bullets.Remove(bul);
            }

            #endregion
            #region Clearing_Enemies
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Amo < 0)
                    Money += Config.EnemyPrice;
                if (enemy.IsOffTrack())
                {
                    MessageBox.Show("You lost!");
                    this.Close();
                }

                if (enemy.IsOffTrack() || enemy.Amo < 0)
                    Clearence_Ball.Push(enemy);
            }
            while (Clearence_Ball.Count > 0)
            {
                Enemy enemy = Clearence_Ball.Pop();
                Enemies.Remove(enemy);
            }
            #endregion
        }

        #region Cannon_Switcher
        private void cann1_Click(object sender, EventArgs e)
        {
            CanSwitcher = 1;
            SetSwitcher.BackColor = Control.DefaultBackColor;
            SetSwitcher = ((Button)sender);
            ((Button)sender).BackColor = Color.LightBlue;
        }
        private void cann2_Click(object sender, EventArgs e)
        {
            CanSwitcher = 2;
            SetSwitcher.BackColor = Control.DefaultBackColor;
            SetSwitcher = ((Button)sender);
            ((Button)sender).BackColor = Color.LightBlue;
        }
        private void cann3_Click(object sender, EventArgs e)
        {
            CanSwitcher = 3;
            SetSwitcher.BackColor = Control.DefaultBackColor;
            SetSwitcher = ((Button)sender);
            ((Button)sender).BackColor = Color.LightBlue;
        }
        #endregion

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Enemy a = new Enemy();
                Enemies.AddLast(a);
            }
        }

    }

    public class Preview
    {
        static int texture = TexUtil.CreateTextureFromFile("terc.png");
        public static void Draw(int x, int y)
        {
            Vector2d mid = new Vector2d(x, y);
            Vector2d Helpful1 = new Vector2d(150, 150);
            Vector2d Helpful2 = new Vector2d(-150, 150);
            Vector2d R_Down = mid - Helpful2;
            Vector2d L_Up = mid + Helpful2;
            Vector2d R_Up = mid + Helpful1;
            Vector2d L_Down = mid - Helpful1;

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
