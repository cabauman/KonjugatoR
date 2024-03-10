// See https://aka.ms/new-console-template for more information
using KoreanConjugator;

Console.WriteLine("Hello, World!");

ConjugationParams conjugationParams = new()
{
    ClauseType = ClauseType.Declarative,
    Formality = Formality.InformalLow,
    Honorific = false,
    Tense = Tense.Present,
    WordClass = WordClass.Verb,
};

Conjugator conjugator = new(new SuffixTemplateParser());
string stem = "기다리";
var result = conjugator.MemoryTest(stem, conjugationParams);
//Console.WriteLine(result.Value);