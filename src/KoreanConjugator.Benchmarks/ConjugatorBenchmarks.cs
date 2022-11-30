using BenchmarkDotNet.Attributes;
using System.Text;

namespace KoreanConjugator.Benchmarks;

[MemoryDiagnoser]
//[SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 5)]
public class ConjugatorBenchmarks
{
    private static readonly int Iterations = 1;
    private readonly Conjugator _conjugator = new (new SuffixTemplateParser());
    private static readonly string _stem = "기다리";
    private readonly ConjugationResult[] _results = new ConjugationResult[Iterations];
    private readonly ConjugationSuffixTemplateListProvider templateListProvider = new();
    private readonly bool IsIrr = HangulUtil.IsIrregular("abc");
    private ConjugationParams _conjugationParams = new()
    {
        ClauseType = ClauseType.Declarative,
        Formality = Formality.InformalLow,
        Honorific = false,
        Tense = Tense.Present,
        WordClass = WordClass.Verb,
    };

    //[Benchmark]
    //public ConjugationResult[] Conjugate()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        _results[i] = _conjugator.Conjugate(_stem, _conjugationParams);
    //    }
    //    return _results;
    //}

    //[Benchmark]
    //public ConjugationResult[] Conjugate2()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        _conjugator.Conjugate2(_stem, _conjugationParams);
    //    }
    //    return _results;
    //}

    [Benchmark]
    public ConjugationResult[] GetSuffix()
    {
        for (int i = 0; i < Iterations; i++)
        {
            //_conjugator.GetSuffix(_stem, "(아/어)요");
            var template = SuffixTemplateParser2.ParseAEuTemplate("(아/어)요");
            var result = template.ChooseSuffixVariant("가");
        }
        return _results;
    }

    //[Benchmark(Baseline = true)]
    //public ConjugationResult[] Substring()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        _results[i] = new ConjugationResult(_stem.Replace('리', '려'), Array.Empty<string>());
    //    }
    //    return _results;
    //}

    //[Benchmark]
    //public ConjugationResult[] SuffixTemplateParser()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        Conjugator.ApplyIrregularVerbRules(_stem, '어');
    //    }
    //    return _results;
    //}

    //[Benchmark]
    //public ConjugationResult[] SuffixTemplateParser()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        _conjugator.ApplyVerbStemEdgeCaseLogic(_stem, "(아/어)");
    //    }
    //    return _results;
    //}

    //private readonly string[] suffixes = new string[] { "어" };
    //private readonly MutableVerbStem mutableVerbStem = new(new StringBuilder(_stem), false);
    //[Benchmark]
    //public ConjugationResult[] SuffixTemplateParser()
    //{
    //    for (int i = 0; i < Iterations; i++)
    //    {
    //        Conjugator.MergeSyllablesFromLeftToRight(mutableVerbStem, suffixes);
    //    }
    //    return _results;
    //}

    //[Benchmark]
    //public ConjugationResult[] LoadData()
    //{
    //    HangulUtil.LoadData();
    //    return _results;
    //}
}
