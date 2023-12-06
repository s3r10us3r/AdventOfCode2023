static double ComputeWays(double t, double d)
{
    double p1 = Math.Ceiling(( (-t - Math.Sqrt(t*t - 4*d)) / -2) - 1);
    double p2 = Math.Floor(( (-t + Math.Sqrt(t*t - 4*d)) /- 2) + 1);

    return p1 - p2 + 1;
} 

string[] timeStringsRaw = Console.ReadLine().Split(' ');
string[] distStringRaw = Console.ReadLine().Split(' ');

string timeString = "";
string distString = "";


for(int i = 1; i < timeStringsRaw.Length; i++){
    timeString += timeStringsRaw[i];
}

for(int i = 1; i < distStringRaw.Length; i++){
    distString += distStringRaw[i];
}

double time = Convert.ToInt64(timeString);
double dist = Convert.ToInt64(distString);
Console.WriteLine($"{time} {dist}");

double result = ComputeWays(time, dist);


Console.WriteLine(result);