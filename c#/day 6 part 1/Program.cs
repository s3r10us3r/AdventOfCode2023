static double ComputeWays(int t, int d)
{
    double p1 = Math.Ceiling(( (-t - Math.Sqrt(t*t - 4*d)) / -2) - 1);
    double p2 = Math.Floor(( (-t + Math.Sqrt(t*t - 4*d)) /- 2) + 1);

    return p1 - p2 + 1;
} 

int c = 4;

int[] times = new int[c];
int[] distance = new int[c];

string[] timeStringsRaw = Console.ReadLine().Split(' ');
string[] distStringRaw = Console.ReadLine().Split(' ');

var timeStrings = new List<string>();
var distStrings = new List<string>();


foreach(string s in timeStringsRaw){
    if(s != "")
        timeStrings.Add(s);
}

foreach(string s in distStringRaw){
    if(s!= "")
        distStrings.Add(s);
}

for(int i = 1; i < c + 1; i++)
{
    times[i - 1] = Convert.ToInt32(timeStrings[i]);
    distance[i - 1] = Convert .ToInt32(distStrings[i]);
}

double result = 1;

for(int i = 0; i < c; i++)
{
    int t = times[i];
    int d = distance[i];

    result *= ComputeWays(t,d);
}

Console.WriteLine(result);