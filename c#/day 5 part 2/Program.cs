static List<(long, long)> SeedInput()
{
    var seeds = new List<(long, long)>();
    string[] seedStrings = Console.ReadLine().Split(' ');
    for(int i = 1; i < seedStrings.Length; i+= 2)
    {
        long start = Convert.ToInt64(seedStrings[i]);
        long end = start + Convert.ToInt64(seedStrings[i+1]) - 1;
        Console.WriteLine($"{start} {end}");
        seeds.Add((start, end));
    }
    return seeds;
}

static List<List<(long, long, long)>> ConvInput()
{
    var conversions = new List<List<(long, long, long)>>();
    for(int i = 0; i < 7; i++)
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

            var tup = (sourceStart, sourceStart + range - 1, destStart - sourceStart);
            
            vals.Add(tup);
        }
        conversions.Add(vals);
    }
    return conversions;
}

static List<(long, long)> ConvertRanges(List<(long, long)> ranges, List<(long, long, long)> conversions)
{
    var convertedRanges = new List<(long, long)>();


    foreach(var range in ranges)
    {
        var rangesToConsiderNext = new List<(long, long)>();
        rangesToConsiderNext.Add(range);

        foreach(var conversion in conversions)
        {
            long convStart = conversion.Item1;
            long convEnd = conversion.Item2;
            long offset = conversion.Item3;

            var rangesToConsider = rangesToConsiderNext;
            rangesToConsiderNext = new List<(long, long)>(); 
            foreach(var consideredRange in rangesToConsider)
            {
                long rangeStart = consideredRange.Item1;
                long rangeEnd = consideredRange.Item2;

                //range inside conversion
                if(rangeStart >= convStart && rangeEnd <= convEnd)
                {
                    convertedRanges.Add((rangeStart + offset, rangeEnd + offset));
                }
                //conversion inside range
                else if(convStart >= rangeStart && convEnd <= rangeEnd)
                {
                    convertedRanges.Add((convStart + offset, convEnd + offset));
                    //left range
                    if(convStart != rangeStart)
                    {
                        rangesToConsiderNext.Add((rangeStart, convStart - 1));
                    }
                    //right range
                    if(convEnd != rangeEnd)
                    {
                        rangesToConsiderNext.Add((convEnd + 1, rangeEnd));
                    }
                }
                //range starts outside of conversion but ends inside
                else if(rangeEnd >= convStart && rangeEnd <= convEnd)
                {
                    convertedRanges.Add((convStart + offset, rangeEnd + offset));
                    rangesToConsiderNext.Add((rangeStart, convStart - 1));
                }
                //range starts inside but ends outside
                else if(rangeStart >= convStart && rangeStart <= convEnd)
                {
                    convertedRanges.Add((rangeStart + offset, convEnd + offset));
                    rangesToConsiderNext.Add((convEnd + 1, rangeEnd));
                }
                else
                {
                    rangesToConsiderNext.Add(consideredRange);
                }
            }
        }

        foreach(var notConverted in rangesToConsiderNext)
        {
            convertedRanges.Add(notConverted);
        }   
    }

    return convertedRanges;
}

static long Main()
{
    var seeds = SeedInput();
    Console.ReadLine();

    var conversions = ConvInput();

    foreach (var conversion in conversions)
    {
        seeds = ConvertRanges(seeds, conversion);

    }

    Console.WriteLine(seeds.Count);

    long result = long.MaxValue;
    foreach(var seed in seeds)
    {
        if(seed.Item1 < result)
            result = seed.Item1;
    }
    return result;
}

Console.WriteLine(Main());