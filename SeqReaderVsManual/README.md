Results

Arm64 (M1 Max)

``` ini

BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.2.1 (22D68) [Darwin 22.3.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.200
  [Host]     : .NET 7.0.3 (7.0.323.6910), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.3 (7.0.323.6910), Arm64 RyuJIT AdvSIMD


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|      SeqReader | 7.4750 ns | 0.1726 ns | 0.1847 ns |  1.00 |    0.00 |
|      ROSeqRead | 4.3804 ns | 0.0879 ns | 0.0822 ns |  0.59 |    0.02 |
|     MemoryRead | 1.4150 ns | 0.0449 ns | 0.0420 ns |  0.19 |    0.01 |
|      ArrayRead | 0.1220 ns | 0.0013 ns | 0.0012 ns |  0.02 |    0.00 |
|  SeqReader1000 | 5.4043 ns | 0.0034 ns | 0.0028 ns |  0.73 |    0.02 |
|  ROSeqRead1000 | 3.4312 ns | 0.0028 ns | 0.0023 ns |  0.46 |    0.01 |
| MemoryRead1000 | 1.2912 ns | 0.0006 ns | 0.0006 ns |  0.17 |    0.00 |
|  ArrayRead1000 | 0.6300 ns | 0.0010 ns | 0.0009 ns |  0.08 |    0.00 |

x64 (Intel Xeon)

``` ini

BenchmarkDotNet=v0.13.5, OS=ubuntu 22.04
Intel Xeon W-2133 CPU 3.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=8.0.100-alpha.1.23061.8
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |       Mean |     Error |    StdDev | Ratio |
|--------------- |-----------:|----------:|----------:|------:|
|      SeqReader | 12.0009 ns | 0.0818 ns | 0.0683 ns |  1.00 |
|      ROSeqRead |  6.3215 ns | 0.0182 ns | 0.0152 ns |  0.53 |
|     MemoryRead |  2.3254 ns | 0.0084 ns | 0.0070 ns |  0.19 |
|      ArrayRead |  0.3567 ns | 0.0053 ns | 0.0047 ns |  0.03 |
|  SeqReader1000 |  9.3821 ns | 0.0398 ns | 0.0372 ns |  0.78 |
|  ROSeqRead1000 |  5.4784 ns | 0.0131 ns | 0.0123 ns |  0.46 |
| MemoryRead1000 |  2.3389 ns | 0.0062 ns | 0.0055 ns |  0.19 |
|  ArrayRead1000 |  1.8334 ns | 0.0037 ns | 0.0034 ns |  0.15 |
