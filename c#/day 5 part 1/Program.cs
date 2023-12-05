using System;
using System.Collections.Generic;
using System.Linq;

List<long> seeds = new List<long>();
string[] seedStrings = Console.ReadLine().Split(' ');
ArraySegment<string> slice = new ArraySegment<string>(seedStrings, 1, seedStrings.Length - 1);
foreach(string s in slice)
{
    seeds.Add(Convert.ToInt64(s));
}
Console.ReadLine();

foreach(int seed in seeds)
{
    Console.WriteLine(seed);
}

int t = 7;
while(t-- > 0)
{
    Console.ReadLine();

    string inputString;

    var vals = new List<(long, long, long)>();
    while((inputString = Console.ReadLine()) != "")
    {
        string[] inputs = inputString.Split(' ');
        long sourceStart = Convert.ToInt64(inputs[1]);
        long destStart = Convert.ToInt64(inputs[0]);
        long range = Convert.ToInt64(inputs[2]);
        var tup = (sourceStart, destStart, range);
        
        vals.Add(tup);
    }

    for (int i = 0; i < seeds.Count; i++)
    {
        long seed = seeds[i];
        foreach (var tuple in vals)
        {
            long sourceStart = tuple.Item1;
            long destStart = tuple.Item2;
            long range = tuple.Item3;

            if(seed >= sourceStart && seed < sourceStart + range){
                long newSeed = destStart + Math.Abs(seed - sourceStart);
                seeds[i] = newSeed;
                break;
            }
        }
    }

}

Console.WriteLine(seeds.Min());