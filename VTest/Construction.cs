using System;
using V;
using NUnit.Framework;

namespace VTest
{
    [TestFixture]
    public class Construction
    {
        [Test]
        public void Constructor()
        {
            {
                Vector v = new Vector();
                Assert.AreEqual(0, v.Dimensions);
                Assert.AreEqual(new double[] { }, v.ToArray());
            }

            {
                Vector v = new Vector(1);
                Assert.AreEqual(1, v.Dimensions);
                Assert.AreEqual(new double[] { 1 }, v.ToArray());
            }

            {
                Vector v = new Vector(1, -2);
                Assert.AreEqual(2, v.Dimensions);
                Assert.AreEqual(new double[] { 1, -2 }, v.ToArray());
            }
        }

        [Test]
        public void Creator()
        {
            {
                Vector v = Vector.Create(0, 1);
                Assert.AreEqual(0, v.Dimensions);
                Assert.AreEqual(new double[] { }, v.ToArray());
            }

            {
                Vector v = Vector.Create(1, 2);
                Assert.AreEqual(1, v.Dimensions);
                Assert.AreEqual(new double[] { 2 }, v.ToArray());
            }

            {
                Vector v = Vector.Create(2, 3);
                Assert.AreEqual(2, v.Dimensions);
                Assert.AreEqual(new double[] { 3, 3 }, v.ToArray());
            }

            {
                try
                {
                    Vector.Create(-1, 2);
                    Assert.Fail();
                }

                catch { }
            }
        }

        [Test]
        public void Default()
        {
            {
                Vector v = default(Vector);
                Assert.AreEqual(0, v.Dimensions);
                Assert.AreEqual(new double[] { }, v.ToArray());
            }
        }
    }
}
