static string[] ParseInput(string filePath)
{
    try
    {
        using(StreamReader sr = new(filePath))
        {
            string input = sr.ReadLine();
            return input.Split(',');
        }
    } catch(Exception e)
    {
        Console.WriteLine(e);
        return null;
    }
}

static int Hash(string s)
{
    int cv = 0;
    foreach(char c in s)
    {
        cv += c;
        cv *= 17;
        cv %= 256;
    }
    return cv;
}

static void minusOp(string label, List<Box> boxes)
{
    int hash = Hash(label);
    boxes[hash].contents.Remove(new Lens(0, label));
}

static void equalOP(string label, int length, List<Box> boxes)
{
    int hash = Hash(label);
    Lens lens = new Lens(length, label);
    if(boxes[hash].contents.Contains(lens))
    {
        LinkedListNode<Lens> lensNode = boxes[hash].contents.Find(lens);
        lensNode.Value.length = lens.length;
    }
    else
    {
        boxes[hash].contents.AddLast(lens);
    }
}


List<Box> boxes = [];
for(int i = 0; i < 256; i++)
{
    boxes.Add(new Box());
}

string[] strings = ParseInput("input.txt");
long result = 0;
foreach(string s in strings)
{
    if (s.Contains('='))
    {
        args = s.Split('=');
        string label = args[0];
        int length = int.Parse(args[1]);
        equalOP(label, length, boxes);
    }
    else
    {
        args = s.Split('-');
        string label = args[0];
        minusOp(label, boxes);
    }
}

for(int i = 0; i < 256; i++)
{
    int j = 0;
    foreach(Lens lens in boxes[i].contents)
    {
        j++;
        result += (1+i) * j * lens.length;
    }
}
Console.WriteLine(result);