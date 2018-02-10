using System;
using System.Drawing;
using System.Drawing.Imaging;
using V;

namespace VExp
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector size = new Vector(400, 300);

            ExpVectorCross();
            ExpVectorDimensionalityChanges();

            ExpVectorArithmetic();
            VisualisationVectorSum(size);

            ExpVectorGeometry();

            Console.ReadLine();
        }

        public static void ExpVectorCross()
        {
            Vector cross2d = Vector.Cross(new Vector(1, 0));
            Vector cross3d = Vector.Cross(new Vector(1, 0, 0), new Vector(0, 1, 0));
            Vector cross4d = Vector.Cross(new Vector(1, 0, 0, 0), new Vector(0, 1, 0, 0), new Vector(0, 0, 1, 0));
        }

        private static void ExpVectorDimensionalityChanges()
        {
            Vector addDimension = Vector.Insert(new Vector(2, 2), 0, 1);
            Vector removeDimension = Vector.Remove(new Vector(2, 0, 2), 1);

            Vector prefixDimension = Vector.Prefix(new Vector(2, 2), 1);
            Vector unprefixDimension = Vector.UnPrefix(new Vector(1, 2, 2));

            Vector postfixDimension = Vector.Postfix(new Vector(2, 2), 1);
            Vector unpostfixDimension = Vector.UnPostfix(new Vector(2, 2, 1));

            Vector[] swap1 = Vector.Swap(new Vector(1, 2, 3));
            Vector[] swap2 = Vector.Swap(new Vector(1, 2, 3), new Vector(4, 5, 6));
        }

        private static void ExpVectorArithmetic()
        {
            Vector add1 = new Vector(1, 2) + new Vector(2, 1);
            Vector add2 = new Vector(1, 2) + 2;
            Vector add3 = 2 + new Vector(2, 1);

            Vector sub1 = new Vector(1, 2) - new Vector(2, 1);
            Vector sub2 = new Vector(1, 2) - 2;
            Vector sub3 = 2 - new Vector(2, 1);

            Vector mul1 = new Vector(1, 2) * new Vector(2, 1);
            Vector mul2 = new Vector(1, 2) * 2;
            Vector mul3 = 2 * new Vector(2, 1);

            Vector div1 = new Vector(1, 2) / new Vector(2, 1);
            Vector div2 = new Vector(1, 2) / 2;
            Vector div3 = 2 / new Vector(2, 1);

            Vector invert = -new Vector(1, 2, 3);

            Vector min = Vector.Min(new Vector(-1, 1, -1), new Vector(1, -1, 1));
            Vector max = Vector.Max(new Vector(-1, 1, -1), new Vector(1, -1, 1));
        }

        private static void VisualisationVectorSum(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector zero = Vector.Create(3, 0);

                Vector q = new Vector(-0.2, 0.3, -0.3);
                Vector qFlat = Vector.Set(q, 0, 1);
                render.DrawLine(zero, q, Color.Red);
                render.DrawLine(zero, qFlat, Color.Red, dotted: true);
                render.DrawLine(q, qFlat, Color.Red, dotted: true);
                render.DrawText(q, q.ToString(), Color.Red);

                Vector r = new Vector(0.4, 0.4, 0.1);
                Vector rFlat = Vector.Set(r, 0, 1);
                render.DrawLine(zero, r, Color.Green);
                render.DrawLine(zero, rFlat, Color.Green, dotted: true);
                render.DrawLine(r, rFlat, Color.Green, dotted: true);
                render.DrawText(r, r.ToString(), Color.Green);

                Vector sum = q + r;
                Vector sumFlat = Vector.Set(sum, 0, 1);
                render.DrawLine(zero, sum, Color.Blue);
                render.DrawLine(zero, sumFlat, Color.Blue, dotted: true);
                render.DrawLine(sum, sumFlat, Color.Blue, dotted: true);
                render.DrawText(sum, sum.ToString(), Color.Blue);

                render.Save("sum.png");
            }
        }

        private static void ExpVectorGeometry()
        {
            Vector normalize = Vector.Normalize(new Vector(1, 1));
            double dot = Vector.Dot(new Vector(1, 0), new Vector(0.5, 0.5));
        }
    }
}
