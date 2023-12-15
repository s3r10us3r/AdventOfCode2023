static string[] ParseInput(string filePath)
{
    try
    {
        using StreamReader sr = new(filePath);
        string input = sr.ReadLine();
        return input.Split(',');
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

string[] strings = ParseInput("input.txt");
int result = 0;
foreach(string s in strings)
{
    result += Hash(s);
}
Console.WriteLine(result);