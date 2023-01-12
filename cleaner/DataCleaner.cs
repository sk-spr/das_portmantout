using System.Text.Json;
using System.Text.RegularExpressions;

Console.WriteLine("Welcome to PortManTout cleaner.\n Skye Sprung 2023");

using var reader = new StreamReader(File.OpenRead("../data/raw.txt"));
List<string> lines = new();
while (!reader.EndOfStream)
    lines.Add(reader.ReadLine() ?? "");
Console.WriteLine("Done reading.");
reader.Close();
List<string> words = new();
foreach (var line in lines)
{
    //remove variable whitespace
    var singleWhiteSpace = Regex.Replace(line, "[ +\t+]", " ");
    try
    {
        var parts = singleWhiteSpace.Split(" ");
        if (parts.Length == 3)
        {
            var readWord = parts[1];
            var frequency = int.Parse(parts[2]);
            if(!Regex.IsMatch(readWord, "[^a-zA-ZüÜäÄöÖß']"))
                words.Add(readWord);
            
            //words.Add(new Entry { word = readWord, occurrances = frequency });
        }

    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception encountered on line {line}: {e.Message}. Pre split is {singleWhiteSpace}. Attempting to continue and ignore.");
    }
}

using var outstream = File.OpenWrite("../data/processed.json");
JsonSerializer.Serialize(outstream, words);