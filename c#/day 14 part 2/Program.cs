static void pushNorth(List<List<char>> platform)
{
    int width = platform[0].Count;
    int height = platform.Count;

    for(int i = 0; i < width; i++)
    {
        int highestEmpty = 0;
        for(int j = 0; j < height; j++)
        {
            if(platform[j][i] == '#')
                highestEmpty = j + 1;
            if(platform[j][i] == 'O')
            {
                platform[j][i] = '.';
                platform[highestEmpty++][i] = 'O';
            }
        }
    }
}

static void rotate(List<List <char>> platform)
{
    int size = platform.Count;
    //transpose
    for(int i = 0; i < size; i++)
    {
        for(int j = i; j < size; j++)
        {
            (platform[j][i], platform[i][j]) = (platform[i][j], platform[j][i]);
        }
    }
    //reverse
    for(int i = 0; i < size; i++)
    {
        int start = 0, end = size - 1;
        while(start < end)
        {
            (platform[i][end], platform[i][start]) = (platform[i][start], platform[i][end]);
            start++;
            end--;
        }
    }
}

static void cycle(List<List<char>> platform)
{
    for(int i = 0; i < 4; i++)
    {
        pushNorth(platform);
        rotate(platform);
    }
}

static int computeLoad(List<List<char>> platform)
{
    int result = 0;
    int size = platform.Count;
    for(int i = 0; i < size; i++)
    {
        for(int j = 0; j < size; j++)
        {
            if(platform[i][j] == 'O')
                result += size - i;
        }
    }
    return result;
}

static string writePlatform(List<List<char>> platform)
{
    string platformString = "";
    foreach(var line in platform)
    {
        foreach(var chr in line)
        {
            platformString += chr;
        }
        platformString+='\n';
    }
    return platformString;
}

string filePath = "input.txt";

List<List<char>> platform = [];
try
{
    using StreamReader sr = new(filePath);
    string line;

    while ((line = sr.ReadLine()) != null)
    {
        platform.Add(line.ToList());
    }
}
catch (Exception e)
{
    Console.WriteLine($"File wrong");
    Console.WriteLine(e);
}

Dictionary<string, int> platformCycleHashtable = [];
int cyclesLeft = 1_000_000_000;

while(!platformCycleHashtable.ContainsKey(writePlatform(platform)))
{
    platformCycleHashtable.Add(writePlatform(platform), cyclesLeft--);
    cycle(platform);
}

int cycleLength = platformCycleHashtable[writePlatform(platform)] - cyclesLeft; 
int cyclesToMake = cyclesLeft % cycleLength;

for(int i = 0; i < cyclesToMake; i++)
{
    cycle(platform);
}

Console.WriteLine(computeLoad(platform));