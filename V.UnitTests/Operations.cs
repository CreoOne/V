using System;
using System.Linq;
using Xunit;

namespace V.UnitTests
{
    public class Operations
    {
        private const double Precision = 1e-11;

        [Fact]
        public void Swap()
        {
            {
                Vector[] input = new Vector[] { };

                Assert.Throws<IndexOutOfRangeException>(() => Vector.Swap(input));
            }

            {
                Vector[] output = Vector.Swap(default(Vector));

                Assert.Empty(output);
            }

            {
                Vector[] output = Vector.Swap(new Vector(1));

                Assert.Single(output);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2));

                Assert.Equal(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2), new Vector(3, 4));

                Assert.Equal(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1, 3), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2, 4), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2), new Vector(3, 4), new Vector(5, 6));

                Assert.Equal(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1, 3, 5), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2, 4, 6), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2, 3));

                Assert.Equal(3, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2), output[1], Precision));
                Assert.True(Vector.CloseEnough(new Vector(3), output[2], Precision));
            }
        }

        [Fact]
        public void Insert()
        {
            Assert.True(Vector.CloseEnough(Vector.Insert(new Vector(), 1, 0), new Vector(1), Precision));
            Assert.True(Vector.CloseEnough(Vector.Insert(new Vector(3, 3), 1, 1), new Vector(3, 1, 3), Precision));
        }

        [Fact]
        public void Remove()
        {
            Assert.True(Vector.CloseEnough(Vector.Remove(new Vector(1), 0), new Vector(), Precision));
            Assert.True(Vector.CloseEnough(Vector.Remove(new Vector(3, 1, 3), 1), new Vector(3, 3), Precision));
        }

        [Fact]
        public void Prefix()
        {
            Assert.True(Vector.CloseEnough(Vector.Prefix(new Vector(1, 2), 3), new Vector(3, 1, 2), Precision));
        }

        [Fact]
        public void Postfix()
        {
            Assert.True(Vector.CloseEnough(Vector.Postfix(new Vector(1, 2), 3), new Vector(1, 2, 3), Precision));
        }

        [Fact]
        public void UnPrefix()
        {
            Assert.True(Vector.CloseEnough(Vector.UnPrefix(new Vector(1, 2, 3)), new Vector(2, 3), Precision));
        }

        [Fact]
        public void UnPostfix()
        {
            Assert.True(Vector.CloseEnough(Vector.UnPostfix(new Vector(1, 2, 3)), new Vector(1, 2), Precision));
        }

        [Fact]
        public void ToString_()
        {
            Assert.Equal("[1, 3.1415]", new Vector(1, 3.1415).ToString());
        }

        [Fact]
        public void ToEnumerable()
        {
            Assert.Empty(new Vector().ToEnumerable());
            Assert.Equal(new Vector(1, 2, 3).ToEnumerable().ToArray(), new double[] { 1, 2, 3 });
        }

        [Fact]
        public void Map()
        {
            Assert.True(Vector.CloseEnough(Vector.Map(new Vector(1, 2, 3), (i) => i * 2d), new Vector(2, 4, 6), Precision));
        }

        [Fact]
        public void Min()
        {
            Assert.True(Vector.CloseEnough(Vector.Min(new Vector(1, 2), new Vector(2, 1)), new Vector(1, 1), Precision));
        }

        [Fact]
        public void Max()
        {
            Assert.True(Vector.CloseEnough(Vector.Max(new Vector(1, 2), new Vector(2, 1)), new Vector(2, 2), Precision));
        }

        [Fact]
        public void Lerp()
        {
            Assert.True(Vector.CloseEnough(Vector.Lerp(new Vector(1, 2), new Vector(2, 3), 0.5), new Vector(1.5, 2.5), Precision));
        }

        [Fact]
        public void Merge()
        {
            Func<double, double, int, double> operation = (q, r, i) => (q + r) * (i + 1);

            Assert.True(Vector.CloseEnough(Vector.Merge(new Vector(1, 2), new Vector(2, 1), operation), new Vector(3, 6), Precision));
            Assert.True(Vector.CloseEnough(Vector.Merge(new Vector(1, 2), 2, operation), new Vector(3, 8), Precision));
            Assert.True(Vector.CloseEnough(Vector.Merge(2, new Vector(1, 2), operation), new Vector(3, 8), Precision));
        }

        [Fact]
        public void Set()
        {
            Assert.True(Vector.CloseEnough(Vector.Set(new Vector(4, 4, 4, 4), 2, 1), new Vector(4, 2, 4, 4), Precision));
        }

        [Fact]
        public void Swizzle()
        {
            Assert.True(Vector.CloseEnough(Vector.Swizzle(new Vector(1, 2, 3, 4), new[] { 0, 3, 1, 2 }), new Vector(1, 4, 2, 3), Precision));
        }
    }
}
