using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V;

namespace VExp
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector cross2d = Vector.Cross(new Vector(1, 0));
            Vector cross3d = Vector.Cross(new Vector(1, 0, 0), new Vector(0, 1, 0));
            Vector cross4d = Vector.Cross(new Vector(1, 0, 0, 0), new Vector(0, 1, 0, 0), new Vector(0, 0, 1, 0));

            Vector addDimension = Vector.Insert(new Vector(2, 2), 0, 1);
            Vector removeDimension = Vector.Remove(new Vector(2, 0, 2), 1);

            Vector prefixDimension = Vector.Prefix(new Vector(2, 2), 1);
            Vector unprefixDimension = Vector.UnPrefix(new Vector(1, 2, 2));

            Vector postfixDimension = Vector.Postfix(new Vector(2, 2), 1);
            Vector unpostfixDimension = Vector.UnPostfix(new Vector(2, 2, 1));

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

            Vector normalize = Vector.Normalize(new Vector(1, 1));
            double dot = Vector.Dot(new Vector(1, 0), new Vector(0.5, 0.5));

            Vector[] swap1 = Vector.Swap(new Vector(1, 2, 3));
            Vector[] swap2 = Vector.Swap(new Vector(1, 2, 3), new Vector(4, 5, 6));

            Vector min = Vector.Min(new Vector(-1, 1, -1), new Vector(1, -1, 1));
            Vector max = Vector.Max(new Vector(-1, 1, -1), new Vector(1, -1, 1));
        }
    }
}
