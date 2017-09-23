using System;
using System.Collections.Generic;
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
        public static Vector Create(int dimensions, double value)
        {
            return new Vector(Enumerable.Repeat(value, dimensions));
        }

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        public Vector(params double[] values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Dimensions = values.Count();
        }

        /// <summary>
        /// Creates new vector from values
        /// </summary>
        public Vector(IEnumerable<double> values) : this(values.ToArray()) { }


        /// <summary>
        /// Creates unit vector
        /// </summary>
        public static Vector Normalize(Vector q)
        {
            return q / q.Length;
        }

        /// <summary>
        /// Creates dot product of two vectors
        /// </summary>
        public static double Dot(Vector q, Vector r)
        {
            return Aggregate(q * r, (a, b) => a + b);
        }

        /// <summary>
        /// Creates cross product of vectors
        /// </summary>
        public static Vector Cross(params Vector[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            if (v.Any(vec => vec.Dimensions != v.Length + 1))
                throw new DimensionalityMismatchException();

            return Map(Create(v[0].Dimensions, 1), (value, index) => DetWeave(value, v, index));
        }

        /// <summary>
        /// Creates determinant of vectors
        /// </summary>
        public static double Det(params Vector[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            if (v.Any(vec => vec.Dimensions != v.Length))
                throw new DimensionalityMismatchException();

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
        public static bool CloseEnough(Vector q, Vector r, double distance)
        {
            return (r - q).LengthSquared <= distance * distance;
        }

        #region Operators
        public static Vector operator -(Vector q)
        {
            return q * -1;
        }

        public static Vector operator +(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        public static Vector operator -(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        public static Vector operator *(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        public static Vector operator /(Vector q, Vector r)
        {
            return Merge(q, r, (a, b) => a / b);
        }

        public static Vector operator +(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        public static Vector operator -(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        public static Vector operator *(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        public static Vector operator /(Vector q, double r)
        {
            return Merge(q, r, (a, b) => a / b);
        }

        public static Vector operator +(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a + b);
        }

        public static Vector operator -(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a - b);
        }

        public static Vector operator *(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a * b);
        }

        public static Vector operator /(double q, Vector r)
        {
            return Merge(q, r, (a, b) => a / b);
        }
        #endregion

        #region Operations
        /// <summary>
        /// Marges corresponding dimensions of two vectors using operation function
        /// </summary>
        public static Vector Merge(Vector q, Vector r, Func<double, double, double> operation)
        {
            if (q.Dimensions != r.Dimensions)
                throw new DimensionalityMismatchException();

            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r.Values[index]);

            return new Vector(values);
        }

        /// <summary>
        /// Marges corresponding dimensions of two vectors using operation function
        /// </summary>
        public static Vector Merge(Vector q, Vector r, Func<double, double, int, double> operation)
        {
            if (q.Dimensions != r.Dimensions)
                throw new DimensionalityMismatchException();

            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r.Values[index], index);

            return new Vector(values);
        }

        /// <summary>
        /// Marges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        public static Vector Merge(double q, Vector r, Func<double, double, double> operation)
        {
            double[] values = new double[r.Dimensions];

            for (int index = 0; index < r.Dimensions; index++)
                values[index] = operation(q, r.Values[index]);

            return new Vector(values);
        }

        /// <summary>
        /// Marges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        public static Vector Merge(double q, Vector r, Func<double, double, int, double> operation)
        {
            double[] values = new double[r.Dimensions];

            for (int index = 0; index < r.Dimensions; index++)
                values[index] = operation(q, r.Values[index], index);

            return new Vector(values);
        }

        /// <summary>
        /// Marges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
        public static Vector Merge(Vector q, double r, Func<double, double, double> operation)
        {
            double[] values = new double[q.Dimensions];

            for (int index = 0; index < q.Dimensions; index++)
                values[index] = operation(q.Values[index], r);

            return new Vector(values);
        }

        /// <summary>
        /// Marges corresponding dimensions of vector with scalar value using operation function
        /// </summary>
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
        public static double Aggregate(Vector q, Func<double, double, double> operation)
        {
            double result = 0;

            for (int index = 0; index < q.Dimensions; index++)
                result = operation(result, q.Values[index]);

            return result;
        }

        /// <summary>
        /// Aggregates value of every dimension using operation function
        /// </summary>
        public static double Aggregate(Vector q, Func<double, double, int, double> operation)
        {
            double result = 0;

            for (int index = 0; index < q.Dimensions; index++)
                result = operation(result, q.Values[index], index);

            return result;
        }

        /// <summary>
        /// Inserts value in corresponding dimension shifting rest values into further dimentions
        /// </summary>
        public static Vector Insert(Vector q, double value, int position)
        {
            double[] values = new double[q.Dimensions + 1];

            for (int index = 0; index < q.Dimensions; index++)
                if (index < position)
                    values[index] = q.Values[index];
                else
                    values[index + 1] = q.Values[index];

            values[position] = value;

            return new Vector(values);
        }

        /// <summary>
        /// Adds value before first dimension
        /// </summary>
        public static Vector Prefix(Vector q, double value)
        {
            return Insert(q, value, 0);
        }

        /// <summary>
        /// Adds value after last dimension
        /// </summary>
        public static Vector Postfix(Vector q, double value)
        {
            return Insert(q, value, q.Dimensions);
        }

        /// <summary>
        /// Removes value from corresponding dimension shifting rest values into closer dimentions
        /// </summary>
        public static Vector Remove(Vector q, int position)
        {
            double[] values = new double[q.Dimensions - 1];

            for (int index = 0; index < q.Dimensions -1; index++)
                if (index < position)
                    values[index] = q.Values[index];
                else
                    values[index] = q.Values[index + 1];

            return new Vector(values);
        }

        /// <summary>
        /// Removes first value
        /// </summary>
        public static Vector UnPrefix(Vector q)
        {
            return Remove(q, 0);
        }

        /// <summary>
        /// Removes last value
        /// </summary>
        public static Vector UnPostfix(Vector q)
        {
            return Remove(q, q.Dimensions -1);
        }

        /// <summary>
        /// Changes order of values using provided map
        /// </summary>
        public static Vector Swizzle(Vector q, int[] map)
        {
            if (map.Length != q.Dimensions)
                throw new DimensionalityMismatchException();

            return Map(q, (v, i) => q.Values[map[i]]);
        }

        /// <summary>
        /// Sets value in corresponding dimension
        /// </summary>
        public static Vector Set(Vector q, double value, int position)
        {
            double[] values = q.Values;
            values[position] = value;

            return new Vector(values);
        }
        #endregion

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", Values));
        }
    }
}
