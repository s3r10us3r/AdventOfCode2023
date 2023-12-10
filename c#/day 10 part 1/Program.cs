using System.Collections;
using System.Data.Common;

static List<int> GetEnds(int pos, char sign, int size)
{
    int N = -size, S = size, E = 1, W = -1;
    return sign switch
    {
        '|' => [pos + N, pos + S],
        '-' => [pos + E, pos + W],
        'L' => [pos + N, pos + E],
        'J' => [pos + N, pos + W],
        'F' => [pos + S, pos + E],
        '7' => [pos + S, pos + W],
        'S' => [pos + S, pos + N, pos + W, pos + E],
        _ => [-1, -1],
    };
}

string map = "";

try
{
    StreamReader sr = new("input.txt");
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        map += line;
    }
    sr.Close();
}
catch (IOException e)
{
    Console.WriteLine(e);
}

int size = (int)Math.Sqrt(map.Length);
Console.WriteLine(size);

int start = 0;

for(int row = 0; row < size; row++)
{
    for(int col = 0; col < size; col++)
    {
        if(map[row * size + col] == 'S')
            start = row * size + col;
    }
}

int N, S, E, W;
(N, S, E, W) = (-size, size, 1, -1);

var dists = new Dictionary<int, int>();

Queue<(int pos, int dist, int before)> toCheck = [];

if(start / size > 0)
{
    if(GetEnds(start + N, map[start + N], size).Contains(start))
        toCheck.Enqueue((start + N, 1, start));
}
    
if(start / size < size - 1)
{
    if(GetEnds(start + S, map[start + S], size).Contains(start))
        toCheck.Enqueue((start + S, 1, start));
}
if(start % size < size - 1)
{
    if(GetEnds(start + E, map[start + E], size).Contains(start))
        toCheck.Enqueue((start + E, 1, start));
}
if(start % size > 0)
{
    if(GetEnds(start + W, map[start + W], size).Contains(start))
        toCheck.Enqueue((start + W, 1, start));
}

while( !dists.ContainsKey(toCheck.Peek().pos) )
{
    int nowChecked, dist, before;
    (nowChecked, dist, before) = toCheck.Dequeue();
    var ends = GetEnds(nowChecked, map[nowChecked], size);
    if(ends[0] == before)
    {
        toCheck.Enqueue((ends[1], dist+1, nowChecked));
    }
    else if(ends[1] == before)
    {
        toCheck.Enqueue((ends[0], dist+1, nowChecked));
    }
    dists.Add(nowChecked, dist);
}

Console.WriteLine(toCheck.Peek().dist);