using System;
using System.Drawing;
using System.Windows.Forms;

namespace KOCH
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Фрактал Коха на трикутнику";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            this.Paint += MainForm_Paint;
        }

        // Точка трикутника
        PointF P1 = new PointF(300, 500);
        PointF P2 = new PointF(700, 500);
        PointF P3 = new PointF(500, 200);

        int K = 2; 

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawKoch(g, P1, P2, K);
            DrawKoch(g, P2, P3, K);
            DrawKoch(g, P3, P1, K);
        }

        // Основна функція для побудови фракталу Коха
        private void DrawKoch(Graphics g, PointF a, PointF b, int order)
        {
            if (order == 0)
            {
                g.DrawLine(Pens.Blue, a, b);
            }
            else
            {
                PointF oneThird = new PointF(
                    a.X + (b.X - a.X) / 3,
                    a.Y + (b.Y - a.Y) / 3
                );

                PointF twoThird = new PointF(
                    a.X + 2 * (b.X - a.X) / 3,
                    a.Y + 2 * (b.Y - a.Y) / 3
                );

                
                float dx = twoThird.X - oneThird.X;
                float dy = twoThird.Y - oneThird.Y;

                PointF peak = new PointF(
                    (float)(oneThird.X + dx / 2 - Math.Sqrt(3) * dy / 2),
                    (float)(oneThird.Y + dy / 2 + Math.Sqrt(3) * dx / 2)
                );

                // Рекурсія
                DrawKoch(g, a, oneThird, order - 1);
                DrawKoch(g, oneThird, peak, order - 1);
                DrawKoch(g, peak, twoThird, order - 1);
                DrawKoch(g, twoThird, b, order - 1);
            }
        }
    }
}
