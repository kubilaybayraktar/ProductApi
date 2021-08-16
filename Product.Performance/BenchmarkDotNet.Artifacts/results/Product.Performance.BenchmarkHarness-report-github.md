``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1165 (21H1/May2021Update)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.101
  [Host]     : .NET 5.0.4 (5.0.421.11614), X64 RyuJIT
  DefaultJob : .NET 5.0.4 (5.0.421.11614), X64 RyuJIT


```
|             Method |      Mean |    Error |   StdDev |    Median |
|------------------- |----------:|---------:|---------:|----------:|
|        GetProducts | 185.35 μs | 3.691 μs | 8.914 μs | 183.36 μs |
| GetProductsByCache |  16.81 μs | 2.578 μs | 7.562 μs |  14.30 μs |
