using System;
using System.Drawing;
using System.Linq;
using V;

namespace VExp
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector size = new Vector(400, 300);

            ExpVectorArithmetic();
            VisualizationVectorSum(size);
            VisualizationVectorSub(size);

            ExpVectorFunctions();
            VisualizationVectorRotateAroundAxis(size);

            ExpVectorCross();
            VisualizationVectorCross(size);

            ExpVectorDimensionalityChanges();

            Console.ReadLine();
        }

        private static void ExpVectorArithmetic()
        {
            Vector add1 = new Vector(1, 2) + new Vector(2, 1);
            // add1 is now [3, 3]

            Vector add2 = new Vector(1, 2) + 2;
            // add2 is now [3, 4]

            Vector add3 = 2 + new Vector(2, 1);
            // add3 is now [4, 3]

            Vector sub1 = new Vector(1, 2) - new Vector(2, 1);
            // sub1 is now [1, 1]

            Vector sub2 = new Vector(1, 2) - 2;
            // sub2 is now [-1, 0]

            Vector sub3 = 2 - new Vector(2, 1);
            // sub3 is now [0, 1]

            Vector mul1 = new Vector(1, 2) * new Vector(2, 1);
            // mul1 is now [2, 2]

            Vector mul2 = new Vector(1, 2) * 2;
            // mul2 is now [2, 4]

            Vector mul3 = 2 * new Vector(2, 1);
            // mul3 is now [4, 2]

            Vector div1 = new Vector(1, 2) / new Vector(2, 1);
            // div1 is now [0.5, 2]

            Vector div2 = new Vector(1, 2) / 2;
            // div2 is now [0.5, 1]

            Vector div3 = 2 / new Vector(2, 1);
            // div 3 is now [1, 2]

            Vector inv = -new Vector(1, -2, 3);
            // inv is now [-1, 2, -3]
        }

        private static void VisualizationVectorSum(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = new Vector(-0.4, 0.3, -0.6);
                DrawLine(render, q, Color.Green);

                Vector r = new Vector(0.7, 0.4, 0.3);
                DrawLine(render, r, Color.Green);

                Vector sum = q + r;
                DrawLine(render, sum, Color.Blue);

                render.Save(@"..\..\img\sum.png");
            }
        }

        private static void VisualizationVectorSub(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = new Vector(-0.4, 0.6, -0.5);
                DrawLine(render, q, Color.Green);

                Vector r = new Vector(0.4, 0.3, 0.2);
                DrawLine(render, r, Color.Green);

                Vector sub = q - r;
                DrawLine(render, sub, Color.Blue);

                render.Save(@"..\..\img\sub.png");
            }
        }

        private static void ExpVectorFunctions()
        {
            Vector min1 = Vector.Min(new Vector(-1, 1, -1), new Vector(1, -1, 1));
            // min1 is now [-1, -1, -1]

            Vector min2 = Vector.Min(new Vector(1, 2, 3), new Vector(1, -1, 1), new Vector(5, 1, -1));
            // min2 is now [1, -1, -1]

            Vector max = Vector.Max(new Vector(-1, 1, -1), new Vector(1, -1, 1));
            // max is now [1, 1, 1]

            Vector nor = Vector.Normalize(new Vector(12, 0, 0));
            // nor is now [1, 0, 0]

            double dot = Vector.Dot(new Vector(1, 0), new Vector(0.5, 0.5));
            // dot is now 0.5

            double diff = Vector.AngleDifference(new Vector(0, 1), Vector.Create(2, 0), new Vector(1, 0));
            // diff is now half PI

            Vector raa = Vector.RotateAroundAxis(new Vector(1, 0, 0), new Vector(0, 1, 0), Math.PI / 2d);
            // raa is now [0, 0, 1]

            bool closeEnough = Vector.CloseEnough(new Vector(0, 1, -0.1), new Vector(0, 1, 0.1), 0.5);
            // closeEnough is True

            bool notCloseEnough = Vector.CloseEnough(new Vector(0, 1, -0.1), new Vector(0, 1, 0.1), 0.01);
            // notCloseEnough is False
        }

        private static void VisualizationVectorRotateAroundAxis(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = new Vector(0.5, 0.2, 0.6);
                DrawLine(render, q, Color.Green);

                Vector axis = new Vector(0.2, 0.5, -0.1);
                DrawLine(render, axis, Color.Red);

                double angle = -Math.PI;
                int steps = 100;
                Vector lastPosition = q;
                Vector sourceColor = ColorToVector(Color.Green);
                Vector destinationColor = ColorToVector(Color.Blue);

                foreach (int index in Enumerable.Range(1, steps))
                {
                    double position = index / (double)steps;
                    Vector currentPosition = Vector.RotateAroundAxis(q, axis, position * angle);
                    Vector currentColor = Vector.Lerp(sourceColor, destinationColor, position);
                    render.DrawLine(lastPosition, currentPosition, Color.FromArgb(100, VectorToColor(currentColor)));
                    lastPosition = currentPosition;
                }

                Vector raa = Vector.RotateAroundAxis(q, axis, angle);
                DrawLine(render, raa, Color.Blue);

                render.Save(@"..\..\img\raa.png");
            }
        }

        private static Color VectorToColor(Vector v)
        {
            return Color.FromArgb((int)v[0], (int)v[1], (int)v[2]);
        }

        private static Vector ColorToVector(Color color)
        {
            return new Vector(color.R, color.G, color.B);
        }

        public static void ExpVectorCross()
        {
            Vector cross2d = Vector.Cross(new Vector(1, 0));
            // cross2d is now [0, -1]

            Vector cross3d = Vector.Cross(new Vector(1, 0, 0), new Vector(0, 1, 0));
            // cross3d is now [0, 0, 1]

            Vector cross4d = Vector.Cross(new Vector(1, 0, 0, 0), new Vector(0, 1, 0, 0), new Vector(0, 0, 1, 0));
            // cross4d is now [0, 0, 0, -1]
        }

        private static void VisualizationVectorCross(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = Vector.Normalize(new Vector(-0.2, 0.3, -0.3));
                DrawLine(render, q, Color.Green);

                Vector r = Vector.Normalize(new Vector(0.4, 0.4, 0.1));
                DrawLine(render, r, Color.Green);

                Vector cross = Vector.Cross(q, r);
                DrawLine(render, cross, Color.Blue);

                render.Save(@"..\..\img\cross.png");
            }
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

        private static void DrawLine(Render render, Vector v, Color color)
        {
            Vector zero = Vector.Create(3, 0);

            render.DrawLine(zero, v, color);

            Vector vOnXZ = Vector.Set(v, 0, 1);
            render.DrawLine(v, vOnXZ, color, dotted: true);

            Vector vOnX = Vector.Set(vOnXZ, 0, 2);
            render.DrawLine(vOnXZ, vOnX, color, dotted: true);

            Vector vOnZ = Vector.Set(vOnXZ, 0, 0);
            render.DrawLine(vOnXZ, vOnZ, color, dotted: true);

            render.DrawText(v, Vector.Map(v, (d) => Math.Round(d, 3)).ToString(), color);
        }
    }
}
