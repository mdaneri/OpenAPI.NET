```

BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.7392)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 8.0.416
  [Host]   : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4
  ShortRun : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v4

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Mean       | Error       | StdDev     | Gen0   | Allocated |
|---------------------------- |-----------:|------------:|-----------:|-------:|----------:|
| EmptyApiCallback            |   7.371 ns |  28.0118 ns |  1.5354 ns | 0.0051 |      32 B |
| EmptyApiComponents          |  18.967 ns |  58.6069 ns |  3.2124 ns | 0.0166 |     104 B |
| EmptyApiContact             |  14.196 ns |  20.1853 ns |  1.1064 ns | 0.0076 |      48 B |
| EmptyApiDiscriminator       |   5.903 ns |   8.7000 ns |  0.4769 ns | 0.0064 |      40 B |
| EmptyDocument               | 836.098 ns | 802.7158 ns | 43.9996 ns | 0.1802 |    1136 B |
| EmptyApiEncoding            |   7.425 ns |  34.4068 ns |  1.8860 ns | 0.0089 |      56 B |
| EmptyApiExample             |   6.559 ns |   4.1211 ns |  0.2259 ns | 0.0089 |      56 B |
| EmptyApiExternalDocs        |   5.457 ns |  14.5229 ns |  0.7960 ns | 0.0064 |      40 B |
| EmptyApiHeader              |  10.649 ns |  14.6181 ns |  0.8013 ns | 0.0127 |      80 B |
| EmptyApiInfo                |   6.291 ns |  11.6495 ns |  0.6386 ns | 0.0127 |      80 B |
| EmptyApiLicense             |   6.278 ns |   4.1829 ns |  0.2293 ns | 0.0076 |      48 B |
| EmptyApiLink                |   7.769 ns |  31.9362 ns |  1.7505 ns | 0.0115 |      72 B |
| EmptyApiMediaType           |   5.878 ns |   7.1486 ns |  0.3918 ns | 0.0089 |      56 B |
| EmptyApiOAuthFlow           |   6.663 ns |   7.6945 ns |  0.4218 ns | 0.0089 |      56 B |
| EmptyApiOAuthFlows          |   6.494 ns |  16.3772 ns |  0.8977 ns | 0.0089 |      56 B |
| EmptyApiOperation           | 114.331 ns | 403.6944 ns | 22.1279 ns | 0.0598 |     376 B |
| EmptyApiParameter           |   6.079 ns |   9.9021 ns |  0.5428 ns | 0.0153 |      96 B |
| EmptyApiPathItem            |  10.268 ns | 126.6413 ns |  6.9416 ns | 0.0102 |      64 B |
| EmptyApiPaths               |  58.188 ns |  16.7261 ns |  0.9168 ns | 0.0395 |     248 B |
| EmptyApiRequestBody         |   4.265 ns |   6.8222 ns |  0.3739 ns | 0.0076 |      48 B |
| EmptyApiResponse            |   4.222 ns |   7.5421 ns |  0.4134 ns | 0.0089 |      56 B |
| EmptyApiResponses           |  68.434 ns | 201.3403 ns | 11.0361 ns | 0.0395 |     248 B |
| EmptyApiSchema              |  16.768 ns |  57.0069 ns |  3.1247 ns | 0.0650 |     408 B |
| EmptyApiSecurityRequirement |   9.231 ns |   2.9246 ns |  0.1603 ns | 0.0166 |     104 B |
| EmptyApiSecurityScheme      |   5.647 ns |  13.6518 ns |  0.7483 ns | 0.0140 |      88 B |
| EmptyApiServer              |   3.881 ns |   1.9645 ns |  0.1077 ns | 0.0076 |      48 B |
| EmptyApiServerVariable      |   3.678 ns |   0.6074 ns |  0.0333 ns | 0.0076 |      48 B |
| EmptyApiTag                 |   4.001 ns |   1.8709 ns |  0.1026 ns | 0.0076 |      48 B |
