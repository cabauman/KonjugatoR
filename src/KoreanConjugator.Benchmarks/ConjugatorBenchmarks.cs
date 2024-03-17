using BenchmarkDotNet.Attributes;

namespace KoreanConjugator.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 10)]
public class ConjugatorBenchmarks
{
    private static readonly int Iterations = 100;
    private readonly Conjugator _conjugator = new(new SuffixTemplateParser());
    private readonly string _stem = "기다리";
    private readonly ConjugationResult[] _results = new ConjugationResult[Iterations];
    private readonly string[] _results2 = new string[Iterations];
    private ConjugationParams _conjugationParams = new()
    {
        ClauseType = ClauseType.Declarative,
        Formality = Formality.InformalLow,
        Honorific = false,
        Tense = Tense.Present,
        WordClass = WordClass.Verb,
    };

    //[Benchmark]
    public ConjugationResult[] Conjugate()
    {
        for (int i = 0; i < Iterations; i++)
        {
            _results[i] = _conjugator.Conjugate(_stem, _conjugationParams);
        }
        return _results;
    }

    [Benchmark]
    public string[] MemoryTest()
    {
        for (int i = 0; i < Iterations; i++)
        {
            _results2[i] = _conjugator.MemoryTest(_stem, _conjugationParams);
        }
        return _results2;
    }
}
