static int[] Extrapolate(int[] series)
{
    int[] diffSeries = new int[series.Length - 1]; 
    for (int i = 0; i < series.Length - 1; i++)
    {
        diffSeries[i] = series[i + 1] - series[i];
    }
    return diffSeries;
}

static long Solve(int[] series)
{
    foreach (int num in series)
    {
        if (num != 0)
            return series[0] - Solve(Extrapolate(series));
    }
    return 0;    
}

long result = 0;

int t = 200;
while(t-- > 0)
{
    string[] inputs = Console.ReadLine().Split(" ");
    int[] series = inputs.Select(int.Parse).ToArray();
    result += Solve(series);
}

Console.WriteLine(result);