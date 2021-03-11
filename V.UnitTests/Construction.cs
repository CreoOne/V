using System;
using Xunit;

namespace V.UnitTests
{
    public class Construction
    {
        [Fact]
        public void Constructor()
        {
            {
                Vector v = new Vector();
                Assert.Equal(0, v.Dimensions);
                Assert.Equal(new double[] { }, v.ToArray());
            }

            {
                Vector v = new Vector(1);
                Assert.Equal(1, v.Dimensions);
                Assert.Equal(new double[] { 1 }, v.ToArray());
            }

            {
                Vector v = new Vector(1, -2);
                Assert.Equal(2, v.Dimensions);
                Assert.Equal(new double[] { 1, -2 }, v.ToArray());
            }

            {
                Assert.Throws<ArgumentNullException>(() => new Vector(null));
            }
        }

        [Fact]
        public void Creator()
        {
            {
                Vector v = Vector.Create(0, 1);
                Assert.Equal(0, v.Dimensions);
                Assert.Equal(new double[] { }, v.ToArray());
            }

            {
                Vector v = Vector.Create(1, 2);
                Assert.Equal(1, v.Dimensions);
                Assert.Equal(new double[] { 2 }, v.ToArray());
            }

            {
                Vector v = Vector.Create(2, 3);
                Assert.Equal(2, v.Dimensions);
                Assert.Equal(new double[] { 3, 3 }, v.ToArray());
            }

            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Vector.Create(-1, 2));
            }
        }

        [Fact]
        public void Default()
        {
            {
                Vector v = default;
                Assert.Equal(0, v.Dimensions);
                Assert.Equal(new double[] { }, v.ToArray());
            }
        }

        [Fact]
        public void IndexAccess()
        {
            {
                Vector v = new Vector(0, 1, 2, 3);
                Assert.Equal(4, v.Dimensions);
                Assert.Equal(0, v[0]);
                Assert.Equal(1, v[1]);
                Assert.Equal(2, v[2]);
                Assert.Equal(3, v[3]);
                Assert.Throws<IndexOutOfRangeException>(() => v[4].ToString());
                Assert.Throws<IndexOutOfRangeException>(() => v[-1].ToString());
            }

            {
                Vector v = new Vector();
                Assert.Equal(0, v.Dimensions);
                Assert.Throws<IndexOutOfRangeException>(() => v[0].ToString());
            }
        }
    }
}
