List<List<char>> map = [];

static void addLine(List<List<char>> list, int index, int width)
{
    List<char> line = Enumerable.Repeat('.', width).ToList();
    list.Insert(index, line);
}

static void addCol(List<List<char>> list, int index, int height)
{
    foreach(var line in list)
    {
        line.Insert(index, '.');
    }
}

static int manhattanDistance(int x1, int y1, int x2, int y2)
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

List<int> rowsToAdd = [];
List<int> colsToAdd = [];

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
        rowsToAdd.Add(i);
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
        colsToAdd.Add(i);
    }
}

for(int i = 0; i < rowsToAdd.Count; i++)
{
    addLine(map, rowsToAdd[i] + i, width);
}
height = map.Count;
for(int i = 0; i < colsToAdd.Count; i++)
{
    addCol(map, colsToAdd[i] + i, height);
}
width = map[0].Count;
List<(int x, int y)> galaxies = [];



for(int x = 0; x < height; x++)
{
    for(int y = 0; y < width; y++)
    {
        if(map[x][y] == '#')
            galaxies.Add((x, y));
    }
}

int result = 0;

for(int i = 0; i < galaxies.Count - 1; i++)
{
    for(int j = i + 1; j < galaxies.Count; j++)
    {
        int dist = manhattanDistance(galaxies[i].x, galaxies[i].y, galaxies[j].x, galaxies[j].y);
        result += dist;
    }
}

Console.WriteLine(result);