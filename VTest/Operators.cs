using NUnit.Framework;
using V;

namespace VTest
{
    [TestFixture]
    public class Operators
    {
        [Test]
        public void Add()
        {
            Assert.That((default(Vector) + new Vector()).Dimensions, Is.EqualTo(0));

            Assert.That(Vector.CloseEnough(new Vector(1) + 1d, new Vector(2), 1e-11));
            Assert.That(Vector.CloseEnough(1d + new Vector(1), new Vector(2), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) + new Vector(1, 1)).ToString());
        }

        [Test]
        public void Subtract()
        {
            Assert.That((default(Vector) - new Vector()).Dimensions, Is.EqualTo(0));

            Assert.That(Vector.CloseEnough(new Vector(2) - 1d, new Vector(1), 1e-11));
            Assert.That(Vector.CloseEnough(2d - new Vector(1), new Vector(1), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) - new Vector(1, 1)).ToString());
        }

        [Test]
        public void Multiply()
        {
            Assert.That((default(Vector) * new Vector()).Dimensions, Is.EqualTo(0));

            Assert.That(Vector.CloseEnough(new Vector(2) * 3d, new Vector(6), 1e-11));
            Assert.That(Vector.CloseEnough(2d * new Vector(3), new Vector(6), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) * new Vector(1, 1)).ToString());
        }


        [Test]
        public void Divide()
        {
            Assert.That((default(Vector) / new Vector()).Dimensions, Is.EqualTo(0));

            Assert.That(Vector.CloseEnough(new Vector(4) / 2d, new Vector(2), 1e-11));
            Assert.That(Vector.CloseEnough(4d / new Vector(2), new Vector(2), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) / new Vector(1, 1)).ToString());
        }
    }
}
