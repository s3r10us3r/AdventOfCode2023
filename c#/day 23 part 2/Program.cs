public class MainClass{
    
    private const string inputFile = "input.txt";
    
    static int width;
    static int height;
    static (int y, int x) start = (0, 1);
    static (int y, int x) finish;
    static int maxLen = 0;

    static List<string> map = [];
    static Dictionary<(int y, int x), Node> allNodes = [];
    static HashSet<(int y, int x)> visited = [];

    public static List<string> ReadFile()
    {
        List<string> result = [];

        using (StreamReader sr = new(inputFile))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                result.Add(line);
            }
        }

        width = result[0].Length;
        height = result.Count;
        finish = (height - 1, width - 2);

        return result;
    }

    public static void FindAllNodes()
    {
        for(int y = 1; y < height - 1; y++)
        {
            for(int x = 1; x < width - 1; x++)
            {
                if(map[y][x] == '#')
                {
                    continue;
                }
                (int y, int x)[] around = [(y + 1, x), (y - 1, x), (y, x - 1), (y, x + 1)];
                int empty_count = 0;
                foreach((int ny, int nx) in around)
                {
                    if(map[ny][nx] != '#')
                    {
                        empty_count++;
                    }
                }

                if(empty_count != 2)
                {
                    Node newNode = new Node(y, x);
                    allNodes.Add((y,x),newNode);
                }
            }
        }

        allNodes.Add(start, new Node(start.y, start.x));
        allNodes.Add(finish, new Node(finish.y, finish.x));
    }

    static (Node node, int length) TraverseTile(int length, (int y, int x) newPos, (int y, int x) prevPos)
    {
        if(allNodes.ContainsKey(newPos))
        {
            return (allNodes[newPos], length);
        }
        (int y, int x)[] around = [(newPos.y + 1, newPos.x), (newPos.y - 1, newPos.x), (newPos.y, newPos.x  - 1), (newPos.y, newPos.x + 1)];

        foreach((int ny, int nx) in around)
        {
            if((ny,nx) != prevPos && map[ny][nx] != '#')
            {
                var result = TraverseTile(length+1, (ny,nx), newPos);
                return (result.node, result.length);
            }
        }

        throw new ArgumentOutOfRangeException("TRAVERSED WITHOUT SUCCESS");
    }

    static void ConnectNodes()
    {
        foreach(((int y, int x), Node node) in allNodes)
        {
            (int y, int x)[] around = [(y+1, x), (y-1,x), (y, x-1), (y, x+1)];
            foreach(var tile in around)
            {
                if(tile.y < 0 || tile.y >= height || tile.x < 0 || tile.x >= width)
                {
                    continue;
                }

                if(map[tile.y][tile.x] != '#')
                {
                    (Node connectedNode, int len) = TraverseTile(1, tile, node.pos);
                    node.Connect(connectedNode, len); 
                }
            }
        }
    }

    static void FindLongestPath(Node node, int length)
    {
        if(node.pos.x == finish.x && node.pos.y == finish.y)
        {
            if(length > maxLen)
            {
                maxLen = length;
            }
            return;
        }

        visited.Add(node.pos);

        foreach((Node other, int len) in node.vertices)
        {
            if(!visited.Contains(other.pos))
            {
                FindLongestPath(other, length + len);
            }
        }
        visited.Remove(node.pos);
    }

    static void Main(String[] args)
    {
        map = ReadFile();
        FindAllNodes();
        ConnectNodes();
        
        FindLongestPath(allNodes[start], 0);
        Console.WriteLine(maxLen);
    }
}


class Node(int y, int x)
{
    public List<(Node node, int length)> vertices = [];
    public (int y, int x) pos = (y, x);

    public void Connect(Node otherNode, int length)
    {
        vertices.Add((otherNode, length));
    }
}
