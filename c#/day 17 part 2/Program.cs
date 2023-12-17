static int[,] parseInput(string path)
{
    try
    {
        using StreamReader streamReader = new(path);
        string line;
        int size;
        List<string> lines = new();
        while((line = streamReader.ReadLine()) != null)
        {
            lines.Add(line);
        }
        size = lines.Count;
        int[,] inputs = new int[size, size];
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                inputs[j,i] = lines[i][j] - '0';
            }
        }
        return inputs;
    }
    catch(Exception ignore)
    {
        return null;
    }
}

static int findResult(int[,] inputs)
{
    int nodesVisited = 0;
    int N = 0, S = 1, W = 2, E = 3;
    int size = inputs.GetLength(0);

    HashSet<(int x, int y, int dir, int inSameDir)> visitTest = new();

    PriorityQueue<(int x, int y, int dist, int dir, int inSameDir), int> pq = new();
    pq.Enqueue((1, 0, inputs[1, 0], E, 1), inputs[1,0]);
    pq.Enqueue((0, 1, inputs[0, 1], S, 1), inputs[1,0]);
    while(true)
    {
        (int x, int y, int dist, int dir, int inSameDir) = pq.Dequeue();
        if(visitTest.Contains((x,y,dir,inSameDir)) || inSameDir > 10)
        {
            continue;
        }
        if(x == size -1 && y == size - 1 && inSameDir >= 4)
        {
            return dist;
        }
        
        visitTest.Add((x,y,dir,inSameDir));
        nodesVisited++;
        if(y != 0 && dir != S  && (dir == N || inSameDir >= 4))
        {
            (int x, int y, int dist, int dir, int inSameDir) newNode = (x, y - 1, dist + inputs[x, y - 1], N, dir == N ? inSameDir + 1 : 1);
            pq.Enqueue(newNode, newNode.dist);
        }
        if(y != size - 1 && dir != N && (dir == S || inSameDir >= 4))
        {
            (int x, int y, int dist, int dir, int inSameDir) newNode = (x, y + 1, dist + inputs[x, y + 1], S, dir == S ? inSameDir + 1 : 1);
            pq.Enqueue(newNode, newNode.dist);  
        }
        if(x != 0 && dir != E && (dir == W || inSameDir >= 4))
        {
            (int x, int y, int dist, int dir, int inSameDir) newNode = (x - 1, y, dist + inputs[x - 1, y], W, dir == W ? inSameDir + 1 : 1);
            pq.Enqueue(newNode, newNode.dist);
        }
        if(x != size - 1 && dir != W && (dir == E || inSameDir >= 4))
        {
            (int x, int y, int dist, int dir, int inSameDir) newNode = (x + 1, y, dist + inputs[x + 1, y], E, dir == E ? inSameDir + 1 : 1);
            pq.Enqueue(newNode, newNode.dist);
        }
    }
}

int[,] inputs = parseInput("input.txt");
Console.WriteLine(findResult(inputs));