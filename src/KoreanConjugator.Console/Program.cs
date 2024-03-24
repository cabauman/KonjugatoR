// See https://aka.ms/new-console-template for more information
using KoreanConjugator;
using System.Text;

Console.WriteLine("Hello, World!");

ConjugationParams conjugationParams = new()
{
    ClauseType = ClauseType.Declarative,
    Formality = Formality.FormalHigh,
    Honorific = true,
    Tense = Tense.Present,
    WordClass = WordClass.Verb,
};
Console.OutputEncoding = System.Text.Encoding.UTF8;
Conjugator conjugator = new(new SuffixTemplateParser());
string stem = "기다리";
var result = conjugator.Conjugate(stem, conjugationParams);
Console.WriteLine(result.Value);
stem = "마"; // "기다리";
var sb = new StringBuilder(6);
sb.Append('마');
sb.Append('가');
sb.Append('나');
sb.Append('바');
sb.Append('카');
sb.Append('다');
var charArr = new char[] { '마' };
long startMemory = GC.GetTotalMemory(true);
//var result2 = conjugator.Conjugate2(stem, conjugationParams);
var result2 = new string(charArr);
//var result2 = string.Create(6, sb, (span, builder) =>
//{
//    builder.CopyTo(0, span, 6);
//});
long endMemory = GC.GetTotalMemory(true);
long allocatedMemory = endMemory - startMemory;
Console.WriteLine($"Memory allocated within Conjugate method: {allocatedMemory} bytes");
Console.WriteLine(result2);
Console.WriteLine(Encoding.UTF8.GetByteCount(result.Value));
Console.WriteLine(Encoding.ASCII.GetByteCount(result.Value));
Console.WriteLine(result.Value.Length * sizeof(char));
//var result = conjugator.MemoryTest(stem, conjugationParams);
//Console.WriteLine(result.Value);