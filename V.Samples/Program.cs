using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace V.Samples
{
    class Program
    {
        private const int TraceAlpha = 80;

        static void Main(string[] args)
        {
            Vector size = new Vector(400, 300);

            ExpVectorArithmetic();
            VisualizationVectorSum(size);
            VisualizationVectorSub(size);

            ExpVectorFunctions();
            VisualizationVectorRotateAroundAxis(size);
            VisualizationVectorLinearInterpolation(size);
            VisualizationVectorLinearExtrapolation(size);

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

                render.Save(GetPathToFile("sum.png"));
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

                render.Save(GetPathToFile("sub.png"));
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

            Vector inter = Vector.Lerp(new Vector(-0.8, 0.3, -0.5), new Vector(0.4, 0.5, 0.3), 0.5);
            // inter is now [-0.2, 0.4, -0.1]

            Vector extra = Vector.Lerp(new Vector(-0.8, 0.3, -0.5), new Vector(0.4, 0.5, 0.3), 1.2);
            // extra is now [0.64, 0.54, 0.46]
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
                DrawTrace(render, (p) => Vector.RotateAroundAxis(q, axis, p * -angle), Color.Silver, Color.Silver, 50);
                DrawTrace(render, (p) => Vector.RotateAroundAxis(q, axis, p * angle), Color.Green, Color.Blue, 50);

                Vector raa = Vector.RotateAroundAxis(q, axis, angle);
                DrawLine(render, raa, Color.Blue);

                render.Save(GetPathToFile("raa.png"));
            }
        }

        private static void VisualizationVectorLinearInterpolation(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = new Vector(-0.8, 0.3, -0.5);
                DrawLine(render, q, Color.Green);

                Vector r = new Vector(0.4, 0.5, 0.3);
                DrawLine(render, r, Color.Green);

                Vector half = Vector.Lerp(q, r, 0.5);
                DrawLine(render, half, Color.Blue);

                DrawTrace(render, (p) => Vector.Lerp(q, half, p), Color.Green, Color.Blue, 10);
                DrawTrace(render, (p) => Vector.Lerp(r, half, p), Color.Green, Color.Blue, 10);

                render.Save(GetPathToFile("lerpIn.png"));
            }
        }

        private static void VisualizationVectorLinearExtrapolation(Vector size)
        {
            using (Render render = new Render(size))
            {
                render.DrawAxes();

                Vector q = new Vector(-0.8, 0.3, -0.5);
                DrawLine(render, q, Color.Green);

                Vector r = new Vector(0.4, 0.5, 0.3);
                DrawLine(render, r, Color.Green);

                Vector extra = Vector.Lerp(q, r, 1.2);
                DrawLine(render, extra, Color.Blue);

                render.DrawLine(q, r, Color.FromArgb(TraceAlpha, Color.Green));
                DrawTrace(render, (p) => Vector.Lerp(r, extra, p), Color.Green, Color.Blue, 10);

                render.Save(GetPathToFile("lerpEx.png"));
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

                render.Save(GetPathToFile("cross.png"));
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

        private static void DrawTrace(Render render, Func<double, Vector> traceFunction, Color qColor, Color rColor, int steps)
        {
            Vector sourceColor = ColorToVector(qColor);
            Vector destinationColor = ColorToVector(rColor);

            Vector lastPosition = default;

            foreach (int index in Enumerable.Range(0, steps + 1))
            {
                double position = index / (double)steps;
                Vector currentPosition = traceFunction(position);

                if (index > 0)
                {
                    Vector currentColor = Vector.Lerp(sourceColor, destinationColor, position);
                    render.DrawLine(lastPosition, currentPosition, Color.FromArgb(TraceAlpha, VectorToColor(currentColor)));
                }

                lastPosition = currentPosition;
            }
        }

        private static string GetPathToFile(string filename)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img");
            Directory.CreateDirectory(directory);
            return Path.Combine(directory, filename);
        }
    }
}
