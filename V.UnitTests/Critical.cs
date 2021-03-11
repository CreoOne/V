using System;
using Xunit;

namespace V.UnitTests
{
    public class Critical
    {
        private const double PrecisionE = 1e-11;
        private const int Precision = 11;
        private const double HalfPI = Math.PI / 2d;

        [Fact]
        public void CloseEnough()
        {
            Assert.True(Vector.CloseEnough(default, default, PrecisionE));
            Assert.True(Vector.CloseEnough(new Vector(1), new Vector(1), PrecisionE));
            Assert.False(Vector.CloseEnough(new Vector(0.3333333333), new Vector(1) / 3, PrecisionE));
            Assert.True(Vector.CloseEnough(new Vector(0.33333333333), new Vector(1) / 3, PrecisionE));
            Assert.False(Vector.CloseEnough(new Vector(1, -1), new Vector(-1, 1), PrecisionE));
        }

        [Fact]
        public void AngleDifference()
        {
            Assert.Equal(0, Vector.AngleDifference(default, default, default), Precision);
            Assert.Equal(0, Vector.AngleDifference(new Vector(1, 0), Vector.Create(2, 0), new Vector(1, 0)), Precision);
            Assert.Equal(0, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(1, 0, 0)), Precision);

            Assert.Equal(HalfPI, Vector.AngleDifference(new Vector(1, 0), Vector.Create(2, 0), new Vector(0, 1)), Precision);
            Assert.Equal(HalfPI, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(0, 0, 1)), Precision);
            Assert.Equal(HalfPI, Vector.AngleDifference(new Vector(1, 0, 0), Vector.Create(3, 0), new Vector(0, -1, 0)), Precision);
        }

        [Fact]
        public void RotateAroundAxis()
        {
            Assert.True(Vector.CloseEnough(new Vector(1, 0, 0), Vector.RotateAroundAxis(new Vector(0, 0, 1), new Vector(0, 1, 0), -HalfPI), PrecisionE));
        }
    }
}
