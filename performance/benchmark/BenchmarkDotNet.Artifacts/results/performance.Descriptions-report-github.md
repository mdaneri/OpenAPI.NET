```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.418
  [Host]   : .NET 8.0.24 (8.0.24, 8.0.2426.7010), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 8.0.24 (8.0.24, 8.0.2426.7010), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method       | Mean         | Error         | StdDev       | Gen0       | Gen1       | Gen2      | Allocated    |
|------------- |-------------:|--------------:|-------------:|-----------:|-----------:|----------:|-------------:|
| PetStoreYaml |     304.7 μs |     210.37 μs |     11.53 μs |    58.5938 |    11.7188 |         - |    361.39 KB |
| PetStoreJson |     130.7 μs |      23.22 μs |      1.27 μs |    36.1328 |     7.8125 |         - |    223.25 KB |
| GHESYaml     | 768,374.9 μs | 320,733.26 μs | 17,580.47 μs | 63000.0000 | 21000.0000 | 8000.0000 | 345351.09 KB |
| GHESJson     | 363,766.2 μs | 110,071.32 μs |  6,033.38 μs | 36000.0000 | 14000.0000 | 5000.0000 | 223286.45 KB |
