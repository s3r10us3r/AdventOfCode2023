List<List<char>> map = [];


static long manhattanDistance(long x1, long y1, long x2, long y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}

string file = "input.txt";
try
{
    using (StreamReader sr = new StreamReader(file))
    {
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            map.Add(new List<char>(line.ToCharArray()));
        }
    }
}
catch(Exception e)
{
    Console.WriteLine("file wrong");
}

int width = map[0].Count;
int height = map.Count;

List<int> emptyRows = [];
List<int> emptyCols = [];

for(int i = 0; i < height; i++)
{
    bool allEmpty = true;
    for(int j = 0; j < width; j++)
    {
        if (map[i][j] != '.')
        {
            allEmpty = false;
        }
    }
    if(allEmpty)
    {
        emptyRows
    .Add(i);
    }
}

for(int i = 0; i < width; i++)
{
    bool allEmpty = true;
    for(int j = 0; j < height; j++)
    {
        if (map[j][i] != '.')
        {
            allEmpty = false;
        }
    }
    if (allEmpty)
    {
        emptyCols.Add(i);
    }
}

width = map[0].Count;
List<(long x, long y)> galaxies = [];

int emptyRowsPassed = 0;

for(int x = 0; x < height; x++)
{
    if(emptyRows.Contains(x))
        emptyRowsPassed+= 999_999;
    int emptyColsPassed = 0;
    for(int y = 0; y < width; y++)
    {
        if(emptyCols.Contains(y))
            emptyColsPassed+=999_999;
        if(map[x][y] == '#')
            galaxies.Add((x + emptyRowsPassed, y + emptyColsPassed));
    }
}

long result = 0;

for(int i = 0; i < galaxies.Count - 1; i++)
{
    for(int j = i + 1; j < galaxies.Count; j++)
    {
        long dist = manhattanDistance(galaxies[i].x, galaxies[i].y, galaxies[j].x, galaxies[j].y);
        result += dist;
    }
}

Console.WriteLine(result);