using Xunit;

namespace V.UnitTests
{
    public class Operators
    {
        [Fact]
        public void Add()
        {
            Assert.Equal(0, (default(Vector) + new Vector()).Dimensions);

            Assert.True(Vector.CloseEnough(new Vector(1) + 1d, new Vector(2), 1e-11));
            Assert.True(Vector.CloseEnough(1d + new Vector(1), new Vector(2), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) + new Vector(1, 1)).ToString());
        }

        [Fact]
        public void Subtract()
        {
            Assert.Equal(0, (default(Vector) - new Vector()).Dimensions);

            Assert.True(Vector.CloseEnough(new Vector(2) - 1d, new Vector(1), 1e-11));
            Assert.True(Vector.CloseEnough(2d - new Vector(1), new Vector(1), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) - new Vector(1, 1)).ToString());
        }

        [Fact]
        public void Multiply()
        {
            Assert.Equal(0, (default(Vector) * new Vector()).Dimensions);

            Assert.True(Vector.CloseEnough(new Vector(2) * 3d, new Vector(6), 1e-11));
            Assert.True(Vector.CloseEnough(2d * new Vector(3), new Vector(6), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) * new Vector(1, 1)).ToString());
        }


        [Fact]
        public void Divide()
        {
            Assert.Equal(0, (default(Vector) / new Vector()).Dimensions);

            Assert.True(Vector.CloseEnough(new Vector(4) / 2d, new Vector(2), 1e-11));
            Assert.True(Vector.CloseEnough(4d / new Vector(2), new Vector(2), 1e-11));

            Assert.Throws<DimensionalityMismatchException>(() => (new Vector(1) / new Vector(1, 1)).ToString());
        }

        [Fact]
        public void Negative()
        {
            Assert.True(Vector.CloseEnough(-new Vector(1), new Vector(-1), 1e-11));
        }
    }
}
