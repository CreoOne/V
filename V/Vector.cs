using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace V
{
    public readonly struct Vector
    {
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

        /// <summary>
        /// Dimensions of vector
        /// </summary>
        public int Dimensions { get; }

        /// <summary>
        /// Concrete values in corresponding dimensions
        /// </summary>
        private double[] Values { get; }

        public double LengthSquared => Aggregate(this, (r, q) => r + q * q);
        public double Length => Math.Sqrt(LengthSquared);

        public double this[int index] => Values[index];

        /// <summary>
        /// Creates new vector with all dimensions filled with value
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Create(int dimensions, double value) => new Vector(Enumerable.Repeat(value, dimensions));

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        [DebuggerStepThrough]
        public Vector(params double[] values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Dimensions = values.Length;
        }

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        [DebuggerStepThrough]
        public Vector(IEnumerable<double> values) : this((values ?? throw new ArgumentNullException(nameof(values))).ToArray()) { }


        /// <summary>
        /// Creates unit vector
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Normalize(Vector q) => q / q.Length;

        /// <summary>
        /// Creates dot product of two vectors
        /// </summary>
        [DebuggerStepThrough]
        public static double Dot(Vector q, Vector r) => Aggregate(q * r, (a, b) => a + b);

        /// <summary>
        /// Creates cross product of vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Cross(params Vector[] vectors)
        {
            EnsureConsistentDimensionality(vectors);

            if (vectors[0].Dimensions != vectors.Length + 1)
                throw new ArgumentException();

            return Map(Create(vectors[0].Dimensions, 1), (value, index) => DetWeave(value, vectors, index));
        }

        /// <summary>
        /// Creates determinant of vectors
        /// </summary>
        [DebuggerStepThrough]
        public static double Det(params Vector[] vectors)
        {
            EnsureConsistentDimensionality(vectors);

            if (vectors[0].Dimensions != vectors.Length)
                throw new ArgumentException();

            if (vectors[0].Dimensions == 1)
                return vectors[0].Values[0];

            return Aggregate(vectors[0], (result, value, index) => result + DetWeave(value, vectors.Skip(1), index));
        }

        private static double DetWeave(double value, IEnumerable<Vector> vectors, int index)
        {
            return (index % 2 == 0 ? 1 : -1) * (value * Det(vectors.Select(d => Remove(d, index)).ToArray()));
        }

        /// <summary>
        /// Proximity check
        /// </summary>
        [DebuggerStepThrough]
        public static bool CloseEnough(Vector q, Vector r, double distance)
        {
            EnsureConsistentDimensionality(q, r);

            return (r - q).LengthSquared <= distance * distance;
        }

        /// <summary>
        /// Angle difference between two vectors in radians
        /// </summary>
        [DebuggerStepThrough]
        public static double AngleDifference(Vector q, Vector o, Vector r)
        {
            EnsureConsistentDimensionality(q, o, r);

            if (q.Dimensions == 0)
                return 0;

            return Math.Acos(Dot(Normalize(q - o), Normalize(r - o)));
        }

        /// <summary>
        /// Rotate vector around axis by angle in radians
        /// </summary>
        [DebuggerStepThrough]
        public static Vector RotateAroundAxis(Vector vector, Vector axis, double angle)
        {
            EnsureConsistentDimensionality(vector, axis);

            double CosTheta = Math.Cos(-angle);
            axis = Normalize(axis);

            // v * cos(theta) + cross(k, v) * sin(theta) + k * dot(k, v) * (1 - cos(theta));
            return vector * CosTheta + Cross(axis, vector) * Math.Sin(-angle) + axis * Dot(axis, vector) * (1.0f - CosTheta);
        }

        /// <summary>
        /// Min value vector from vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Min(params Vector[] vectors)
        {
            EnsureConsistentDimensionality(vectors);

            Vector[] swapped = Swap(vectors);
            double[] values = new double[vectors[0].Dimensions];

            for (int index = 0; index < swapped.Length; index++)
                values[index] = Aggregate(swapped[index], (result, q) => Math.Min(result, q), double.MaxValue);

            return new Vector(values);
        }

        /// <summary>
        /// Max value vector from vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Max(params Vector[] vectors)
        {
            EnsureConsistentDimensionality(vectors);

            Vector[] swapped = Swap(vectors);
            double[] values = new double[vectors[0].Dimensions];

            for (int index = 0; index < swapped.Length; index++)
                values[index] = Aggregate(swapped[index], (result, q) => Math.Max(result, q), double.MinValue);

            return new Vector(values);
        }

        /// <summary>
        /// Linear interpolation
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Lerp(Vector q, Vector r, double position)
        {
            EnsureConsistentDimensionality(q, r);

            return q + (r - q) * position;
        }

        #region Operators
        [DebuggerStepThrough]
        public static Vector operator -(Vector q) => q * -1;

        [DebuggerStepThrough]
        public static Vector operator +(Vector q, Vector r)
        {
            EnsureConsistentDimensionality(q, r);

            return Merge(q, r, (a, b) => a + b);
        }

        [DebuggerStepThrough]
        public static Vector operator -(Vector q, Vector r)
        {
            EnsureConsistentDimensionality(q, r);

            return Merge(q, r, (a, b) => a - b);
        }

        [DebuggerStepThrough]
        public static Vector operator *(Vector q, Vector r)
        {
            EnsureConsistentDimensionality(q, r);

            return Merge(q, r, (a, b) => a * b);
        }

        [DebuggerStepThrough]
        public static Vector operator /(Vector q, Vector r)
        {
            EnsureConsistentDimensionality(q, r);

            return Merge(q, r, (a, b) => a / b);
        }

        [DebuggerStepThrough]
        public static Vector operator +(Vector q, double r) => Merge(q, r, (a, b) => a + b);

        [DebuggerStepThrough]
        public static Vector operator -(Vector q, double r) => Merge(q, r, (a, b) => a - b);

        [DebuggerStepThrough]
        public static Vector operator *(Vector q, double r) => Merge(q, r, (a, b) => a * b);

        [DebuggerStepThrough]
        public static Vector operator /(Vector q, double r) => Merge(q, r, (a, b) => a / b);

        [DebuggerStepThrough]
        public static Vector operator +(double q, Vector r) => Merge(q, r, (a, b) => a + b);

        [DebuggerStepThrough]
        public static Vector operator -(double q, Vector r) => Merge(q, r, (a, b) => a - b);

        [DebuggerStepThrough]
        public static Vector operator *(double q, Vector r) => Merge(q, r, (a, b) => a * b);

        [DebuggerStepThrough]
        public static Vector operator /(double q, Vector r) => Merge(q, r, (a, b) => a / b);
        #endregion

        #region Operations
        /// <summary>
        /// Swaps
        /// </summary>
        [DebuggerStepThrough]
        public static Vector[] Swap(params Vector[] vectors)
        {
            EnsureConsistentDimensionality(vectors);

            Vector[] result = new Vector[vectors[0].Dimensions];

            for (int dimension = 0; dimension < vectors[0].Dimensions; dimension++)
            {
                double[] values = new double[vectors.Length];

                for (int index = 0; index < vectors.Length; index++)
                    values[index] = vectors[index].Values[dimension];

                result[dimension] = new Vector(values);
            }

            return result;
        }

        /// <summary>
        /// Merges corresponding dimensions of two vectors using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(Vector q, Vector r, Func<double, double, double> operation)
        {
            EnsureConsistentDimensionality(q, r);

            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r.Values[index]);

            return new Vector(values);
        }

        /// <summary>
        /// Merges corresponding dimensions of two vectors using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(Vector q, Vector r, Func<double, double, int, double> operation)
        {
            EnsureConsistentDimensionality(q, r);

            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r.Values[index], index);

            return new Vector(values);
        }

        /// <summary>
        /// Merges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(double q, Vector r, Func<double, double, double> operation)
        {
            double[] values = new double[r.Dimensions];

            for (int index = 0; index < r.Dimensions; index++)
                values[index] = operation(q, r.Values[index]);

            return new Vector(values);
        }

        /// <summary>
        /// Merges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(double q, Vector r, Func<double, double, int, double> operation)
        {
            double[] values = new double[r.Dimensions];

            for (int index = 0; index < r.Dimensions; index++)
                values[index] = operation(q, r.Values[index], index);

            return new Vector(values);
        }

        /// <summary>
        /// Merges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(Vector q, double r, Func<double, double, double> operation)
        {
            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r);

            return new Vector(values);
        }

        /// <summary>
        /// Merges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Merge(Vector q, double r, Func<double, double, int, double> operation)
        {
            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r, index);

            return new Vector(values);
        }

        /// <summary>
        /// Applies operation function to every dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Map(Vector q, Func<double, double> operation)
        {
            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index]);

            return new Vector(values);
        }

        /// <summary>
        /// Applies operation function to every dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Map(Vector q, Func<double, int, double> operation)
        {
            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], index);

            return new Vector(values);
        }

        /// <summary>
        /// Aggregates value of every dimension using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static double Aggregate(Vector q, Func<double, double, double> operation, double entry = default)
        {
            for (int index = 0; index < q.Dimensions; index++)
                entry = operation(entry, q.Values[index]);

            return entry;
        }

        /// <summary>
        /// Aggregates value of every dimension using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static double Aggregate(Vector q, Func<double, double, int, double> operation, double entry = default)
        {
            for (int index = 0; index < q.Dimensions; index++)
                entry = operation(entry, q.Values[index], index);

            return entry;
        }

        /// <summary>
        /// Inserts value in corresponding dimension shifting rest values into further dimentions
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Insert(Vector q, double value, int position)
        {
            double[] values = new double[q.Dimensions + 1];

            for (int index = 0; index < q.Dimensions; index++)
                values[index + (index < position ? 0 : 1)] = q.Values[index];

            values[position] = value;

            return new Vector(values);
        }

        /// <summary>
        /// Adds value before first dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Prefix(Vector q, double value) => Insert(q, value, 0);

        /// <summary>
        /// Adds value after last dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Postfix(Vector q, double value) => Insert(q, value, q.Dimensions);

        /// <summary>
        /// Removes value from corresponding dimension shifting rest values into closer dimentions
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Remove(Vector q, int position)
        {
            double[] values = new double[q.Dimensions - 1];

            for (int index = 0; index < q.Dimensions -1; index++)
                values[index] = q.Values[index + (index < position ? 0 : 1)];

            return new Vector(values);
        }

        /// <summary>
        /// Removes first value
        /// </summary>
        [DebuggerStepThrough]
        public static Vector UnPrefix(Vector q) => Remove(q, 0);

        /// <summary>
        /// Removes last value
        /// </summary>
        [DebuggerStepThrough]
        public static Vector UnPostfix(Vector q) => Remove(q, q.Dimensions - 1);

        /// <summary>
        /// Changes order of values using provided map
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Swizzle(Vector q, int[] map)
        {
            if (map.Length != q.Dimensions)
                throw new DimensionalityMismatchException();

            return Map(q, (v, i) => q.Values[map[i]]);
        }

        /// <summary>
        /// Sets value in corresponding dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Set(Vector q, double value, int position)
        {
            double[] result = new double[q.Dimensions];
            Array.Copy(q.Values, result, q.Dimensions);
            result[position] = value;

            return new Vector(result);
        }
        #endregion

        [DebuggerStepThrough]
        private static void EnsureConsistentDimensionality(Vector q, Vector r)
        {
            if (q.Dimensions != r.Dimensions)
                throw new DimensionalityMismatchException();
        }

        [DebuggerStepThrough]
        private static void EnsureConsistentDimensionality(Vector q, Vector r, Vector s)
        {
            if (q.Dimensions != r.Dimensions || r.Dimensions != s.Dimensions)
                throw new DimensionalityMismatchException();
        }

        [DebuggerStepThrough]
        private static void EnsureConsistentDimensionality(params Vector[] vectors)
        {
            if (vectors == null || vectors.Length <= 1)
                return;

            int dimensions = vectors[0].Dimensions;

            foreach(Vector vector in vectors.Skip(1))
                if (dimensions != vector.Dimensions)
                    throw new DimensionalityMismatchException();
        }

        [DebuggerStepThrough]
        public override string ToString() => string.Format("[{0}]", string.Join(", ", Values.Select(v => v.ToString(NumberFormat))));

        [DebuggerStepThrough]
        public double[] ToArray()
        {
            if (Values == null)
                return new double[] { };

            double[] result = new double[Dimensions];
            Array.Copy(Values, result, Dimensions);
            return result;
        }

        [DebuggerStepThrough]
        public IEnumerable<double> ToEnumerable() => Values;
    }
}
