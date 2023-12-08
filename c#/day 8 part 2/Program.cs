using System.Xml.XPath;

static double GCD(double a, double b)
{
    if (a == 0)
        return b;
    
    return GCD(b % a, a);
}


var graph = new Dictionary<string, (string left, string right)>();

string roadString = Console.ReadLine();
Console.ReadLine();

int t = 750;

List <string> currentNodes = [];

while(t-- > 0)
{
    string[] input = Console.ReadLine().Split(' ');
    input[2] = input[2].Replace("(", "").Replace(",", "");
    input[3] = input[3].Replace(")", "");

    graph.Add(input[0], (input[2], input[3]));
    if(input[0][2] == 'A')
        currentNodes.Add(input[0]);
}

double multiple = 1;

foreach(var key in currentNodes)
{
    int index = 0;
    double roadLength = 0;
    string currentNode = key;
    while( currentNode[2] != 'Z')
    {
        roadLength++;
        char direction = roadString[index++];
        if(direction == 'L')
            currentNode = graph[currentNode].left;
        else
            currentNode = graph[currentNode].right;
        if (index == roadString.Length)
            index = 0;
    }
    double gcd = GCD(multiple, roadLength);

    multiple *= roadLength/gcd;
}

Console.WriteLine(multiple);