# AdventOfCode2021
My take on AoC 2021

I _could_ add lots of fancy pictures and what have you here. I won't.


## Benchmark results per day
### System
Here's the relevant system data according to BenchmarkDotNet:
```
BenchmarkDotNet=v0.13.1, OS=manjaro 
AMD FX(tm)-8300, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.205
  [Host]     : .NET 5.0.8 (5.0.821.37701), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.37701), X64 RyuJIT
```

### Day 1
|  Method |     Mean |   Error |  StdDev |
|-------- |---------:|--------:|--------:|
| partOne | 115.4 ns | 1.98 ns | 2.65 ns |
| partTwo | 243.5 ns | 1.76 ns | 1.56 ns |

### Day 2
|  Method |      Mean |    Error |   StdDev |
|-------- |----------:|---------:|---------:|
| partOne | 579.37 ns | 2.757 ns | 2.579 ns |
| partTwo |  11.07 ns | 0.051 ns | 0.048 ns |
