using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    public partial class Form1 : Form
    {
        Graphics g;     // холст
        GraphicsPath pp;  // контур фантома-дырки
        SolidBrush br;   // кисть
        public Form1()
        {
            InitializeComponent();
            g = Graphics.FromHwnd(this.Handle); // холст
            pp = paraplan(100, 100, 50); // положение и размер
        }
        private GraphicsPath paraplan(int x, int y, int r)
            {
            int[,] pts = { {x,y },
                {x - 2*r, y + r },
                {x,y+ 3*r },
                {x+2*r,y + r },
                {x,y },
                {x+4*r,y },
                {x+4*r,y+3*r },
                {x, y+3*r}
            };
            Point[] pt = new Point[8]; // массив точек для gp
            byte[] typ = new byte[8];  // массив соединений
            for (int p = 0; p < 8; p++){
                pt[p].X = pts[p, 0];
                pt[p].Y = pts[p, 1];
                typ[p] = (byte)PathPointType.Line;};
            GraphicsPath gp = new GraphicsPath(pt, typ);
            return gp; // возвращаем внутренняя часть формы
        }

        private void Form1_Click(object sender, EventArgs e)
        {

            // Метод обновляет отсеченную область gp

            Region gp = new Region(pp);

            g.ExcludeClip(gp);

            // Рисуем параплан большего размера

            GraphicsPath pp1 = paraplan(300, 250, 80); // контур

            br = new SolidBrush(RandomColor());    // кисть

            g.FillPath(br, pp1);            // закраска
        }

        public Color RandomColor()

        {

            int r, g, b;

            byte[] bytes1 = new byte[3];   // массив 3 цветов

            Random rnd1 = new Random();   // объект класса

            rnd1.NextBytes(bytes1);     // генерация массива

            r = Convert.ToInt16(bytes1[0]);

            g = Convert.ToInt16(bytes1[1]);

            b = Convert.ToInt16(bytes1[2]);

            return Color.FromArgb(r, g, b); // цвет через метод

        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.ResetClip(); // сбрасывает вырезанную область и

            // делает ее бесконечной, анти_ExcludeClip();

            br = new SolidBrush(RandomColor());

            g.FillPath(br, pp); // ставим заплатку
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphicsPath pp1 = paraplan(400, 80, 30);

            // Рисуем параплан меньшего размера

            Pen pen = new Pen(RandomColor(), 4f);

            g.DrawPath(pen, pp1);
        }
    }

}
