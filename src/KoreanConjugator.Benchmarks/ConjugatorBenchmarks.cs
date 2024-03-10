using BenchmarkDotNet.Attributes;

namespace KoreanConjugator.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 100)]
public class ConjugatorBenchmarks
{
    private static readonly int Iterations = 100;
    private readonly Conjugator _conjugator = new (new SuffixTemplateParser());
    private readonly string _stem = "기다리";
    private ConjugationParams _conjugationParams = new()
    {
        ClauseType = ClauseType.Declarative,
        Formality = Formality.InformalLow,
        Honorific = false,
        Tense = Tense.Present,
        WordClass = WordClass.Verb,
    };

    [Benchmark]
    public ConjugationResult[] Conjugate()
    {
        var results = new ConjugationResult[Iterations];
        for (int i = 0; i < Iterations; i++)
        {
            results[i] = _conjugator.Conjugate(_stem, _conjugationParams);
        }
        return results;
    }
}
