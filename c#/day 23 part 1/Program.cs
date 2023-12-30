List<string> map = [];

using(StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        map.Add(line);
    }
}

int height = map.Count;
int width = map[0].Length;

int count = 0;

int FindLongestPath((int y, int x) now, int length, HashSet<(int, int)> visited)
{
    count++;
    if(visited.Contains(now))
    {
        return length - 1;
    }
    int result = length;
    if(now.y == height - 1 && now.x == width - 2)
    {
        return result;
    }

    HashSet<(int, int)> newVisited = new(visited);
    newVisited.Add(now);

    char currentTile = map[now.y][now.x];
    if(currentTile == '<')
    {
        return FindLongestPath((now.y, now.x - 1), length+1, newVisited);
    }
    if(currentTile == '>')
    {
        return FindLongestPath((now.y, now.x + 1), length+1, newVisited);
    }
    if(currentTile == 'v')
    {
        return FindLongestPath((now.y + 1, now.x), length+1, newVisited);
    }
    if(currentTile == '^')
    {
        return FindLongestPath((now.y - 1, now.x), length+1, newVisited);
    }

    (int, int)[] nexts = [(now.y - 1, now.x), (now.y + 1, now.x), (now.y, now.x - 1), (now.y, now.x + 1)];

    foreach((int y, int x) in nexts)
    {
        if(x < 0 || x >= width || y < 0 || y >= height)
            continue;
        
        char tile = map[y][x];
        if(tile != '#')
        {
            int score = FindLongestPath((y,x), length+1, newVisited);
            result = score > result ? score : result;
        }
    }

    return result;
}

Console.WriteLine(FindLongestPath((0, 1), 0, []));
Console.WriteLine(count);