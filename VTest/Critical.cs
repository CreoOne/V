using System;
using NUnit.Framework;
using V;

namespace VTest
{
    [TestFixture]
    public class Critical
    {
        private const double Precision = 1e-11;
        private const double HalfPI = Math.PI / 2d;

        [Test]
        public void CloseEnough()
        {
            Assert.True(Vector.CloseEnough(default(Vector), default(Vector), Precision));
            Assert.True(Vector.CloseEnough(new Vector(1), new Vector(1), Precision));
            Assert.False(Vector.CloseEnough(new Vector(0.3333333333), new Vector(1) / 3, Precision));
            Assert.True(Vector.CloseEnough(new Vector(0.33333333333), new Vector(1) / 3, Precision));
            Assert.False(Vector.CloseEnough(new Vector(1, -1), new Vector(-1, 1), Precision));
        }

        [Test]
        public void AngleDifference()
        {
            Assert.AreEqual(0, Vector.AngleDifference(default(Vector), default(Vector), default(Vector)), Precision);
            Assert.AreEqual(0, Vector.AngleDifference(new Vector(1, 0), Vector.Create(2, 0), new Vector(1, 0)), Precision);
            Assert.AreEqual(0, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(1, 0, 0)), Precision);

            Assert.AreEqual(HalfPI, Vector.AngleDifference(new Vector(1, 0), Vector.Create(2, 0), new Vector(0, 1)), Precision);
            Assert.AreEqual(HalfPI, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(0, 0, 1)), Precision);
            Assert.AreEqual(HalfPI, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(0, -1, 0)), Precision);
        }

        [Test]
        public void RotateAroundAxis()
        {
            Assert.True(Vector.CloseEnough(new Vector(1, 0, 0), Vector.RotateAroundAxis(new Vector(0, 0, 1), new Vector(0, 1, 0), -HalfPI), Precision));
        }
    }
}
