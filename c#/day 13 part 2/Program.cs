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

static int findSmugedAxis(List<string> problem)
{
    for(int axis = 1; axis < problem[0].Length; axis++)
    {
        int smugedAxis = -1;
        foreach(string line in problem)
        {
            for(int i = 0; i < Math.Min(axis, Math.Abs(problem[0].Length - axis)); i++)
            {
                if(line[axis - 1 - i] != line[axis + i])
                {
                    if( smugedAxis == -1)
                        smugedAxis = axis;
                    else
                    {
                        smugedAxis = -2;
                        break;   
                    }
                }
            }
            if(smugedAxis == -2)
            {
                break;
            }
        }
        if(smugedAxis > -1)
        {
            return smugedAxis;
        }
    }
    return 0;
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
int i = -1;
foreach(var problem in problems)
{
    i++;
    int smugedAxis = findSmugedAxis(problem);
    if(smugedAxis > 0)
    {
        result += smugedAxis;
        continue;
    }
    List<string> problemColumns = transposeStringList(problem);
    smugedAxis = findSmugedAxis(problemColumns);
    if(smugedAxis > 0)
    {
        result += 100 * smugedAxis;
    }
}

Console.WriteLine(result);