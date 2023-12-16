List<string> map = [];

try{
    using StreamReader streamReader = new("input.txt");
    string line;
    while((line = streamReader.ReadLine()) != null)
    {
        map.Add(line);
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}

const int N = 0, S = 1, E = 2, W = 3;

int x = -1;
int y = 0;
int direction = E; 

int size = map.Count;
bool[,] energized = new bool[size, size];
bool[,,] visitedFrom = new bool[size, size, 4];


void reflectBeam(int x, int y, int direction, char c)
{
    if(c == '\\')
    {
        switch(direction)
        {
            case N:
                visitNode(x, y, W);
                break;
            case S:
                visitNode(x, y, E);
                break;
            case W:
                visitNode(x, y, N);
                break;
            case E:
                visitNode(x, y, S);
                break;
        }
        return;
    }

    if(c == '/')
    {
        switch(direction)
        {
            case N:
                visitNode(x, y, E);
                break;
            case S:
                visitNode(x, y, W);
                break;
            case W:
                visitNode(x, y, S);
                break;
            case E:
                visitNode(x, y, N);
                break;
        }
        return;
    }

    if(c == '-')
    {
        if(direction == N || direction == S)
        {
            visitNode(x,y, W);
            visitNode(x,y,E);
        }
        else
        {
            visitNode(x,y,direction);
        }
        return;
    }
    
    if(c == '|')
    {
        if(direction == E || direction == W)
        {
            visitNode(x,y, N);
            visitNode(x,y, S);
        }
        else
        {
            visitNode(x,y,direction);
        }
        return;
    }
    visitNode(x,y,direction);
}

void visitNode(int x, int y, int direction)
{
    switch(direction)
    {
        case N:
            y--;
            break;
        case S:
            y++;
            break;
        case E:
            x++;
            break;
        case W:
            x--;
            break;
    }

    if(x >= size || x < 0 || y >= size || y < 0 || visitedFrom[x,y,direction])
    {
        return;
    }

    visitedFrom[x,y,direction] = true;
    energized[x,y] = true;

    char c = map[y][x];
    reflectBeam(x, y, direction, c);
}

visitNode(x, y, direction);
int result = 0;
foreach(bool energy in energized)
{
    if(energy)
        result++;
}

Console.WriteLine(result);