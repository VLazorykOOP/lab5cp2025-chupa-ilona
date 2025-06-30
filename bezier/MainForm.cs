using System;
using System.Drawing;
using System.Windows.Forms;

namespace BEZIER
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Крива Без’є 3-го порядку";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            this.Paint += MainForm_Paint;
        }

        // Задання контрольних точок
        PointF P1 = new PointF(100, 400);
        PointF P2 = new PointF(200, 100);
        PointF P3 = new PointF(500, 100);
        PointF P4 = new PointF(600, 400);

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Малюємо опорний чотирикутник
            using (Pen controlPen = new Pen(Color.LightGray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                g.DrawLines(controlPen, new PointF[] { P1, P2, P3, P4 });
            }

            // Малюємо точки
            DrawPoint(g, P1, Brushes.Red);
            DrawPoint(g, P2, Brushes.Green);
            DrawPoint(g, P3, Brushes.Green);
            DrawPoint(g, P4, Brushes.Blue);

            // Малюємо криву Без’є
            DrawBezier(g, P1, P2, P3, P4);
        }

        private void DrawPoint(Graphics g, PointF pt, Brush brush)
        {
            float r = 4;
            g.FillEllipse(brush, pt.X - r, pt.Y - r, r * 2, r * 2);
        }

        private void DrawBezier(Graphics g, PointF p1, PointF p2, PointF p3, PointF p4)
        {
            PointF prev = p1;
            int steps = 100;
            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                PointF pt = BezierPoint(t, p1, p2, p3, p4);
                g.DrawLine(Pens.DarkBlue, prev, pt);
                prev = pt;
            }
        }

        // Формула кубічної кривої Без’є
        private PointF BezierPoint(float t, PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            float x = uuu * p1.X + 3 * uu * t * p2.X + 3 * u * tt * p3.X + ttt * p4.X;
            float y = uuu * p1.Y + 3 * uu * t * p2.Y + 3 * u * tt * p3.Y + ttt * p4.Y;

            return new PointF(x, y);
        }
    }
}
