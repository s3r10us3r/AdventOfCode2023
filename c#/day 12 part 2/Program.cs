static bool checkIfFixedPosition(int end, int newStart, HashSet<int> fixedPositions)
{
    bool noInBetween = true;
    for(int i = end + 1; i < newStart; i++)
    {
        if(fixedPositions.Contains(i))
        {
            noInBetween = false;
            break;
        }
    }
    return noInBetween;
}

static void solve(string spring, List<int> nums, int numIndex, List<(int end, long numOfPositions)> ends, HashSet<int> fixedPositions)
{   
    int lastFixed = fixedPositions.Count > 0 ? fixedPositions.Max() : 0;
    int num = nums[numIndex];
    List<(int end, long numOfPositions)> newEnds = [];
    for(int i = 0; i <= spring.Length - num; i++)
    {
        bool isViable = true;
        for(int j = i; j < i + num; j++)
        {
            if(spring[j] == '.')
                isViable = false;
        }

        if(isViable)
        {
            newEnds.Add((i + num - 1, 0));
        }
    }

    if(numIndex == nums.Count - 1)
    {
        for(int i = 0; i < newEnds.Count; i++)
        {
            newEnds[i] = (newEnds[i].end, 1);
        }
    }
    else
    {
        solve(spring, nums, numIndex+1, newEnds, fixedPositions);
    }
    for(int i = 0; i < newEnds.Count; i++)
    {
        if( numIndex == nums.Count - 1 && newEnds[i].end < lastFixed)
        {
            continue;
        }
        for(int j = 0; j < ends.Count; j++)
        {
            if( newEnds[i].end - num + 1 > ends[j].end + 1 && checkIfFixedPosition(ends[j].end, newEnds[i].end - num + 1, fixedPositions))
            {
                ends[j] = (ends[j].end, ends[j].numOfPositions + newEnds[i].numOfPositions);
            }
        }
    }
}

string filePath = "input.txt";

List<(string spring, List<int> nums)> problemList = []; 

try
{
    using StreamReader sr = new StreamReader(filePath);
    string line;

    while ((line = sr.ReadLine()) != null)
    {
        string[] strings = line.Split(" ");
        
        string spring = strings[0] + "?" + strings[0] + "?" + strings[0] + "?" + strings[0] + "?" + strings[0];
        List<int> nums = [];

        for(int i =0; i < 5; i++){
            foreach(string num in strings[1].Split(','))
            {
                nums.Add(int.Parse(num));
            }
        }

        problemList.Add((spring, nums));
    }
}
catch (Exception e)
{
    Console.WriteLine($"File wrong");
    Console.WriteLine(e);
}

long result = 0;



foreach(var (spring, nums) in problemList)
{
    HashSet<int> fixedPositions = [];
    for(int i = 0; i < spring.Length; i++)
    {
        if(spring[i] == '#')
            fixedPositions.Add(i);
    }
    List<(int end, long numOfPositions)> startList = [(-2, 0)];
    solve(spring, nums, 0, startList, fixedPositions); 
    result += startList[0].numOfPositions;
}


Console.WriteLine(result);