# V
Yet another, reinvented, multidimensional Vector library designed for work with changing dimensionality.

### Requirements
* Microsoft .NET Framework 4.6.1 (Most likely works on older versions, potential TODO)

### Dependencies
* NUnit 3.9.0.0 (for VTest project)
* NUnit Test Adapter 3.9.0.0 (extension for test execution in Visual Studio used only in VTest project)

### Projects
* V - Main functionality
* VExp - Usage samples
* VTest - Tests

### Setup
* Download repository
* Open V.sln in Visual Studio (i'm using 2017 Community version but 2013 and 2015 should work too)
* Build project V (check project properties for output path, most likely ./bin/Debug/)
* V.dll is now ready to be added as reference in your projects

### Future
* Documentation
* In-depth samples and usage examples

### Samples
Here are some usage examples that might be helpful

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
[[https://github.com/CreoOne/V/blob/master/VExp/img/sum.png|alt=addition_visualization]]

