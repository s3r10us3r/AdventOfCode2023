using System.ComponentModel.DataAnnotations;

static int solve(string spring, List<int> nums, int numIndex, int start, int left_to_cover)
{   
    int num = nums[numIndex];
    int possibleSolutions = 0;
    for(int i = start; i < spring.Length - num + 1; i++)
    {
        bool canPlace = true;
        int covered = 0;
        for(int j = i; j < i + num; j++)
        {
            if(spring[j] == '.')
                canPlace = false;
            if(spring[j] == '#')
                covered++;
        }
        if( canPlace && (i == start || spring[i - 1] != '#') && (i + num == spring.Length || spring[i + num] != '#') )
        {
            if(numIndex == nums.Count - 1)
            {
                if(left_to_cover - covered == 0)
                    possibleSolutions++;
            }
            else
            {
                possibleSolutions += solve(spring, nums, numIndex+1, i + num + 1, left_to_cover - covered);
            }
        }
    }

    return possibleSolutions;
}

string filePath = "input.txt"; // Replace with the path to your file

List<(string spring, List<int> nums)> problemList = []; 

try
{
    using StreamReader sr = new StreamReader(filePath);
    string line;

    // Read and display lines from the file until the end of the file is reached.
    while ((line = sr.ReadLine()) != null)
    {
        string[] strings = line.Split(" ");
        
        string spring = strings[0];
        List<int> nums = [];

        foreach(string num in strings[1].Split(','))
        {
            nums.Add(int.Parse(num));
        }

        problemList.Add((spring, nums));
    }
}
catch (Exception e)
{
    Console.WriteLine($"File wrong");
    Console.WriteLine(e);
}

int result = 0;



foreach(var (spring, nums) in problemList)
{
    int thissol = solve(spring, nums, 0, 0, spring.Count(c => c == '#')); 
    result += thissol;
}


Console.WriteLine(result);