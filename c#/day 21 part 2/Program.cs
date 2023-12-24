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

ulong odd = 0;
ulong even = 0;
ulong even_corners = 0;
ulong odd_corners = 0;
for(int i = 0; i < height; i++)
{
    for(int j  = 0; j < width; j++)
    {
        if(dists[i,j] != -1)
        {
            if(dists[i,j] % 2 == 0)
            {
                even++;
                if(dists[i,j] > 65)
                {
                    even_corners++;
                }
            }
            else
            {
                odd++;
                if(dists[i,j] > 65)
                {
                    odd_corners++;
                }
            }
        }
    }
}

ulong n = 202300;
ulong result = (n+1)*(n+1)*odd + n*n*even - (n+1) * odd_corners + n * even_corners;
Console.WriteLine(result);