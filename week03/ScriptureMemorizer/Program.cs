using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptureLibrary = new List<Scripture>();

        scriptureLibrary.Add(new Scripture(new Reference("John", 3, 16),
            "For God so loved the world that he gave his only begotten Son, that whoever believes in him should not perish but have eternal life."));

        scriptureLibrary.Add(new Scripture(new Reference("Psalm", 23, 1),
            "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: he leadeth me beside the still waters."));

        scriptureLibrary.Add(new Scripture(new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."));

        var rnd = new Random();
        Scripture current = scriptureLibrary[rnd.Next(scriptureLibrary.Count)];

        Console.WriteLine("Memorize the scripture. Type 'quit' to exit.");
        while (true)
        {
            Console.Clear();
            Console.WriteLine(current.GetReferenceText());
            Console.WriteLine();
            Console.WriteLine(current.GetDisplayText());
            Console.WriteLine();

            if (current.IsCompletelyHidden())
            {
                Console.WriteLine("All words hidden. Well done!");
                break;
            }

            Console.Write("Press Enter to hide more words or type 'quit' to exit: ");
            string input = Console.ReadLine();
            if (input != null && input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                break;

            current.HideRandomWords(3, rnd);
        }
    }
}

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int? VerseEnd { get; }

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        return VerseEnd.HasValue
            ? $"{Book} {Chapter}:{VerseStart}-{VerseEnd}"
            : $"{Book} {Chapter}:{VerseStart}";
    }
}

class Scripture
{
    private readonly Reference _reference;
    private readonly string[] _tokens;
    private readonly bool[] _hidden;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _tokens = text.Split(' ');
        _hidden = new bool[_tokens.Length];
        for (int i = 0; i < _tokens.Length; i++)
            if (!ContainsLetterOrDigit(_tokens[i]))
                _hidden[i] = true;
    }

    public string GetReferenceText() => _reference.ToString();

    public string GetDisplayText()
    {
        return string.Join(" ", _tokens.Select((t, i) => _hidden[i] ? MaskToken(t) : t));
    }

    public bool IsCompletelyHidden()
    {
        return _hidden.All(h => h);
    }

    public void HideRandomWords(int count, Random rnd)
    {
        var candidates = Enumerable.Range(0, _tokens.Length).Where(i => !_hidden[i] && ContainsLetterOrDigit(_tokens[i])).ToList();
        if (candidates.Count == 0) return;
        int toHide = Math.Min(count, candidates.Count);
        for (int k = 0; k < toHide; k++)
        {
            int idx = rnd.Next(candidates.Count);
            _hidden[candidates[idx]] = true;
            candidates.RemoveAt(idx);
        }
    }

    private static bool ContainsLetterOrDigit(string s) => s.Any(char.IsLetterOrDigit);

    private static string MaskToken(string token)
    {
        var chars = token.Select(c => char.IsLetterOrDigit(c) ? '_' : c).ToArray();
        return new string(chars);
    }
}