using NUnit.Framework;
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
    }
}
