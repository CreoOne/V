using NUnit.Framework;
using System;
using System.Linq;
using V;

namespace VTest
{
    [TestFixture]
    public class Operations
    {
        private const double Precision = 1e-11;

        [Test]
        public void Swap()
        {
            {
                try
                {
                    Vector[] input = new Vector[]
                    {

                    };

                    Vector[] output = Vector.Swap(input);
                    Assert.Fail();
                }

                catch { }
            }

            {
                Vector[] output = Vector.Swap(default(Vector));

                Assert.AreEqual(0, output.Length);
            }

            {
                Vector[] output = Vector.Swap(new Vector(1));

                Assert.AreEqual(1, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2));

                Assert.AreEqual(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2), new Vector(3, 4));

                Assert.AreEqual(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1, 3), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2, 4), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2), new Vector(3, 4), new Vector(5, 6));

                Assert.AreEqual(2, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1, 3, 5), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2, 4, 6), output[1], Precision));
            }

            {
                Vector[] output = Vector.Swap(new Vector(1, 2, 3));

                Assert.AreEqual(3, output.Length);
                Assert.True(Vector.CloseEnough(new Vector(1), output[0], Precision));
                Assert.True(Vector.CloseEnough(new Vector(2), output[1], Precision));
                Assert.True(Vector.CloseEnough(new Vector(3), output[2], Precision));
            }
        }

        [Test]
        public void Insert()
        {
            Assert.That(Vector.CloseEnough(Vector.Insert(new Vector(), 1, 0), new Vector(1), Precision));
            Assert.That(Vector.CloseEnough(Vector.Insert(new Vector(3, 3), 1, 1), new Vector(3, 1, 3), Precision));
        }

        [Test]
        public void Remove()
        {
            Assert.That(Vector.CloseEnough(Vector.Remove(new Vector(1), 0), new Vector(), Precision));
            Assert.That(Vector.CloseEnough(Vector.Remove(new Vector(3, 1, 3), 1), new Vector(3, 3), Precision));
        }

        [Test]
        public void Prefix()
        {
            Assert.That(Vector.CloseEnough(Vector.Prefix(new Vector(1, 2), 3), new Vector(3, 1, 2), Precision));
        }

        [Test]
        public void Postfix()
        {
            Assert.That(Vector.CloseEnough(Vector.Postfix(new Vector(1, 2), 3), new Vector(1, 2, 3), Precision));
        }

        [Test]
        public void UnPrefix()
        {
            Assert.That(Vector.CloseEnough(Vector.UnPrefix(new Vector(1, 2, 3)), new Vector(2, 3), Precision));
        }

        [Test]
        public void UnPostfix()
        {
            Assert.That(Vector.CloseEnough(Vector.UnPostfix(new Vector(1, 2, 3)), new Vector(1, 2), Precision));
        }

        [Test]
        public void ToString_()
        {
            Assert.That(new Vector(1, 3.1415).ToString(), Is.EqualTo("[1, 3.1415]"));
        }

        [Test]
        public void ToEnumerable()
        {
            Assert.That(new Vector().ToEnumerable(), Is.Empty);
            Assert.That(new Vector(1, 2, 3).ToEnumerable().ToArray(), Is.EqualTo(new double[] { 1, 2, 3 }));
        }

        [Test]
        public void Map()
        {
            Assert.That(Vector.CloseEnough(Vector.Map(new Vector(1, 2, 3), (i) => i * 2d), new Vector(2, 4, 6), Precision));
        }

        [Test]
        public void Min()
        {
            Assert.That(Vector.CloseEnough(Vector.Min(new Vector(1, 2), new Vector(2, 1)), new Vector(1, 1), Precision));
        }

        [Test]
        public void Max()
        {
            Assert.That(Vector.CloseEnough(Vector.Max(new Vector(1, 2), new Vector(2, 1)), new Vector(2, 2), Precision));
        }

        [Test]
        public void Lerp()
        {
            Assert.That(Vector.CloseEnough(Vector.Lerp(new Vector(1, 2), new Vector(2, 3), 0.5), new Vector(1.5, 2.5), Precision));
        }

        [Test]
        public void Merge()
        {
            Func<double, double, int, double> operation = (q, r, i) => (q + r) * (i + 1);

            Assert.That(Vector.CloseEnough(Vector.Merge(new Vector(1, 2), new Vector(2, 1), operation), new Vector(3, 6), Precision));
            Assert.That(Vector.CloseEnough(Vector.Merge(new Vector(1, 2), 2, operation), new Vector(3, 8), Precision));
            Assert.That(Vector.CloseEnough(Vector.Merge(2, new Vector(1, 2), operation), new Vector(3, 8), Precision));
        }
    }
}
