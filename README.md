# V 
Yet another, reinvented, multidimensional Vector library designed for work with changing dimensionality.

### Requirements
* Microsoft .NET Standard 2.1
* Microsoft .NET 6.0

### Projects
* V - Main functionality
* V.Samples - Usage samples
* V.UnitTests - Unit tests
* V.Benchmarks - Benchmarks

### Setup
* Download repository
* Open V.sln in Visual Studio (i'm using 2019 Community)
* Build project V (check project properties for output path, most likely ./bin/Debug/)
* V.dll is now ready to be added as reference in your projects

### Future / TODO
* Function RotateAroundAxis should work in all dimensions (currently only in 3)

---

### Samples
Here are some usage examples that might be helpful. Code used for samples and images is available in V.Samples project.

#### Vector arithmetic operators

##### Addition
```csharp
Vector add1 = new Vector(1, 2) + new Vector(2, 1);
// add1 is now [3, 3]

Vector add2 = new Vector(1, 2) + 2;
// add2 is now [3, 4]

Vector add3 = 2 + new Vector(2, 1);
// add3 is now [4, 3]
```

Visualization of two 3 dimensional vectors (in green) resulting in third vector as a sum (in blue)
![sum_chart](./V.Samples/img/sum.png "3 dimensional isometric chart with 3 vectors represented as a lines")

##### Subtraction
```csharp
Vector sub1 = new Vector(1, 2) - new Vector(2, 1);
// sub1 is now [1, 1]

Vector sub2 = new Vector(1, 2) - 2;
// sub2 is now [-1, 0]

Vector sub3 = 2 - new Vector(2, 1);
// sub3 is now [0, 1]
```

Visualization of two 3 dimensional vectors (in green) resulting in third vector as a diffrence (in blue)
![sub_chart](./V.Samples/img/sub.png "3 dimensional isometric chart with 3 vectors represented as a lines")

##### Multiplication
```csharp
Vector mul1 = new Vector(1, 2) * new Vector(2, 1);
// mul1 is now [2, 2]

Vector mul2 = new Vector(1, 2) * 2;
// mul2 is now [2, 4]

Vector mul3 = 2 * new Vector(2, 1);
// mul3 is now [4, 2]
```

##### Division
```csharp
Vector div1 = new Vector(1, 2) / new Vector(2, 1);
// div1 is now [0.5, 2]

Vector div2 = new Vector(1, 2) / 2;
// div2 is now [0.5, 1]

Vector div3 = 2 / new Vector(2, 1);
// div 3 is now [1, 2]
```

##### Inversion
```csharp
Vector inv = -new Vector(1, -2, 3);
// inv is now [-1, 2, -3]
```

#### Functions

##### Min / Max
Produces vector with minimal/maximal values in every dimension.
```csharp
Vector min1 = Vector.Min(new Vector(-1, 1, -1), new Vector(1, -1, 1));
// min1 is now [-1, -1, -1]

Vector min2 = Vector.Min(new Vector(1, 2, 3), new Vector(1, -1, 1), new Vector(5, 1, -1));
// min2 is now [1, -1, -1]

Vector max = Vector.Max(new Vector(-1, 1, -1), new Vector(1, -1, 1));
// max is now [1, 1, 1]
```

##### Normalize
Produces unit vector.
```csharp
Vector nor = Vector.Normalize(new Vector(12, 0, 0));
// nor is now [1, 0, 0]
```

##### Dot
Produces dot product of two vectors.
```csharp
double dot = Vector.Dot(new Vector(1, 0), new Vector(0.5, 0.5));
// dot is now 0.5
```

##### AngleDifference
Produces angle difference between two vectors (in radians).
```csharp
double diff = Vector.AngleDifference(new Vector(0, 1), Vector.Create(2, 0), new Vector(1, 0));
// diff is now half PI
```

##### RotateAroundAxis
Rotates vector around specified axis by angle (in radians).
```csharp
Vector raa = Vector.RotateAroundAxis(new Vector(1, 0, 0), new Vector(0, 1, 0), Math.PI / 2d);
// raa is now [0, 0, 1]
```

Visualization of 3 dimensional vector (in green) being rotated 180° around axis (in red) resulting in third vector (in blue)
![raa_chart](./V.Samples/img/raa.png "3 dimensional isometric chart with 3 vectors represented as a lines")

##### CloseEnough
Checks proximity of two vectors with specified tolerance.
```csharp
bool closeEnough = Vector.CloseEnough(new Vector(0, 1, -0.1), new Vector(0, 1, 0.1), 0.5);
// closeEnough is True

bool notCloseEnough = Vector.CloseEnough(new Vector(0, 1, -0.1), new Vector(0, 1, 0.1), 0.01);
// notCloseEnough is False
```

##### Lerp
Produces interpolation of two vectors.
```csharp
Vector inter = Vector.Lerp(new Vector(-0.8, 0.3, -0.5), new Vector(0.4, 0.5, 0.3), 0.5);
// inter is now [-0.2, 0.4, -0.1]
```

Visualization of two 3 dimensional vectors (in green) resulting in third vector (in blue)
![lerpIn_chart](./V.Samples/img/lerpIn.png "3 dimensional isometric chart with 3 vectors represented as a lines")

Lerp can also be used for extrapolation.
```csharp
Vector extra = Vector.Lerp(new Vector(-0.8, 0.3, -0.5), new Vector(0.4, 0.5, 0.3), 1.2);
// extra is now [0.64, 0.54, 0.46]
```

Visualization of two 3 dimensional vectors (in green) resulting in third vector (in blue)
![lerpEx_chart](./V.Samples/img/lerpEx.png "3 dimensional isometric chart with 3 vectors represented as a lines")

##### Cross
Produces vector that is perpendicular to all provided vectors.

There is specific amount of vectors that is required for this function to work.
```csharp
Vector cross2d = Vector.Cross(new Vector(1, 0));
// cross2d is now [0, -1]

Vector cross3d = Vector.Cross(new Vector(1, 0, 0), new Vector(0, 1, 0));
// cross3d is now [0, 0, 1]

Vector cross4d = Vector.Cross(new Vector(1, 0, 0, 0), new Vector(0, 1, 0, 0), new Vector(0, 0, 1, 0));
// cross4d is now [0, 0, 0, -1]
```

Visualization of two 3 dimensional vectors (in green) resulting in third vector as a cross product (in blue)
![cross_chart](./V.Samples/img/cross.png "3 dimensional isometric chart with 3 vectors represented as a lines")
