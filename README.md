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

### Day 3
|  Method |      Mean |    Error |    StdDev |
|-------- |----------:|---------:|----------:|
| PartOne | 423.78 ns | 8.385 ns | 11.193 ns |
| PartTwo |  27.01 ns | 0.383 ns |  0.358 ns |

### Day 4
|  Method |     Mean |   Error |  StdDev |
|-------- |---------:|--------:|--------:|
| PartOne | 121.9 ns | 1.98 ns | 1.85 ns |
| PartTwo | 129.9 ns | 1.53 ns | 1.28 ns |

### Day 5
|  Method |     Mean |   Error |  StdDev |
|-------- |---------:|--------:|--------:|
| PartOne | 197.9 ns | 0.54 ns | 0.42 ns |
| PartTwo | 305.4 ns | 1.63 ns | 1.45 ns |

### Day 6
|  Method |     Mean |     Error |    StdDev |
|-------- |---------:|----------:|----------:|
| PartOne | 4.354 us | 0.0073 us | 0.0069 us |
| PartTwo | 8.910 us | 0.0111 us | 0.0104 us |

### Day 7
|  Method |        Mean |     Error |    StdDev |
|-------- |------------:|----------:|----------:|
| PartOne | 329.0888 ns | 6.4746 ns | 8.8625 ns |
| PartTwo |   0.6119 ns | 0.0107 ns | 0.0095 ns |

### Day 8
|  Method |         Mean |     Error |    StdDev |
|-------- |-------------:|----------:|----------:|
| PartOne | 1,023.181 ns | 5.5490 ns | 4.3323 ns |
| PartTwo |     4.755 ns | 0.0392 ns | 0.0348 ns |

### Day 9
|  Method |     Mean |   Error |  StdDev |
|-------- |---------:|--------:|--------:|
| PartOne | 293.9 ns | 2.21 ns | 2.07 ns |
| PartTwo | 210.6 ns | 2.24 ns | 1.87 ns |

### Day 10
|  Method |     Mean |   Error |  StdDev |
|-------- |---------:|--------:|--------:|
| PartOne | 793.1 ns | 5.20 ns | 4.61 ns |
| PartTwo | 266.8 ns | 1.76 ns | 1.38 ns |

### Day 11
|  Method |      Mean |     Error |    StdDev |
|-------- |----------:|----------:|----------:|
| PartOne | 52.727 us | 0.7806 us | 0.6919 us |
| PartTwo |  1.308 us | 0.0039 us | 0.0036 us |
