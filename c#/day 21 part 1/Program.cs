List<string> map = [];
using(StreamReader sr = new("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        map.Add(line);
    }
}

int width = map[0].Length;
int height = map.Count;

(int y, int x) start = (0,0);

for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        if(map[i][j] == 'S')
        {
            start = (i, j);
            i = height;
            j = width;
        }
        
    }
}


int[,] dists = new int[height, width];

for(int i = 0; i < height; i++)
{
    for(int j = 0; j < width; j++)
    {
        dists[i,j] = -1;
    }
}

Queue<(int dist, int y, int x)> bfsque = [];
bfsque.Enqueue((0, start.y, start.x));

while(bfsque.Count > 0)
{
    (int dist, int y, int x) = bfsque.Dequeue();
    if(dists[y,x] == -1)
    {
        dists[y,x] = dist;
        if(dist < 64)
        {
            if(y > 0 && map[y - 1][x] != '#')
            {
                bfsque.Enqueue((dist + 1, y - 1, x));
            }
            if( y < height - 1 && map[y+1][x] != '#')
            {
                bfsque.Enqueue((dist + 1, y + 1, x));
            }
            if(x > 0 && map[y][x-1] != '#')
            {
                bfsque.Enqueue((dist + 1, y, x-1));
            }
            if(x < width - 1 && map[y][x+1] != '#')
            {
                bfsque.Enqueue((dist + 1, y, x+1));
            }
        }
    }
}

int result = 0;
for(int i = 0; i < height; i++)
{
    for(int j  = 0; j < width; j++)
    {
        if(dists[i,j] != -1 && dists[i,j] % 2 == 0)
        {
            result++;
        }
    }
}

Console.WriteLine(result);