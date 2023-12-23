
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
        if(name == "R")
        {
            continue;
        }

        if(name == "A")
        {
            acceptedParts.Add(newPart);
            continue;
        }
    
        proccesPart(newPart, conditions[name]);
        
    }
}

proccesPart(new Part(), conditions["in"]);


ulong result = 0;
foreach(Part part in acceptedParts)
{
    ulong xRange = part.maximums["x"] - part.minimums["x"] + 1;
    ulong mRange = part.maximums["m"] - part.minimums["m"] + 1;
    ulong aRange = part.maximums["a"] - part.minimums["a"] + 1;
    ulong sRange = part.maximums["s"] - part.minimums["s"] + 1;
    result += xRange * mRange * aRange * sRange;
}

Console.WriteLine(result);