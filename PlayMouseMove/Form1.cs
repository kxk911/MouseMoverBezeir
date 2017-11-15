using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace PlayMouseMove
{

    struct positions
    {
       // public int[] posX = { 0, 222, 333, 1200 };
        //public int[] posY = { 0, 500, 100, 500 };
        //public int leng = 4;
    }

    delegate void moveStop();

    public partial class Form1 : Form
    {
        double bezeT = 1;
        double bezeX = 0;
        double bezeY = 0;
        int TimerInterval = 10000;
        int LastX = 0;
        int LastY = 0;
        Thread elipse;
        moveStop delMoveStop;
        public Form1()
        {
            InitializeComponent();
            Thread tread = new Thread(Move);
           // tread.Start();
            //Move();
            elipse = new Thread(MoveBeze);
            elipse.Start();
            timer1.Interval = TimerInterval;
            delMoveStop = new moveStop(Stop);
            timer1.Start();
        }

        private void Stop()
        {
            elipse.Abort();
            int curPosX = Cursor.Position.X;
            int curPosY = Cursor.Position.Y;
            positions pos = new positions();
            Thread trMoveToPoint = new Thread(delegate() { MoveBezeToPoint(curPosX,curPosY,1300,700); });
            //Thread.Sleep(5000);
           // elipse = new Thread(MoveBeze);
            //elipse.Start();
            //if (!elipse.IsAlive) elipse.Start();
            trMoveToPoint.Start();


        }

        static private void Move()
        {

            string str="";
            StreamReader streamReader = new StreamReader("D:\\myfile.txt");
            while (!streamReader.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
            {
                str += streamReader.ReadLine(); //В переменную str по строчно записываем содержимое файла
            }
            streamReader.Close();
            string[] sp = str.Split('|');
            for (int i = 0; i < sp.Count()-1; i++)
            {
                string[] spXY = sp[i].Split(',');

                Cursor.Position = new System.Drawing.Point(int.Parse(spXY[0]), int.Parse(spXY[1]));
                Thread.Sleep(4);
            }            
        }

        private void MoveBeze()
        {
            Random rnd = new Random();
            int x1 = 0, x2 = 0, x3 = 0;
            int y1 = 0, y2 = 0, y3 = 0;
            double i = 0;
            int pause = 0;
            int speed = 5;
            while (true)
            {

                x1 = LastX;
                x2 = rnd.Next(1366);
                x3 = rnd.Next(1366);
                y1 = LastY;
                y2 = rnd.Next(768);
                y3 = rnd.Next(768);

                for (i = 0; i < bezeT; i = i + 0.01)
                {
                    Math.Pow(i, 2);
                    bezeX = (Math.Pow((1 - i), 2)) * x1 + 2 * i * (1 - i) * x2 + (Math.Pow(i, 2)) * (x3 ^ 2);
                    bezeY = (Math.Pow((1 - i), 2)) * y1 + 2 * i * (1 - i) * y2 + (Math.Pow(i, 2)) * (y3 ^ 2);
                    Cursor.Position = new System.Drawing.Point((int)bezeX, (int)bezeY);
                    label1.Invoke(new Action(() => label1.Text = bezeX.ToString()));
                    label2.Invoke(new Action(() => label2.Text = bezeY.ToString()));
                    Thread.Sleep(speed);
                }
                LastX = x3;
                LastY = y3;
                speed = rnd.Next(7, 15);
                label3.Invoke(new Action(() => label3.Text = speed.ToString()));
                
                pause = rnd.Next(1000);
                label4.Invoke(new Action(() => label4.Text = pause.ToString()));
                Thread.Sleep(rnd.Next(pause));
            }
        }

         private void MoveBezeToPoint(int X0, int Y0, int X1, int Y1)
         {
             Random rnd = new Random();
             int x1 = 0, x2 = 0, x3 = 0;
             int y1 = 0, y2 = 0, y3 = 0;
             double i = 0;
             int pause = 0;
             int speed = 5;


                 x1 = X0;
                 x2 = rnd.Next(1366);
                 //int posIndex = rnd.Next(0, pos.leng);
                 x3 = X1;
                 y1 = Y0;
                 y2 = rnd.Next(768);
                 y3 = Y1;

                 for (i = 0; i < bezeT; i = i + 0.01)
                 {
                     Math.Pow(i, 2);
                     bezeX = (Math.Pow((1 - i), 2)) * x1 + 2 * i * (1 - i) * x2 + (Math.Pow(i, 2)) * (x3 ^ 2);
                     bezeY = (Math.Pow((1 - i), 2)) * y1 + 2 * i * (1 - i) * y2 + (Math.Pow(i, 2)) * (y3 ^ 2);
                     Cursor.Position = new System.Drawing.Point((int)bezeX, (int)bezeY);
                     label1.Invoke(new Action(() => label1.Text = bezeX.ToString()));
                     label2.Invoke(new Action(() => label2.Text = bezeY.ToString()));
                     Thread.Sleep(speed);
                 }
                 int CurPosX = 0;
                 int CurPosY = 0;
                 if (Cursor.Position.X != X1 || Cursor.Position.Y != Y1)
                 {
                     if (Cursor.Position.X > X1 && Cursor.Position.Y > Y1)
                     {
                         while (true)
                         {
                             if (Cursor.Position.X != X1) CurPosX = Cursor.Position.X - 1;
                             if (Cursor.Position.Y != Y1) CurPosY = Cursor.Position.Y - 1;
                             Cursor.Position = new System.Drawing.Point(CurPosX,CurPosY);
                             if (Cursor.Position.X == X1 && Cursor.Position.Y == Y1) break;
                             Thread.Sleep(10);
                         }
                     }
                     else if (Cursor.Position.X > X1 && Cursor.Position.Y < Y1)
                     {
                         while (true)
                         {
                             if (Cursor.Position.X != X1) CurPosX = Cursor.Position.X - 1;
                             if (Cursor.Position.Y != Y1) CurPosY = Cursor.Position.Y + 1;
                             Cursor.Position = new System.Drawing.Point(CurPosX, CurPosY);
                             if (Cursor.Position.X == X1 && Cursor.Position.Y == Y1) break;
                             Thread.Sleep(10);
                         }
                     }
                     else if (Cursor.Position.X < X1 && Cursor.Position.Y > Y1)
                     {
                         while (true)
                         {
                             if (Cursor.Position.X != X1) CurPosX = Cursor.Position.X + 1;
                             if (Cursor.Position.Y != Y1) CurPosY = Cursor.Position.Y - 1;
                             Cursor.Position = new System.Drawing.Point(CurPosX, CurPosY);
                             if (Cursor.Position.X == X1 && Cursor.Position.Y == Y1) break;
                             Thread.Sleep(10);
                         }
                     }
                     else if (Cursor.Position.X < X1 && Cursor.Position.Y < Y1)
                     {
                         while (true)
                         {
                             if (Cursor.Position.X != X1) CurPosX = Cursor.Position.X + 1;
                             if (Cursor.Position.Y != Y1) CurPosY = Cursor.Position.Y + 1;
                             Cursor.Position = new System.Drawing.Point(CurPosX, CurPosY);
                             if (Cursor.Position.X == X1 && Cursor.Position.Y == Y1) break;
                             Thread.Sleep(10);
                         }
                     }
                 }
                 LastX = x3;
                 LastY = y3;
                 speed = rnd.Next(7, 15);
                 label3.Invoke(new Action(() => label3.Text = speed.ToString()));

                 pause = rnd.Next(1000);
                 label4.Invoke(new Action(() => label4.Text = pause.ToString()));
                 Thread.Sleep(rnd.Next(pause));

         }
         private void label5_Click(object sender, EventArgs e)
         {

         }

         private void timer1_Tick(object sender, EventArgs e)
         {
            // Stop();
             delMoveStop.Invoke();
         }
    }
}
