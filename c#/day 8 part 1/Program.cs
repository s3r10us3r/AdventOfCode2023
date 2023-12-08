var graph = new Dictionary<string, (string left, string right)>();

string roadString = Console.ReadLine();
Console.ReadLine();

int t = 750;

while(t-- > 0)
{
    string[] input = Console.ReadLine().Split(' ');
    input[2] = input[2].Replace("(", "").Replace(",", "");
    input[3] = input[3].Replace(")", "");

    graph.Add(input[0], (input[2], input[3]));
}

string currentNode = "AAA";
int result = 0;

int index = 0;
while(currentNode != "ZZZ")
{
    char direction = roadString[index++];
    if(direction == 'L')
        currentNode = graph[currentNode].left;
    else
        currentNode = graph[currentNode].right;
    if(index == roadString.Length)
        index = 0;
    result++;
}

Console.WriteLine((long)result);