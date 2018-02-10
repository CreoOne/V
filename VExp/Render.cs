using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using V;

namespace VExp
{
    internal sealed class Render : IDisposable
    {
        private Vector Size;
        private Bitmap Canvas;
        private Graphics Context;
        private Font Font;
        private float FontSize = 9;

        public Render(Vector size)
        {
            Size = size;
            Canvas = new Bitmap((int)Size[0], (int)Size[1]);

            Context = Graphics.FromImage(Canvas);
            Context.SmoothingMode = SmoothingMode.AntiAlias;
            Context.Clear(Color.White);

            Font = new Font(FontFamily.GenericSansSerif, FontSize);
        }

        public void DrawLine(Vector q, Vector r, Color color, bool dotted = false)
        {
            using (Pen pen = new Pen(color))
            {
                if (dotted)
                    pen.DashPattern = new float[] { 1, 3 };

                Context.DrawLine(pen, VectorToPointF(q), VectorToPointF(r));
            }
        }

        public void DrawText(Vector v, string text, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                PointF point = VectorToPointF(v);
                Context.DrawString(text, Font, brush, point.X - 2, point.Y - FontSize * 1.7f);
            }
        }

        public void DrawAxes()
        {
            Vector zero = Vector.Create(3, 0);
            string[] marks = new string[] { "x", "y", "z" };

            foreach(int dimension in Enumerable.Range(0, 3))
            {
                Vector positive = Vector.Set(zero, 1, dimension);
                Vector negative = -positive;

                DrawLine(negative, positive, Color.Silver);
                DrawText(positive, marks[dimension], Color.Silver);
            }
        }

        public void Save(string filename)
        {
            Canvas.Save(filename, ImageFormat.Png);
        }

        private Vector ProjectionToScreen(Vector v)
        {
            Vector halfScreen = Size / 2d;
            return Vector.Postfix(halfScreen, 0) + v * (halfScreen[1]) * new Vector(1, -1, 1);
        }

        private Vector ModelToWorld(Vector v)
        {
            Vector rotation = new Vector(-Math.PI / 15d, Math.PI / 10d, 0);
            Vector headRotation = Vector.RotateAroundAxis(v, new Vector(0, 1, 0), rotation[0]);
            Vector pitchRotation = Vector.RotateAroundAxis(headRotation, new Vector(1, 0, 0), rotation[1]);
            return pitchRotation + new Vector(0, 0, 1.5);
        }

        private Vector WorldToProjection(Vector v)
        {
            return v / new Vector(v[2], v[2], 1);
        }

        private PointF VectorToPointF(Vector v)
        {
            Vector transformed = ProjectionToScreen(ModelToWorld(v));
            return new PointF((float)transformed[0], (float)transformed[1]);
        }

        public void Dispose()
        {
            Context?.Dispose();
            Font?.Dispose();
        }
    }
}
