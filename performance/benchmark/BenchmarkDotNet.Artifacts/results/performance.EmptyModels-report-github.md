```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK 8.0.418
  [Host]   : .NET 8.0.24 (8.0.24, 8.0.2426.7010), Arm64 RyuJIT armv8.0-a
  ShortRun : .NET 8.0.24 (8.0.24, 8.0.2426.7010), Arm64 RyuJIT armv8.0-a

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method                      | Mean       | Error       | StdDev     | Gen0   | Allocated |
|---------------------------- |-----------:|------------:|-----------:|-------:|----------:|
| EmptyApiCallback            |   2.800 ns |   0.6839 ns |  0.0375 ns | 0.0051 |      32 B |
| EmptyApiComponents          |   5.079 ns |   2.1198 ns |  0.1162 ns | 0.0179 |     112 B |
| EmptyApiContact             |   3.303 ns |   0.4142 ns |  0.0227 ns | 0.0076 |      48 B |
| EmptyApiDiscriminator       |   3.111 ns |   2.0276 ns |  0.1111 ns | 0.0076 |      48 B |
| EmptyDocument               | 571.763 ns | 351.5283 ns | 19.2685 ns | 0.1822 |    1144 B |
| EmptyApiEncoding            |   4.184 ns |   1.8539 ns |  0.1016 ns | 0.0127 |      80 B |
| EmptyApiExample             |   4.012 ns |   0.8669 ns |  0.0475 ns | 0.0115 |      72 B |
| EmptyApiExternalDocs        |   2.915 ns |   1.9656 ns |  0.1077 ns | 0.0064 |      40 B |
| EmptyApiHeader              |   4.264 ns |   3.8855 ns |  0.2130 ns | 0.0127 |      80 B |
| EmptyApiInfo                |   4.167 ns |   2.2812 ns |  0.1250 ns | 0.0127 |      80 B |
| EmptyApiLicense             |   3.300 ns |   2.7376 ns |  0.1501 ns | 0.0076 |      48 B |
| EmptyApiLink                |   3.953 ns |   1.1344 ns |  0.0622 ns | 0.0115 |      72 B |
| EmptyApiMediaType           |   4.199 ns |   2.5379 ns |  0.1391 ns | 0.0127 |      80 B |
| EmptyApiOAuthFlow           |   3.684 ns |   3.2829 ns |  0.1799 ns | 0.0102 |      64 B |
| EmptyApiOAuthFlows          |   3.777 ns |   2.8912 ns |  0.1585 ns | 0.0102 |      64 B |
| EmptyApiOperation           |  58.434 ns |   3.3328 ns |  0.1827 ns | 0.0598 |     376 B |
| EmptyApiParameter           |   4.681 ns |   0.0819 ns |  0.0045 ns | 0.0153 |      96 B |
| EmptyApiPathItem            |   3.655 ns |   1.1731 ns |  0.0643 ns | 0.0102 |      64 B |
| EmptyApiPaths               |  50.057 ns |   2.2093 ns |  0.1211 ns | 0.0395 |     248 B |
| EmptyApiRequestBody         |   3.183 ns |   1.6345 ns |  0.0896 ns | 0.0076 |      48 B |
| EmptyApiResponse            |   3.741 ns |   1.7796 ns |  0.0975 ns | 0.0102 |      64 B |
| EmptyApiResponses           |  49.688 ns |   2.4097 ns |  0.1321 ns | 0.0395 |     248 B |
| EmptyApiSchema              |  15.709 ns |   2.6679 ns |  0.1462 ns | 0.0663 |     416 B |
| EmptyApiSecurityRequirement |   8.815 ns |   0.9859 ns |  0.0540 ns | 0.0166 |     104 B |
| EmptyApiSecurityScheme      |   4.825 ns |   2.7843 ns |  0.1526 ns | 0.0166 |     104 B |
| EmptyApiServer              |   3.497 ns |   3.0736 ns |  0.1685 ns | 0.0089 |      56 B |
| EmptyApiServerVariable      |   3.350 ns |   0.3557 ns |  0.0195 ns | 0.0076 |      48 B |
| EmptyApiTag                 |   3.966 ns |   0.2400 ns |  0.0132 ns | 0.0115 |      72 B |
