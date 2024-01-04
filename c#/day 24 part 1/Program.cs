public class Program
{
    static ulong result = 0;
    static double lowerBound = 200000000000000;
    static double upperBound = 400000000000000;
    public static void Main(string[] args)
    {
        List<Hail> hails = ParseInput("input.txt");

        for(int i = 0; i < hails.Count - 1; i++)
        {
            for(int j = i + 1; j < hails.Count; j++)
            {
                if(CheckIfIntersect(hails[i], hails[j]))
                {
                    result++;
                }
            }
        }

        Console.WriteLine(result);
    }

    static List<Hail> ParseInput(string input)
    {
        List<Hail> result = [];
        using (StreamReader sr = new StreamReader(input))
        {
            string line;
            while((line = sr.ReadLine()) != null)
            {
                result.Add(new Hail(line));
            }
        }

        return result;
    }

    static bool CheckIfIntersect(Hail hail1, Hail hail2)
    {
        double a1 = hail1.dy / hail1.dx;
        double a2 = hail2.dy / hail2.dx;

        double x = (a1 * hail1.x - hail1.y - a2 * hail2.x + hail2.y) / (a1 - a2);
        double y = a1 * (x - hail1.x) + hail1.y;

        double t1 = (x - hail1.x) / hail1.dx;
        double t2 = (x - hail2.x) / hail2.dx;

        return x >= lowerBound && x <= upperBound && y >= lowerBound && y <= upperBound && t1 >= 0 && t2 >= 0;
    }
}



class Hail
{
    public readonly double x;
    public readonly double y;
    public readonly double z;

    public readonly double dx;
    public readonly double dy;
    public readonly double dz;

    public Hail(string hailString)
    {
        string[] hailArr = hailString.Split(" @ ");
        string[] start = hailArr[0].Split(", ");
        string[] vels = hailArr[1].Split(", ");

        x = double.Parse(start[0]);
        y = double.Parse(start[1]);
        z = double.Parse(start[2]);

        dx = double.Parse(vels[0]);
        dy = double.Parse(vels[1]);
        dz = double.Parse(vels[2]);
    }
}