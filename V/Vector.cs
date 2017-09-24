using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V
{
    public struct Vector
    {
        /// <summary>
        /// Dimensions of vector
        /// </summary>
        public int Dimensions { get; private set; }

        /// <summary>
        /// Concrete values in corresponding dimensions
        /// </summary>
        public double[] Values { get; private set; }

        public double LengthSquared { get { return Aggregate(this, (r, q) => r + q * q); } }
        public double Length { get { return Math.Sqrt(LengthSquared); } }

        public double this[int index]
        {
            get { return Values[index]; }
        }

        /// <summary>
        /// Creates new vector with all dimensions filled with value
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Create(int dimensions, double value)
        {
            return new Vector(Enumerable.Repeat(value, dimensions));
        }

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        [DebuggerStepThrough]
        public Vector(params double[] values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));

            if (values.Length == 0)
                throw new ArgumentException();

            Dimensions = values.Count();
        }

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        [DebuggerStepThrough]
        public Vector(IEnumerable<double> values) : this(values.ToArray()) { }


        /// <summary>
        /// Creates unit vector
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Normalize(Vector q)
        {
            return q / q.Length;
        }

        /// <summary>
        /// Creates dot product of two vectors
        /// </summary>
        [DebuggerStepThrough]
        public static double Dot(Vector q, Vector r)
        {
            return Aggregate(q * r, (a, b) => a + b);
        }

        /// <summary>
        /// Creates cross product of vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Cross(params Vector[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            if (v.Length == 0)
                throw new InvalidOperationException();

            EnsureConsistentDimensionality(v);

            if (v.Any(vec => vec.Dimensions != v.Length + 1))
                throw new ArgumentException();

            return Map(Create(v[0].Dimensions, 1), (value, index) => DetWeave(value, v, index));
        }

        /// <summary>
        /// Creates determinant of vectors
        /// </summary>
        [DebuggerStepThrough]
        public static double Det(params Vector[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            if (v.Length == 0)
                throw new InvalidOperationException();

            EnsureConsistentDimensionality(v);

            if (v[0].Dimensions != v.Length)
                throw new ArgumentException();

            if (v[0].Dimensions == 1)
                return v[0].Values[0];

            return Aggregate(v[0], (result, value, index) => result + DetWeave(value, v.Skip(1), index));
        }

        private static double DetWeave(double value, IEnumerable<Vector> v, int index)
        {
            return (index % 2 == 0 ? 1 : -1) * (value * Det(v.Select(d => Remove(d, index)).ToArray()));
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
        /// Angle difference between two vectors
        /// </summary>
        [DebuggerStepThrough]
        public static double AngleDifference(Vector q, Vector o, Vector r)
        {
            EnsureConsistentDimensionality(q, o, r);

            return Math.Acos(Dot(Normalize(q - o), Normalize(r - o)));
        }

        /// <summary>
        /// Rotate vector around axis
        /// </summary>
        [DebuggerStepThrough]
        public static Vector RotateAroundAxis(Vector v, Vector axis, double theta)
        {
            EnsureConsistentDimensionality(v, axis);

            double CosTheta = Math.Cos(-theta);
            axis = Normalize(axis);

            // v * cos(theta) + cross(k, v) * sin(theta) + k * dot(k, v) * (1 - cos(theta));
            return v * CosTheta + Cross(axis, v) * Math.Sin(-theta) + axis * Dot(axis, v) * (1.0f - CosTheta);
        }

        /// <summary>
        /// Min value vector from vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Min(params Vector[] v)
        {
            EnsureConsistentDimensionality(v);

            Vector[] swapped = Swap(v);
            double[] values = new double[v[0].Dimensions];

            for (int index = 0; index < swapped.Length; index++)
                values[index] = Aggregate(swapped[index], (result, q) => Math.Min(result, q), double.MaxValue);

            return new Vector(values);
        }

        /// <summary>
        /// Max value vector from vectors
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Max(params Vector[] v)
        {
            EnsureConsistentDimensionality(v);

            Vector[] swapped = Swap(v);
            double[] values = new double[v[0].Dimensions];

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
        public static Vector operator -(Vector q)
        {
            return q * -1;
        }

        [DebuggerStepThrough]
        public static Vector operator +(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        [DebuggerStepThrough]
        public static Vector operator -(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        [DebuggerStepThrough]
        public static Vector operator *(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        [DebuggerStepThrough]
        public static Vector operator /(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a / b);
        }

        [DebuggerStepThrough]
        public static Vector operator +(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        [DebuggerStepThrough]
        public static Vector operator -(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        [DebuggerStepThrough]
        public static Vector operator *(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        [DebuggerStepThrough]
        public static Vector operator /(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a / b);
        }

        [DebuggerStepThrough]
        public static Vector operator +(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        [DebuggerStepThrough]
        public static Vector operator -(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        [DebuggerStepThrough]
        public static Vector operator *(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        [DebuggerStepThrough]
        public static Vector operator /(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a / b);
        }
        #endregion

        #region Operations
        /// <summary>
        /// Swaps
        /// </summary>
        [DebuggerStepThrough]
        public static Vector[] Swap(params Vector[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            if (v.Length == 0)
                throw new ArgumentException();

            EnsureConsistentDimensionality(v);

            Vector[] result = new Vector[v[0].Dimensions];

            for (int dimension = 0; dimension < v[0].Dimensions; dimension++)
            {
                double[] values = new double[v.Length];

                for (int index = 0; index < v.Length; index++)
                    values[index] = v[index].Values[dimension];

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
        public static double Aggregate(Vector q, Func<double, double, double> operation, double entry = default(double))
        {
            for (int index = 0; index < q.Dimensions; index++)
                entry = operation(entry, q.Values[index]);

            return entry;
        }

        /// <summary>
        /// Aggregates value of every dimension using operation function
        /// </summary>
        [DebuggerStepThrough]
        public static double Aggregate(Vector q, Func<double, double, int, double> operation, double entry = default(double))
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
        public static Vector Prefix(Vector q, double value)
        {
            return Insert(q, value, 0);
        }

        /// <summary>
        /// Adds value after last dimension
        /// </summary>
        [DebuggerStepThrough]
        public static Vector Postfix(Vector q, double value)
        {
            return Insert(q, value, q.Dimensions);
        }

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
        public static Vector UnPrefix(Vector q)
        {
            return Remove(q, 0);
        }

        /// <summary>
        /// Removes last value
        /// </summary>
        [DebuggerStepThrough]
        public static Vector UnPostfix(Vector q)
        {
            return Remove(q, q.Dimensions -1);
        }

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
            double[] values = q.Values;
            values[position] = value;

            return new Vector(values);
        }
        #endregion

        private static void EnsureConsistentDimensionality(params Vector[] vectors)
        {
            if (vectors == null || vectors.Length <= 1)
                return;

            int dimensions = vectors[0].Dimensions;

            foreach(Vector vector in vectors)
                if (dimensions != vector.Dimensions)
                    throw new DimensionalityMismatchException();
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", Values));
        }
    }
}
