using System;
using System.Buffers;
using System.Buffers.Binary;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

[module: SkipLocalsInit]

namespace SeqReaderVsManual;

public class Benchmarks
{
    public int Position = 100;
    public ReadOnlySequence<byte> ROSeq { get; set; }
    public Memory<byte> FirstSegment { get; set; }
    public byte[] FirstSegmentArray { get; set; }

    [Benchmark(Baseline = true)]
    public int SeqReader()
    {
        var reader = new SequenceReader<byte>(ROSeq);
        reader.Advance(Position);
        reader.TryReadLittleEndian(out int value);
        return value;
    }

    [Benchmark]
    public int ROSeqRead()
    {
        var data = ROSeq.FirstSpan.Slice(Position);
        BinaryPrimitives.TryReadInt32LittleEndian(data, out var value);
        return value;
    }

    [Benchmark]
    public int MemoryRead()
    {
        var data = FirstSegment.Span.Slice(Position);
        BinaryPrimitives.TryReadInt32LittleEndian(data, out var value);
        return value;
    }

    [Benchmark]
    public int ArrayRead()
    {
        var data = FirstSegmentArray.AsSpan(Position);
        BinaryPrimitives.TryReadInt32LittleEndian(data, out var value);
        return value;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public int SeqReader1000()
    {
        var value = 0;
        for (var i = 0; i < 1000; i++)
        {
            var reader = new SequenceReader<byte>(ROSeq);
            reader.Advance(Position);
            reader.TryReadLittleEndian(out value);
        }

        return value;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public int ROSeqRead1000()
    {
        var value = 0;
        for (var i = 0; i < 1000; i++)
        {
            var data = ROSeq.FirstSpan.Slice(Position);
            BinaryPrimitives.TryReadInt32LittleEndian(data, out value);
        }

        return value;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public int MemoryRead1000()
    {
        var value = 0;
        for (var i = 0; i < 1000; i++)
        {
            var data = FirstSegment.Span.Slice(Position);
            BinaryPrimitives.TryReadInt32LittleEndian(data, out value);
        }

        return value;
    }

    [Benchmark(OperationsPerInvoke = 1000)]
    public int ArrayRead1000()
    {
        var value = 0;
        for (var i = 0; i < 1000; i++)
        {
            var data = FirstSegmentArray.AsSpan(Position);
            BinaryPrimitives.TryReadInt32LittleEndian(data, out value);
        }

        return value;
    }

    [GlobalSetup(Targets = new[]
    {
        nameof(SeqReader), nameof(ROSeqRead), nameof(MemoryRead), nameof(ArrayRead),
        nameof(SeqReader1000), nameof(ROSeqRead1000), nameof(MemoryRead1000), nameof(ArrayRead1000)
    })]
    public async Task Setup()
    {
        const int segmentSize = 8192;
        var data = new byte[segmentSize];
        Array.Fill<byte>(data, 1);
        // Benchmarking showed the difference between 1 or 2+ segments to be insignificant here.
        var pipe = new Pipe(new PipeOptions(minimumSegmentSize: segmentSize / 2));
        pipe.Writer.Write(data);
        await pipe.Writer.FlushAsync();
        var result = await pipe.Reader.ReadAsync();
        ROSeq = result.Buffer;
        MemoryMarshal.TryGetArray(result.Buffer.First, out var segment);
        FirstSegment = segment;
        FirstSegmentArray = segment.Array;
    }
}
