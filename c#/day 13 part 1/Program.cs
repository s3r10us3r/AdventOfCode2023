static string ReverseString(string s1)
{
    char[] chars = s1.ToCharArray();
    Array.Reverse(chars);
    return new string(chars);
}

static HashSet<int> FindSymmetryAxes(string line)
{
    HashSet<int> axes = [];
    for(int i = 0; i < line.Length / 2; i++)
    {
        string leftSide = line[..(i + 1)];
        string rightSide = line[(i+1)..(2*i + 2)];
        if(leftSide == ReverseString(rightSide))
            axes.Add(i + 1);
    }
    for(int i = line.Length/2; i > 0; i--)
    {
        string leftSide = line[(line.Length - 2*i)..(line.Length - i)];
        string rightSide = line[(line.Length - i)..];
        if(leftSide == ReverseString(rightSide))
            axes.Add(line.Length - i);
    }
    return axes;
}


static List<string> transposeStringList(List<string> strings)
{
    List<string> transposed = [];
    for(int i = 0; i < strings[0].Length; i++)
    {
        transposed.Add("");
    }
    foreach(string line in strings)
    {
        for(int i = 0; i < line.Length; i++)
        {
            transposed[i] += line[i];
        }
    }
    return transposed;
}

string filePath = "input.txt";

List<List<string>> problems = [];
try
{
    using StreamReader sr = new(filePath);
    string line;

    while ((line = sr.ReadLine()) != null)
    {
        List<string> problem = [];
        while(line != null && line != "")
        {
            problem.Add(line);
            line = sr.ReadLine();
        }
        problems.Add(problem);
    }
}
catch (Exception e)
{
    Console.WriteLine($"File wrong");
    Console.WriteLine(e);
}

int result = 0;
foreach(var problem in problems)
{
    HashSet<int> possibleAxes = FindSymmetryAxes(problem[0]);
    foreach(string line in problem)
    {
        possibleAxes.IntersectWith(FindSymmetryAxes(line));
        if(possibleAxes.Count == 0)
        {
            break;
        }
    } 
    if(possibleAxes.Count > 0)
    {
        result += possibleAxes.ToList()[0];
        continue;
    }


    List<string> cols = transposeStringList(problem);
    possibleAxes = FindSymmetryAxes(cols[0]);
    foreach(string col in cols)
    {
        possibleAxes.IntersectWith(FindSymmetryAxes(col));
        if(possibleAxes.Count == 0)
        {
            break;
        }
    }
    if(possibleAxes.Count > 0)
    {
        result += 100 * possibleAxes.ToList()[0];
    }
}

Console.WriteLine(result);