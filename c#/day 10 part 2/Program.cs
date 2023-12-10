static List<int> GetEnds(int pos, char sign, int width)
{
    int N = -width, S = width, E = 1, W = -1;
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

int height = 0, width = 0;

try
{
    StreamReader sr = new("input.txt");
    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        map += line;
        width = line.Length;
        height++;
    }
    sr.Close();
}
catch (IOException e)
{
    Console.WriteLine(e);
}

int start = 0;

for(int row = 0; row < height; row++)
{
    for(int col = 0; col < width; col++)
    {
        if(map[row * width + col] == 'S')
            start = row * width + col;
    }
}

int N, S, E, W;
(N, S, E, W) = (-width, width, 1, -1);

var dists = new Dictionary<int, int>();

Queue<(int pos, int dist, int before)> toCheck = [];

if(start / width > 0)
{
    if(GetEnds(start + N, map[start + N], width).Contains(start))
        toCheck.Enqueue((start + N, 1, start));
}
    
if(start / width < height - 1)
{
    if(GetEnds(start + S, map[start + S], width).Contains(start))
        toCheck.Enqueue((start + S, 1, start));
}
if(start % width < width - 1)
{
    if(GetEnds(start + E, map[start + E], width).Contains(start))
        toCheck.Enqueue((start + E, 1, start));
}
if(start % width > 0)
{
    if(GetEnds(start + W, map[start + W], width).Contains(start))
        toCheck.Enqueue((start + W, 1, start));
}

HashSet<int> loop = [start];

int firstRow = 150;
int lastRow = 0;
int firstCol = 150;
int lastCol = 0;
while( !dists.ContainsKey(toCheck.Peek().pos) )
{
    int nowChecked, dist, before;
    (nowChecked, dist, before) = toCheck.Dequeue();
    var ends = GetEnds(nowChecked, map[nowChecked], width);
    if(ends[0] == before)
    {
        toCheck.Enqueue((ends[1], dist+1, nowChecked));
        loop.Add(nowChecked);
        if(nowChecked / width < firstRow)
            firstRow = nowChecked / width;
        if(nowChecked / width > lastRow)
            lastRow = nowChecked / width;
        if(nowChecked % width < firstCol)
            firstCol = nowChecked % width;
        if(nowChecked % width > lastCol)
            lastCol = nowChecked % width;
    }
    else if(ends[1] == before)
    {
        toCheck.Enqueue((ends[0], dist+1, nowChecked));
        loop.Add(nowChecked);
        if(nowChecked / width < firstRow)
            firstRow = nowChecked / width;
        if(nowChecked / width > lastRow)
            lastRow = nowChecked / width;
        if(nowChecked % width < firstCol)
            firstCol = nowChecked % width;
        if(nowChecked % width > lastCol)
            lastCol = nowChecked % width;
    }
    dists.Add(nowChecked, dist);
}

int result = 0;



for(int row = 0; row < height; row++)
{
    int sideCount = 0;
    int cell = row * width;
    while(cell < (row+1) * width)
    {
        if(loop.Contains(cell))
        { 
            if(map[cell] != '|')
            {
                char cornerStart = map[cell++];
                while(map[cell] == '-')
                    cell++;
                string corner = cornerStart.ToString() + map[cell].ToString();
                if(corner == "FJ" || corner == "L7" || corner == "LS")
                    sideCount++;
            }
            else
            {
                sideCount++;
            }   
        }
        else if(sideCount % 2 == 1)
        {
            result++;
        }
        cell++;
    }
}

Console.WriteLine(result);