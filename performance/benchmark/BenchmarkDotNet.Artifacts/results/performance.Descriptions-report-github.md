```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7392)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.416
  [Host]   : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4
  ShortRun : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method       | Mean           | Error        | StdDev       | Gen0       | Gen1       | Gen2      | Allocated    |
|------------- |---------------:|-------------:|-------------:|-----------:|-----------:|----------:|-------------:|
| PetStoreYaml |       605.8 μs |   1,596.8 μs |     87.52 μs |    58.5938 |    11.7188 |         - |     369.4 KB |
| PetStoreJson |       231.2 μs |     271.1 μs |     14.86 μs |    37.1094 |     7.8125 |         - |    231.55 KB |
| GHESYaml     | 1,182,382.8 μs | 803,469.7 μs | 44,040.88 μs | 60000.0000 | 23000.0000 | 4000.0000 | 345029.91 KB |
| GHESJson     |   515,858.5 μs | 365,567.9 μs | 20,038.01 μs | 33000.0000 | 12000.0000 | 2000.0000 | 206550.25 KB |
