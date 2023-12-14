string filePath = "input.txt";

List<string> platform = [];
try
{
    using StreamReader sr = new(filePath);
    string line;

    while ((line = sr.ReadLine()) != null)
    {
        platform.Add(line);
    }
}
catch (Exception e)
{
    Console.WriteLine($"File wrong");
    Console.WriteLine(e);
}


int width = platform[0].Length;
int height = platform.Count;

long result = 0;
for(int i = 0; i < width; i++)
{
    int highestRockPosition = height + 1;
    for(int j = 0; j < height; j++)
    {
        if (platform[j][i] == 'O')
        {
            highestRockPosition--;
            result += highestRockPosition;
        }
        if (platform[j][i] == '#')
        {
            highestRockPosition = height - j;
        }
    }
}

Console.WriteLine(result);