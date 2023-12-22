
Dictionary<string, Condition>  conditions = [];
using (StreamReader sr = new("input.txt"))
{
    string line;
    while( (line = sr.ReadLine()) != "")
    {
        string[] inputs = line.Split("{");
        string name = inputs[0];
        string conditionsString = inputs[1].Trim('}');
        string[] conditionsStrings = conditionsString.Split(",");
        Console.WriteLine($"conditions string: {conditionsString}");
        List<string> littleConditions = [];
        for(int i = 0; i < conditionsStrings.Length - 1; i++)
        {
            littleConditions.Add(conditionsStrings[i]);   
        }

        conditions.Add(name, new Condition(littleConditions, conditionsStrings[^1]));
    }
}

List<Part> acceptedParts = [];

void proccesPart(Part part, Condition condition)
{
    List<(string condition, Part part)> proccesedParts = condition.Process(part);
    foreach((string name, Part newPart) in proccesedParts)
    {
        if(name == "A")
        {
            acceptedParts.Add(newPart);
        }
        else if(name != "R")
        {
            proccesPart(newPart, conditions[name]);
        }
    }
}

proccesPart(new Part(), conditions["in"]);


ulong result = 0;
foreach(Part part in acceptedParts)
{
    Console.WriteLine($"minimal attrs: x={part.minimums["x"]} m={part.minimums["m"]} a={part.minimums["a"]} s={part.minimums["s"]}");
    Console.WriteLine($"maximal attr: x={part.maximums["x"]} m={part.maximums["m"]} a={part.maximums["a"]} s={part.maximums["s"]}");
    int xRange = part.maximums["x"] - part.minimums["x"] - 2;
    int mRange = part.maximums["m"] - part.minimums["m"] - 2;
    int aRange = part.maximums["a"] - part.minimums["a"] - 2;
    int sRange = part.maximums["s"] - part.minimums["s"] - 2;
    result += (ulong)(xRange * mRange * aRange * sRange);
}

for(int i = 0; i < 4000; i++)
{
    for(int ii = 0; ii < 4000; ii++)
        for(int iii = 0; iii < 4000; iii++)
            for(int j = 0; j < 4000; j++)
            {
                
            }
}
Console.WriteLine(result);